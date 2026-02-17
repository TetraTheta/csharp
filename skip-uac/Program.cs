using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;

namespace SkipUAC {
[SuppressMessage("ReSharper", "LocalizableElement")]
internal static class Program {
  private enum ExecutionMode {
    Parallel, // default
    Sequential, // '>'
    SequentialStopOnError // '&'
  }

  private const string TaskFolderName = "SkipUAC";

  [STAThread]
  [SuppressMessage("ReSharper", "LocalizableElement")]
  private static void Main(string[] args) {
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);

    if (args.Length == 0) {
      MessageBox.Show("Usage:\n  SkipUAC.exe <config.txt> [/register]\n  SkipUAC.exe <config.txt> [/run] (internal - invoked by task)", "Usage");
      return;
    }

    string configFile;
    try { configFile = Path.GetFullPath(args[0]); } catch (Exception e) {
      MessageBox.Show($"Invalid config path: {e.Message}", "Path Error");
      return;
    }

    bool doRegister = args.Length > 1 && args[1].Equals("/register", StringComparison.OrdinalIgnoreCase);
    bool doRun = args.Length > 1 && args[1].Equals("/run", StringComparison.OrdinalIgnoreCase);

    string taskName = Path.GetFileNameWithoutExtension(configFile);

    if (doRun) {
      RunFromConfig(configFile);
      return;
    }

    if (doRegister) {
      if (!IsAdmin()) {
        RestartAsAdmin(new[] { $"\"{configFile}\"", "/register" });
        return;
      }

      RegisterAndRun(configFile, taskName);
      return;
    }

    if (TryRunTask(taskName)) return;
    if (!IsAdmin()) { RestartAsAdmin(new[] { $"\"{configFile}\"", "/register" }); } else { RegisterAndRun(configFile, taskName); }
  }

  private static void RegisterAndRun(string configFile, string taskName) {
    try {
      RegisterTask(configFile, taskName);
      TryRunTask(taskName);
    } catch (Exception e) { MessageBox.Show($"Registration failed: {e.Message}", "Error"); }
  }

  private static bool IsAdmin() => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

  private static bool IsExecutable(string file) {
    var ext = Path.GetExtension(file)?.ToLowerInvariant() ?? string.Empty;
    return ext == ".exe" || ext == ".com" || ext == ".bat" || ext == ".cmd" || ext == ".ps1";
  }

  private static void RestartAsAdmin(string[] args) {
    var psi = new ProcessStartInfo {
      FileName = Application.ExecutablePath,
      UseShellExecute = true,
      Verb = "runas",
      Arguments = string.Join(" ", args),
      WorkingDirectory = Environment.CurrentDirectory
    };
    // ReSharper disable once EmptyGeneralCatchClause
    try { Process.Start(psi); } catch { }
  }

  private static void RegisterTask(string configFile, string taskName) {
    string exePath = Application.ExecutablePath;

    using (var ts = new TaskService()) {
      var td = ts.NewTask();
      td.RegistrationInfo.Description = $"SkipUAC Task for {Path.GetFileName(configFile)}";

      if (IsExecutable(configFile)) {
        var ext = Path.GetExtension(configFile)?.ToLowerInvariant();
        switch (ext) {
          case ".bat":
          case ".cmd":
            td.Actions.Add(new ExecAction("cmd.exe", $"/c \"{configFile}\"", Path.GetDirectoryName(configFile) ?? Environment.CurrentDirectory));
            break;
          case ".ps1":
            td.Actions.Add(new ExecAction("powershell.exe", $"-NoProfile -ExecutionPolicy Bypass -File \"{configFile}\"", Path.GetDirectoryName(configFile) ?? Environment.CurrentDirectory));
            break;
          default:
            td.Actions.Add(new ExecAction(configFile, null, Path.GetDirectoryName(configFile) ?? Environment.CurrentDirectory));
            break;
        }
      } else {
        string actionArguments = $"\"{configFile}\" /run";
        td.Actions.Add(new ExecAction(exePath, actionArguments, Path.GetDirectoryName(exePath)));
      }

      td.Principal.LogonType = TaskLogonType.InteractiveToken;
      td.Principal.RunLevel = TaskRunLevel.Highest;
      td.Settings.AllowDemandStart = true;
      td.Settings.DisallowStartIfOnBatteries = false;
      td.Settings.StopIfGoingOnBatteries = false;

      TaskFolder skipFolder = ts.RootFolder.SubFolders.FirstOrDefault(f => f.Name == TaskFolderName) ?? ts.RootFolder.CreateFolder(TaskFolderName);
      skipFolder.RegisterTaskDefinition(taskName, td);
    }
  }

