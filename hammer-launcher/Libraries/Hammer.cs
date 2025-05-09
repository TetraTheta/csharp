using System.IO;

namespace HammerLauncher.Libraries;

public static class Hammer {
  private static readonly string HL2BasePath = Registry.GetHL2InstallPath();
  private static readonly string GModBasePath = Registry.GetGModInstallPath();

  public static string GetHL2H() {
    if (!string.IsNullOrWhiteSpace(HL2BasePath)) {
      string path = Path.Combine(HL2BasePath, "bin", "hammer.exe");
      return File.Exists(path) ? path : string.Empty;
    } else {
      return string.Empty;
    }
  }

  public static string GetHL2HPP() {
    if (!string.IsNullOrWhiteSpace(HL2BasePath)) {
      string path = Path.Combine(HL2BasePath, "bin", "hammerplusplus.exe");
      return File.Exists(path) ? path : string.Empty;
    } else {
      return string.Empty;
    }
  }

  public static string GetGModH() {
    if (!string.IsNullOrWhiteSpace(GModBasePath)) {
      string path = Path.Combine(GModBasePath, "bin", "hammer.exe");
      return File.Exists(path) ? path : string.Empty;
    } else {
      return string.Empty;
    }
  }

  public static string GetGModHPP() {
    if (!string.IsNullOrWhiteSpace(GModBasePath)) {
      string path = Path.Combine(GModBasePath, "bin", "win64", "hammerplusplus.exe");
      return File.Exists(path) ? path : string.Empty;
    } else {
      return string.Empty;
    }
  }
}
