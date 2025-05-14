using Configuration;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace SkipUAC;

internal static class Program {
  [STAThread]
  private static void Main(string[] args) {
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);

    if (args.Length == 0) {
      MessageBox.Show("SkipUAC.exe <config txt file path> [/register]", "Usage");
      return;
    }

    // Get absolute path of config file
    string configFile;
    try {
      configFile = Path.GetFullPath(args[0]);
    } catch (Exception ex) {
      MessageBox.Show(ex.ToString(), ex.GetType().FullName);
      return;
    }

    bool forceRegister = args.Length > 1 && args[1].Equals("/register", StringComparison.OrdinalIgnoreCase);
    string taskName = Path.GetFileNameWithoutExtension(configFile);

    // Re-register task if needed
    if (forceRegister) {
      if (!IsAdministrator()) {
        RestartAdmin(args);
        return;
      }
      RegisterTask(configFile);
      RunTask(taskName);
      return;
    }

    // Run task if the task exists
    if (RunTask(taskName)) return;

    // Check admin privilege before registering new task
    if (!IsAdministrator()) {
      RestartAdmin(args);
    }

    // Register new task
    RegisterTask(configFile);
    RunTask(taskName);
  }

  private static bool IsAdministrator() => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

  private static void RestartAdmin(string[] args) {
    var psi = new ProcessStartInfo {
      FileName = Application.ExecutablePath,
      UseShellExecute = true,
      Verb = "runas",
      Arguments = string.Join(" ", args.Select(a => $"\"{a}\"")),
      WorkingDirectory = Environment.CurrentDirectory
    };
    try {
      Process.Start(psi);
    } catch (Exception ex) {
      MessageBox.Show(ex.ToString(), ex.GetType().FullName);
      return;
    }
  }

  private static void RegisterTask(string configFile) {
    // Parse config file
    List<ProcessStartInfo> psis = [];
    try {
      psis = [.. TextConfig.ParseCommandLine(configFile)];
    } catch (Exception ex) {
      MessageBox.Show(ex.ToString(), ex.GetType().FullName);
    }
    if (psis.Count > 0) {
      using TaskService ts = new();
      TaskDefinition td = ts.NewTask();
      foreach (ProcessStartInfo p in psis) {
        td.Actions.Add(new ExecAction(p.FileName, p.Arguments, p.WorkingDirectory));
        td.RegistrationInfo.Description += p.FileName + "\n";
      }
      td.Principal.LogonType = TaskLogonType.InteractiveToken;
      td.Principal.RunLevel = TaskRunLevel.Highest;
      td.Settings.AllowDemandStart = true;

      string taskName = Path.GetFileNameWithoutExtension(configFile);

      TaskFolder skipUAC = ts.GetFolder(@"\SkipUAC");
      if (skipUAC is null) {
        ts.RootFolder.CreateFolder("SkipUAC");
        skipUAC = ts.GetFolder(@"\SkipUAC");
      }

      skipUAC.RegisterTaskDefinition(taskName, td);
    }
  }

  private static bool RunTask(string taskName) {
    Task task = new TaskService().GetTask($"SkipUAC\\{taskName}");
    if (task == null) {
      return false;
    } else {
      task.Run();
      return true;
    }
  }
}
