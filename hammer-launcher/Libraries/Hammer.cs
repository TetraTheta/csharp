using System.IO;

namespace HammerLauncher.Libraries {
public static class Hammer {
  private static readonly string Hl2BasePath = SteamRegistry.GetHl2InstallPath();
  private static readonly string GModBasePath = SteamRegistry.GetGModInstallPath();

  public static string GetHl2H() {
    if (string.IsNullOrWhiteSpace(Hl2BasePath)) return string.Empty;
    string path = Path.Combine(Hl2BasePath, "bin", "hammer.exe");
    return File.Exists(path) ? path : string.Empty;
  }

  public static string GetHl2Hpp() {
    if (string.IsNullOrWhiteSpace(Hl2BasePath)) return string.Empty;
    string path = Path.Combine(Hl2BasePath, "bin", "hammerplusplus.exe");
    return File.Exists(path) ? path : string.Empty;
  }

  public static string GetGModH() {
    if (string.IsNullOrWhiteSpace(GModBasePath)) return string.Empty;
    string path = Path.Combine(GModBasePath, "bin", "hammer.exe");
    return File.Exists(path) ? path : string.Empty;
  }

  public static string GetGModHpp() {
    if (string.IsNullOrWhiteSpace(GModBasePath)) return string.Empty;
    string path = Path.Combine(GModBasePath, "bin", "win64", "hammerplusplus.exe");
    return File.Exists(path) ? path : string.Empty;
  }
}
}
