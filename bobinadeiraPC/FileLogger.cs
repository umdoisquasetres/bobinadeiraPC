using System;
using System.IO;

namespace bobinadeiraPC
{
    public sealed class FileLogger
    {
        private static readonly Lazy<FileLogger> lazyInstance = new Lazy<FileLogger>(() => new FileLogger());
        private readonly string _logFilePath;

        public static FileLogger Instance => lazyInstance.Value;

        private FileLogger()
        {
            _logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bobinadeira_log.txt");
            Log("Logger inicializado.");
        }

        public void Log(string message)
        {
            try
            {
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}{Environment.NewLine}";
                File.AppendAllText(_logFilePath, logMessage);
            }
            catch
            {
                // Ignorar erros de log para não travar a aplicação.
            }
        }

        public void LogError(string message, Exception ex = null)
        {
            string errorMessage = $"ERRO: {message}";
            if (ex != null)
            {
                errorMessage += $"{Environment.NewLine}  Exception: {ex.Message}{Environment.NewLine}  StackTrace: {ex.StackTrace}";
            }
            Log(errorMessage);
        }
    }
}
