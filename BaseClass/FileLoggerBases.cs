using System.IO;
using NetSparkleUpdater.Interfaces;

namespace NetSparkleUpdaterApp.BaseClass;

public class FileLoggerBases : ILogger {
    private readonly  string _filePath;

    protected FileLoggerBases(string filePath = "sparkle-log.txt")
    {
        _filePath = filePath;
    }

    public virtual void PrintMessage(string message, params object[]? args) {
        if (args == null) return;
        var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {string.Format(message, args )}{Environment.NewLine}";
        File.AppendAllText(_filePath, line);
    }
}