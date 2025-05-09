using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace HammerLauncher.Libraries;

public static class Registry {
  public static string GetHL2InstallPath() {
    return GetRegistryPath("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Steam App 220");
  }

  public static string GetGModInstallPath() {
    return GetRegistryPath("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Steam App 4000");
  }

  private static string GetRegistryPath(string path) {
    try {
      using RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(path, false);
      object v = key.GetValue("InstallLocation");
      return v is string s ? !string.IsNullOrWhiteSpace(s) ? s : string.Empty : string.Empty;
    } catch (Exception e) {
      MessageBox.Show($"Could not open '{path}':\n{e.Message}");
      return string.Empty;
    }
  }
}
