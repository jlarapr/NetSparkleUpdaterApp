using System.IO;
using NetSparkleUpdater.Interfaces;

public class FileLogger : ILogger
{
    private readonly string _filePath;

    public FileLogger(string filePath = "sparkle-log.txt")
    {
        _filePath = filePath;
    }

    public void PrintMessage(string message, params object[] args)
    {
        var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {string.Format(message, args)}{Environment.NewLine}";
        File.AppendAllText(_filePath, line);
    }
}