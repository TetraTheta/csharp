using System.IO;

namespace HammerLauncher.Common.Hammer;

public static class Hammer {
  private static readonly string HL2BasePath = SteamRegistry.GetHL2InstallPath();
  private static readonly string GModBasePath = SteamRegistry.GetGModInstallPath();

  public static string GetHL2Hammer() {
    if (string.IsNullOrWhiteSpace(HL2BasePath)) return string.Empty;
    string path = Path.Combine(HL2BasePath, "bin", "hammer.exe");
    return File.Exists(path) ? path : string.Empty;
  }

  public static string GetHL2HammerPP() {
    if (string.IsNullOrWhiteSpace(HL2BasePath)) return string.Empty;
    string path = Path.Combine(HL2BasePath, "bin", "hammerplusplus.exe");
    return File.Exists(path) ? path : string.Empty;
  }

  public static string GetGModHammer() {
    if (string.IsNullOrWhiteSpace(GModBasePath)) return string.Empty;
    string path = Path.Combine(GModBasePath, "bin", "hammer.exe");
    return File.Exists(path) ? path : string.Empty;
  }

  public static string GetGModHammerPP() {
    if (string.IsNullOrWhiteSpace(GModBasePath)) return string.Empty;
    string path = Path.Combine(GModBasePath, "bin", "win64", "hammerplusplus.exe");
    return File.Exists(path) ? path : string.Empty;
  }
}
