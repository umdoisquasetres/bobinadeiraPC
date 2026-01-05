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
            StopBits = StopBits.One
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
            _logger.Log($"Conectado com sucesso na porta {portName}.");
            ConnectionStatusChanged?.Invoke(this, true);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao conectar na porta {portName}.", ex);
            ConnectionStatusChanged?.Invoke(this, false);
            throw; // Re-lança a exceção para ser tratada pela UI
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
            // A porta pode já ter sido fechada ou removida.
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
            await Task.Run(() => _serialPort.Write(command));
            _logger.Log($"Comando enviado: {command.Trim()}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha no envio do comando: {command.Trim()}", ex);
            // Lança uma exceção mais específica ou com mais detalhes
            throw new IOException($"Falha no envio do comando: {ex.Message}", ex);
        }
    }

    private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        try
        {
            string data = _serialPort.ReadLine();
            _logger.Log($"Dados recebidos: {data.Trim()}");
            DataReceived?.Invoke(this, data);
        }
        catch (Exception ex)
        {
            _logger.LogError("Erro ao ler dados da porta serial.", ex);
            // Ignorar erros de leitura que podem ocorrer em desconexões abruptas
        }
    }

    public void Dispose()
    {
        _serialPort?.Dispose();
    }
}