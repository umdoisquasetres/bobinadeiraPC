using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bobinadeiraPC
{
    public partial class Form1 : Form
    {
        // ==========================================
        // 1. Variáveis e Objetos Globais
        // ==========================================
        private readonly SerialCommunicator _communicator;
        private readonly FileLogger _logger = FileLogger.Instance;
        private int _voltasContador = 0;
        private bool _alarmeAtivado = false;
        private bool _prontoParaResetar = false;

        // ==========================================
        // 2. Inicialização (Construtor)
        // ==========================================
        public Form1()
        {
            InitializeComponent();
            _logger.Log("Aplicação iniciada.");

            _communicator = new SerialCommunicator();
            _communicator.ConnectionStatusChanged += OnConnectionStatusChanged;
            _communicator.DataReceived += OnDataReceived;

            // Inicializa estado do contador e alarme
            ResetContadorEAlarme();

            CarregarPortasDisponiveis();
            AlternarBloqueioDeControles(false); // Estado inicial desconectado
        }
        
        // ==========================================
        // Sobreescreve o método Dispose para garantir a liberação dos recursos do comunicador
        // ==========================================
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                _communicator?.Dispose(); // Dispose do SerialCommunicator
            }
            base.Dispose(disposing);
        }

        // ==========================================
        // 3. Lógica dos Botões (Event Handlers)
        // ==========================================

        private void btnAtualizarPortas_Click(object sender, EventArgs e)
        {
            _logger.Log("Atualizando a lista de portas seriais.");
            CarregarPortasDisponiveis();
        }

        private async void btnConectar_Click(object sender, EventArgs e)
        {
            if (!_communicator.IsConnected)
            {
                if (cboPortas.SelectedItem == null)
                {
                    MessageBox.Show("Selecione uma porta COM na lista.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
#pragma warning disable CS8600, CS8604 // Suppress warnings about potential null reference as it's checked above
                string portName = (string)cboPortas.SelectedItem!;
#pragma warning restore CS8600, CS8604
                try
                {
                    btnConectar.Enabled = false; // Desabilita o botão durante a tentativa
                    await _communicator.Connect(portName);
                    toolStripStatusLabel1.Text = $"Conectado em {portName}";
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Falha ao conectar: {ex.Message}", ex);
                    MessageBox.Show($"Erro ao abrir a porta: {ex.Message}", "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnConectar.Enabled = true; // Reabilita o botão
                }
            }
            else
            {
                await _communicator.Disconnect();
            }
        }

        private async void btnEnviarConfig_Click(object sender, EventArgs e)
        {
            string comandoConfig = string.Format(CultureInfo.InvariantCulture,
                CommandConstants.ConfigFormat,
                numEspiras.Value,
                numRPM.Value,
                numDiametro.Value);

            await EnviarComandoComSeguranca(comandoConfig);
        }

        private async void btnIniciar_Click(object sender, EventArgs e)
        {
            // Garante que o estado de "Resetar" seja desfeito ao iniciar
            _prontoParaResetar = false;
            btnParar.Text = "Parar";

            // 1. Reseta o contador no App e no Arduino
            ResetContadorEAlarme();
            await EnviarComandoComSeguranca(CommandConstants.ResetWinding);

            // 2. Inicia o processo
            if (await EnviarComandoComSeguranca(CommandConstants.StartWinding))
            {
                AtualizarStatusVisual("Bobinando...");
            }
        }

        private async void btnParar_Click(object sender, EventArgs e)
        {
            if (_prontoParaResetar)
            {
                await EnviarComandoComSeguranca(CommandConstants.ResetWinding);
                ResetContadorEAlarme();
                _prontoParaResetar = false;
                btnParar.Text = "Parar";
                AtualizarStatusVisual("Pronto");
            }
            else
            {
                if (await EnviarComandoComSeguranca(CommandConstants.StopWinding))
                {
                    AtualizarStatusVisual("Parado pelo usuário");
                    _prontoParaResetar = true;
                    btnParar.Text = "Resetar";
                }
            }
        }

        // ==========================================
        // 4. Métodos Auxiliares de Lógica
        // ==========================================

        private void CarregarPortasDisponiveis()
        {
            cboPortas.Items.Clear();
            string[] portasEncontradas = SerialCommunicator.GetAvailablePorts();
            cboPortas.Items.AddRange(portasEncontradas);

            if (cboPortas.Items.Count > 0)
                cboPortas.SelectedIndex = 0;
            else
                _logger.Log("Nenhuma porta serial encontrada.");
        }

        private async Task<bool> EnviarComandoComSeguranca(string comando)
        {
            try
            {
                await _communicator.SendCommand(comando);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao enviar comando: {ex.Message}", ex);
                MessageBox.Show(ex.Message, "Erro de Comunicação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void AlternarBloqueioDeControles(bool estaConectado)
        {
            cboPortas.Enabled = !estaConectado;
            btnAtualizarPortas.Enabled = !estaConectado;
            btnConectar.Text = estaConectado ? "Desconectar" : "Conectar";
            
            btnEnviarConfig.Enabled = estaConectado;
            btnIniciar.Enabled = estaConectado;
            btnParar.Enabled = estaConectado;
            
            numEspiras.Enabled = true;
            numRPM.Enabled = true;
            numDiametro.Enabled = true;
        }

        private void AtualizarStatusVisual(string mensagem)
        {
            lblStatus.Text = mensagem;
        }

        private void ResetContadorEAlarme()
        {
            _voltasContador = 0;
            _alarmeAtivado = false;
            lblVoltasAtual.Text = "0";
            lblProgressStatus.Text = "0%";
            lblAlarms.Text = "";
            lblAlarms.Visible = false;
            lblAlarms.ForeColor = Color.Red; // Garante a cor padrão de alerta
            progressBar1.Value = 0;
            _logger.Log("Contador de voltas e alarme resetados.");
        }

        // ==========================================
        // 5. Recepção de Dados (Vindo do SerialCommunicator)
        // ==========================================

        private void OnConnectionStatusChanged(object? sender, bool isConnected)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnConnectionStatusChanged(sender, isConnected)));
                return;
            }

            AlternarBloqueioDeControles(isConnected);
            if (!isConnected)
            {
                toolStripStatusLabel1.Text = "Desconectado";
                AtualizarStatusVisual("Status: Desconectado");
            }
        }

        private void OnDataReceived(object? sender, string data)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnDataReceived(sender, data)));
                return;
            }

            string dadosRecebidos = data.Trim();

            if (dadosRecebidos.StartsWith("STATUS:", StringComparison.Ordinal))
            {
                string statusMsg = dadosRecebidos.Substring(7);
                if (statusMsg.StartsWith("Voltas Reais: ", StringComparison.Ordinal))
                {
                    string voltasStr = statusMsg.Substring(14);
                    if (int.TryParse(voltasStr, out int voltas))
                    {
                        _voltasContador = voltas;
                        lblVoltasAtual.Text = $"{_voltasContador}";
                        _logger.Log($"Voltas recebidas: {_voltasContador}");

                        // Verifica condição de alarme (voltas excedidas)
                        decimal totalEspiras = numEspiras.Value;
                        if (_voltasContador > totalEspiras && !_alarmeAtivado)
                        {
                            _alarmeAtivado = true;
                            lblAlarms.Text = "ALERTA: Voltas excedidas!";
                            lblAlarms.Visible = true;
                            _logger.LogError($"ALERTA: Voltas excedidas! Voltas: {_voltasContador}, Esperado: {totalEspiras}");
                        }
                    }
                }
                else
                {
                    // Filtra mensagens de status que não devem ser mostradas diretamente
                    if (statusMsg == "RESETADO" || statusMsg == "INICIADO")
                    {
                        // Ignora esses status intermediários para uma UI mais limpa
                    }
                    else if (statusMsg == "Config Recebida")
                    {
                        AtualizarStatusVisual("Configuração recebida");
                    }
                    else
                    {
                        AtualizarStatusVisual(statusMsg);
                    }

                    if (statusMsg == "CONCLUIDO")
                    {
                        _prontoParaResetar = true;
                        btnParar.Text = "Resetar";
                    }
                }
            }
            else if (dadosRecebidos.StartsWith("PROG:", StringComparison.Ordinal))
            {
                string progStr = dadosRecebidos.Substring(5);
                if (int.TryParse(progStr, out int progresso))
                {
                    progressBar1.Value = Math.Clamp(progresso, 0, 100);
                    lblProgressStatus.Text = $"{progresso}%";
                }
            }
        }
    }
}