using System;
using System.IO;
using System.IO.Ports;
using System.Threading.Tasks;

namespace bobinadeiraPC;

public class SerialCommunicator : IDisposable
{
    private readonly SerialPort _serialPort;
    private readonly FileLogger _logger = FileLogger.Instance;

    public event EventHandler<string>? DataReceived;
    public event EventHandler<bool>? ConnectionStatusChanged;

    public bool IsConnected => _serialPort.IsOpen;

    public SerialCommunicator()
    {
        _serialPort = new SerialPort
        {
            BaudRate = 115200,
            DataBits = 8,
            Parity = Parity.None,
            StopBits = StopBits.One,
            ReadTimeout = 500,        // Timeout de leitura
            WriteTimeout = 500,       // Timeout de escrita
            DtrEnable = true,         // Importante para alguns Arduinos
            RtsEnable = true          // Importante para alguns Arduinos
        };

        _serialPort.DataReceived += SerialPort_DataReceived;
    }

    public static string[] GetAvailablePorts()
    {
        return SerialPort.GetPortNames();
    }

    public async Task Connect(string portName)
    {
        if (IsConnected) return;

        _logger.Log($"Tentando conectar na porta {portName}...");
        _serialPort.PortName = portName;

        try
        {
            await Task.Run(() => _serialPort.Open());

            // Limpa buffers ao conectar
            _serialPort.DiscardInBuffer();
            _serialPort.DiscardOutBuffer();

            _logger.Log($"Conectado com sucesso na porta {portName}.");
            ConnectionStatusChanged?.Invoke(this, true);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao conectar na porta {portName}.", ex);
            ConnectionStatusChanged?.Invoke(this, false);
            throw;
        }
    }

    public async Task Disconnect()
    {
        if (!IsConnected) return;

        string portName = _serialPort.PortName;
        _logger.Log($"Tentando desconectar da porta {portName}...");

        try
        {
            await Task.Run(() => _serialPort.Close());
            _logger.Log($"Desconectado com sucesso da porta {portName}.");
            ConnectionStatusChanged?.Invoke(this, false);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao desconectar da porta {portName}.", ex);
            ConnectionStatusChanged?.Invoke(this, false);
        }
    }

    public async Task SendCommand(string command)
    {
        if (!IsConnected)
        {
            var ex = new InvalidOperationException("A porta serial não está conectada.");
            _logger.LogError("Tentativa de enviar comando sem conexão.", ex);
            throw ex;
        }

        try
        {
            // ===== CORREÇÃO PRINCIPAL =====
            // Adiciona \n ao final do comando se não tiver
            string comandoComTerminador = command;
            if (!command.EndsWith("\n"))
            {
                comandoComTerminador = command + "\n";
            }

            await Task.Run(() => _serialPort.Write(comandoComTerminador));
            _logger.Log($"Comando enviado: {command.Trim()}");

            // Pequeno delay para dar tempo do Arduino processar
            await Task.Delay(50);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha no envio do comando: {command.Trim()}", ex);
            throw new IOException($"Falha no envio do comando: {ex.Message}", ex);
        }
    }

    private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        try
        {
            // Lê enquanto houver dados disponíveis
            while (_serialPort.BytesToRead > 0)
            {
                string data = _serialPort.ReadLine();

                // Ignora linhas vazias
                if (string.IsNullOrWhiteSpace(data))
                    continue;

                _logger.Log($"Dados recebidos: {data.Trim()}");
                DataReceived?.Invoke(this, data);
            }
        }
        catch (TimeoutException)
        {
            // Timeout é normal, ignora
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao ler dados da porta serial.", ex);
        }
    }

    public void Dispose()
    {
        if (_serialPort?.IsOpen == true)
        {
            try
            {
                _serialPort.Close();
            }
            catch { /* Ignora erros ao fechar */ }
        }
        _serialPort?.Dispose();
    }
}