using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace SimpleLog;

public class Logger {
  private const string LOG_EXT = ".log";
  private readonly object fileLock = new();
  private readonly string logDir = Path.GetTempPath();
  private readonly string logFilePrefix;
  private readonly string logFilePath;
  private readonly bool append;

  [Flags]
  private enum LogLevel { TRACE, INFO, DEBUG, WARNING, ERROR, FATAL }

  public Logger(string? logFilePrefix, bool append = true) {
    this.append = append;
    this.logFilePrefix = logFilePrefix ?? Assembly.GetExecutingAssembly().GetName().Name;

    logFilePath = Path.Combine(logDir, $"{this.logFilePrefix}_{DateTime.Now:yyyy-MM-dd}{LOG_EXT}");

    if (!File.Exists(logFilePath)) {
      File.Create(logFilePath).Close();
    }
  }

  public void Trace(string text) => WriteFormattedLog(LogLevel.TRACE, text);
  public void Info(string text) => WriteFormattedLog(LogLevel.INFO, text);
  public void Debug(string text) => WriteFormattedLog(LogLevel.DEBUG, text);
  public void Warning(string text) => WriteFormattedLog(LogLevel.WARNING, text);
  public void Error(string text) => WriteFormattedLog(LogLevel.ERROR, text);
  public void Fatal(string text) => WriteFormattedLog(LogLevel.FATAL, text);

  private void WriteLine(string text) {
    if (string.IsNullOrEmpty(text)) return;
    lock (fileLock) {
      using StreamWriter writer = new(logFilePath, append, Encoding.UTF8);
      writer.WriteLine(text);
    }
  }

  private void WriteFormattedLog(LogLevel level, string text) {
    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
    string prefix = level switch {
      LogLevel.TRACE => $"[{time}] [TRACE] ",
      LogLevel.INFO => $"[{time}] [INFO ] ",
      LogLevel.DEBUG => $"[{time}] [DEBUG] ",
      LogLevel.WARNING => $"[{time}] [WARN ] ",
      LogLevel.ERROR => $"[{time}] [ERROR] ",
      LogLevel.FATAL => $"[{time}] [FATAL] ",
      _ => $"[{time}] ",
    };
    WriteLine($"{prefix}{text}");
  }
}
