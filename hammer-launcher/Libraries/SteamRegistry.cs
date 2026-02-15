using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace HammerLauncher.Libraries {
public static class SteamRegistry {
  public static string GetHl2InstallPath() {
    return GetRegistryPath("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Steam App 220");
  }

  public static string GetGModInstallPath() {
    return GetRegistryPath("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Steam App 4000");
  }

  private static string GetRegistryPath(string path) {
    try {
      using (var hklm64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
      using (var key = hklm64.OpenSubKey(path, false)) { return key?.GetValue("InstallLocation") as string ?? string.Empty; }
    } catch (Exception e) {
      // ReSharper disable once LocalizableElement
      MessageBox.Show($"Could not open '{path}':\n{e.Message}");
      return string.Empty;
    }
  }
}
}
