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

            // Define o texto inicial da barra de status
            toolStripStatusLabel1.Text = "Desconectado";

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
        // Sobreescreve o método Dispose
        // ==========================================
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                _communicator?.Dispose();
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
#pragma warning disable CS8600, CS8604 
                string portName = (string)cboPortas.SelectedItem!;
#pragma warning restore CS8600, CS8604
                try
                {
                    btnConectar.Enabled = false;
                    await _communicator.Connect(portName);

                    // --- CORREÇÃO DO CLIQUE DUPLO ---
                    // Forçamos os botões a ficarem desabilitados enquanto o Arduino reinicia
                    btnEnviarConfig.Enabled = false;
                    btnIniciar.Enabled = false;
                    btnParar.Enabled = false;

                    AtualizarStatusVisual("Aguardando Arduino iniciar...");
                    toolStripStatusLabel1.Text = "Inicializando placa...";

                    // Aguarda 2.5 segundos (tempo seguro para qualquer Arduino)
                    await Task.Delay(2500);

                    // Agora sim, libera os botões
                    btnEnviarConfig.Enabled = true;
                    btnIniciar.Enabled = true;
                    btnParar.Enabled = true;

                    toolStripStatusLabel1.Text = $"Conectado em {portName}";
                    AtualizarStatusVisual("Conectado - Pronto para configurar");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Falha ao conectar: {ex.Message}", ex);
                    MessageBox.Show($"Erro ao abrir a porta: {ex.Message}", "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnConectar.Enabled = true;
                }
            }
            else
            {
                await _communicator.Disconnect();
            }
        }

        private async void btnEnviarConfig_Click(object sender, EventArgs e)
        {
            // Atualiza interface ANTES de enviar
            AtualizarStatusVisual("Enviando configuração...");

            string comandoConfig = string.Format(CultureInfo.InvariantCulture,
                CommandConstants.ConfigFormat,
                numEspiras.Value,
                numRPM.Value,
                numDiametro.Value);

            if (await EnviarComandoComSeguranca(comandoConfig))
            {
                // Mostra confirmação visual imediata
                AtualizarStatusVisual($"Config OK - {numEspiras.Value} espiras e {numRPM.Value} rpm");
            }
            else
            {
                AtualizarStatusVisual("✗ Erro ao enviar configuração");
            }
        }

        private async void btnIniciar_Click(object sender, EventArgs e)
        {
            _prontoParaResetar = false;
            btnParar.Text = "Parar";

            ResetContadorEAlarme();
            await EnviarComandoComSeguranca(CommandConstants.ResetWinding);

            // Pequeno delay entre comandos
            await Task.Delay(100);

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
                AtualizarStatusVisual("Resetado - Pronto para nova bobinagem");
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
        // 4. Métodos Auxiliares
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
            _logger.Log($"Status UI: {mensagem}");
        }

        private void ResetContadorEAlarme()
        {
            _voltasContador = 0;
            _alarmeAtivado = false;
            lblVoltasAtual.Text = "0";
            lblProgressStatus.Text = "0%";
            lblAlarms.Text = "";
            lblAlarms.Visible = false;
            lblAlarms.ForeColor = Color.Red;
            progressBar1.Value = 0;
            _logger.Log("Contador de voltas e alarme resetados.");
        }

        // ==========================================
        // 5. Recepção de Dados
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
                AtualizarStatusVisual("Desconectado");
                ResetContadorEAlarme();
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

            // --- FILTRO DE SEGURANÇA ---
            // Ignora linhas vazias ou mensagens duplicadas/corrompidas
            if (string.IsNullOrWhiteSpace(dadosRecebidos))
                return;

            // Detecta mensagens corrompidas (ex: "STATUS:STATUS:...")
            if (dadosRecebidos.IndexOf("STATUS:", 7) > -1 ||
                dadosRecebidos.IndexOf("PROG:", 5) > -1)
            {
                _logger.LogError($"Mensagem corrompida ignorada: {dadosRecebidos}");
                return;
            }

            // ===== PROCESSAMENTO DE STATUS =====
            if (dadosRecebidos.StartsWith("STATUS:", StringComparison.Ordinal))
            {
                string statusMsg = dadosRecebidos.Substring(7).Trim();

                // --- CONTAGEM DE VOLTAS ---
                if (statusMsg.StartsWith("Voltas:", StringComparison.Ordinal))
                {
                    string voltasStr = statusMsg.Substring(7).Trim();
                    if (int.TryParse(voltasStr, out int voltas))
                    {
                        _voltasContador = voltas;
                        lblVoltasAtual.Text = $"{_voltasContador}";

                        // Verifica alarme de excesso
                        decimal totalEspiras = numEspiras.Value;
                        if (_voltasContador > totalEspiras && !_alarmeAtivado)
                        {
                            _alarmeAtivado = true;
                            lblAlarms.Text = "⚠ ALERTA: Voltas excedidas!";
                            lblAlarms.Visible = true;
                            _logger.LogError($"ALERTA: Voltas excedidas! Voltas: {_voltasContador}");
                        }
                    }
                }
                // --- CONFIGURAÇÃO RECEBIDA ---
                else if (statusMsg.StartsWith("Config", StringComparison.OrdinalIgnoreCase))
                {
                    // Arduino confirmou - não faz nada pois já mostramos no botão
                    // Evita mensagem duplicada
                }
                // --- MÁQUINA CONCLUÍDA ---
                else if (statusMsg == "CONCLUIDO")
                {
                    AtualizarStatusVisual("✓ Bobinagem concluída!");
                    _prontoParaResetar = true;
                    btnParar.Text = "Resetar";
                }
                // --- ESTADOS INTERNOS (não mostrar na UI) ---
                else if (statusMsg == "RESETADO" || statusMsg == "INICIADO")
                {
                    // Ignora - já tem feedback próprio nos botões
                }
                // --- OUTROS STATUS ---
                else
                {
                    // Remove prefixo "Status:" se existir na mensagem
                    if (statusMsg.StartsWith("Status:", StringComparison.OrdinalIgnoreCase))
                    {
                        statusMsg = statusMsg.Substring(7).Trim();
                    }

                    AtualizarStatusVisual(statusMsg);
                }
            }
            // ===== PROCESSAMENTO DE PROGRESSO =====
            else if (dadosRecebidos.StartsWith("PROG:", StringComparison.Ordinal))
            {
                string progStr = dadosRecebidos.Substring(5).Trim();
                if (int.TryParse(progStr, out int progresso))
                {
                    progresso = Math.Clamp(progresso, 0, 100);
                    progressBar1.Value = progresso;
                    lblProgressStatus.Text = $"{progresso}%";
                }
            }
        }
    }
}