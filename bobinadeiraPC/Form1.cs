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
            ResetContadorEAlarme(); // Reset antes de iniciar um novo ciclo
            if (await EnviarComandoComSeguranca(CommandConstants.StartWinding))
            {
                AtualizarStatusVisual("Status: Bobinando...");
            }
        }

        private async void btnParar_Click(object sender, EventArgs e)
        {
            if (await EnviarComandoComSeguranca(CommandConstants.StopWinding))
            {
                AtualizarStatusVisual("Status: Parado pelo usuário");
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
            label10.Text = mensagem;
        }

        private void ResetContadorEAlarme()
        {
            _voltasContador = 0;
            _alarmeAtivado = false;
            lblVoltasAtual.Text = "Voltas Atuais: 0";
            lblAlarme.Text = "";
            lblAlarme.Visible = false;
            lblAlarme.ForeColor = Color.Red; // Garante a cor padrão de alerta
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
            label13.Text = dadosRecebidos; // Continua mostrando o dado bruto para depuração

            // Verifica se é o comando de Contagem de Voltas (CNT:1)
            if (dadosRecebidos.StartsWith("CNT:", StringComparison.Ordinal))
            {
                _voltasContador++;
                lblVoltasAtual.Text = $"Voltas Atuais: {_voltasContador}";
                _logger.Log($"Volta detectada. Total: {_voltasContador}");

                // Calcula e atualiza o progresso
                decimal totalEspiras = numEspiras.Value;
                if (totalEspiras > 0)
                {
                    int progresso = (int)((_voltasContador / totalEspiras) * 100);
                    progressBar1.Value = Math.Clamp(progresso, 0, 100);
                }
                else
                {
                    progressBar1.Value = 0; // Se o total de espiras for 0, o progresso é 0
                }

                // Verifica condição de alarme (voltas excedidas)
                if (_voltasContador > totalEspiras && !_alarmeAtivado)
                {
                    _alarmeAtivado = true;
                    lblAlarme.Text = "ALERTA: Voltas excedidas!";
                    lblAlarme.Visible = true;
                    _logger.LogError($"ALERTA: Voltas excedidas! Voltas: {_voltasContador}, Esperado: {totalEspiras}");
                }
            }
        }
    }
}