  private static bool TryRunTask(string taskName) {
    try {
      using (var ts = new TaskService()) {
        var folder = ts.RootFolder.SubFolders.FirstOrDefault(f => f.Name == TaskFolderName);
        var t = folder?.GetTasks().FirstOrDefault(x => x.Name == taskName);
        if (t == null) return false;

        t.Run();
        return true;
      }
    } catch { return false; }
  }

  private static void RunFromConfig(string configFile) {
    if (!File.Exists(configFile)) return;

    var entries = ParseConfig(configFile);
    if (entries.Count == 0) return;

    ExecuteEntries(entries);
  }

  private static List<(ExecutionMode mode, ProcessStartInfo psi, string originalLine)> ParseConfig(string configFile) {
    var entries = new List<(ExecutionMode, ProcessStartInfo, string)>();
    foreach (var rawLine in File.ReadAllLines(configFile, Encoding.UTF8)) {
      string line = rawLine.Trim();
      if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;

      ExecutionMode mode = ExecutionMode.Parallel;
      if (line.StartsWith(">")) {
        mode = ExecutionMode.Sequential;
        line = line.Substring(1).Trim();
      } else if (line.StartsWith("&")) {
        mode = ExecutionMode.SequentialStopOnError;
        line = line.Substring(1).Trim();
      }

      try {
        var tokens = Tokenize(line);
        if (tokens.Count == 0) continue;

        var psi = new ProcessStartInfo {
          FileName = tokens[0],
          Arguments = tokens.Count > 1 ? string.Join(" ", tokens.Skip(1).Select(QuoteIfNeeded)) : "",
          UseShellExecute = false,
          WorkingDirectory = Path.GetDirectoryName(configFile) ?? Environment.CurrentDirectory
        };
        entries.Add((mode, psi, rawLine));
        // ReSharper disable once EmptyGeneralCatchClause
      } catch { }
    }

    return entries;
  }

  private static void ExecuteEntries(List<(ExecutionMode mode, ProcessStartInfo psi, string originalLine)> entries) {
    var parallelProcesses = new List<Process>();

    foreach (var entry in entries) {
      if (entry.mode == ExecutionMode.Parallel) {
        var p = SafeStart(entry.psi);
        if (p != null) parallelProcesses.Add(p);
      } else {
        // Flush parallel processes before starting sequential ones
        WaitAndDispose(parallelProcesses);

        using (var p = SafeStart(entry.psi)) {
          if (p == null) {
            if (entry.mode == ExecutionMode.SequentialStopOnError) break;
            continue;
          }

          p.WaitForExit();
          if (entry.mode == ExecutionMode.SequentialStopOnError && p.ExitCode != 0) break;
        }
      }
    }

    WaitAndDispose(parallelProcesses);
  }

  private static Process SafeStart(ProcessStartInfo psi) {
    try { return Process.Start(psi); } catch { return null; }
  }

  private static void WaitAndDispose(List<Process> processes) {
    foreach (var p in processes) {
      try {
        p.WaitForExit();
        p.Dispose();
        // ReSharper disable once EmptyGeneralCatchClause
      } catch { }
    }

    processes.Clear();
  }

  private static List<string> Tokenize(string cmd) {
    var res = new List<string>();
    var sb = new StringBuilder();
    bool insideQuotes = false;

    foreach (char c in cmd) {
      if (c == '"') insideQuotes = !insideQuotes;
      else if (char.IsWhiteSpace(c) && !insideQuotes) {
        if (sb.Length <= 0) continue;
        res.Add(sb.ToString());
        sb.Clear();
      } else sb.Append(c);
    }

    if (sb.Length > 0) res.Add(sb.ToString());
    return res;
  }

  private static string QuoteIfNeeded(string arg) => arg.Contains(" ") ? $"\"{arg}\"" : arg;
}
}
