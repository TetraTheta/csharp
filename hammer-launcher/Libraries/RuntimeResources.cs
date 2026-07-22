using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;

using HammerLauncher.Properties;

namespace HammerLauncher.Libraries;

internal static class RuntimeResources {
  private const string Prefix = "HammerLauncher.Resources.";
  private static readonly Assembly Assembly = typeof(RuntimeResources).Assembly;

  internal static Icon MainIcon => IsDesigner() ? Resources.MainIcon : LoadIcon("MainIcon.ico");
  internal static Image GameGModGray_32 => IsDesigner() ? Resources.game_gmod_gray_32 : LoadBitmap("game_gmod_gray_32.png");
  internal static Image GameGMod_32 => IsDesigner() ? Resources.game_gmod_32 : LoadBitmap("game_gmod_32.png");
  internal static Image GameHL2Gray_32 => IsDesigner() ? Resources.game_hl2_gray_32 : LoadBitmap("game_hl2_gray_32.png");
  internal static Image GameHL2_32 => IsDesigner() ? Resources.game_hl2_32 : LoadBitmap("game_hl2_32.png");
  internal static Image HammerGMod_32 => IsDesigner() ? Resources.hammer_gmod_32 : LoadBitmap("hammer_gmod_32.png");
  internal static Image HammerHL2_32 => IsDesigner() ? Resources.hammer_hl2_32 : LoadBitmap("hammer_hl2_32.png");
  internal static Image HammerHPP_32 => IsDesigner() ? Resources.hammer_hpp_32 : LoadBitmap("hammer_hpp_32.png");

  private static bool IsDesigner() {
    if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return true;

    string processName = Process.GetCurrentProcess().ProcessName;
    return processName.Contains("DesignToolsServer", StringComparison.OrdinalIgnoreCase) || processName.Contains("devenv", StringComparison.OrdinalIgnoreCase);
  }

  private static Bitmap LoadBitmap(string fileName) {
    using Stream stream = Open(fileName);
    using Image image = Image.FromStream(stream);
    return new Bitmap(image);
  }

  private static Icon LoadIcon(string fileName) {
    using Stream stream = Open(fileName);
    using Icon icon = new(stream);
    return (Icon)icon.Clone();
  }

  private static Stream Open(string fileName) {
    Stream? stream = Assembly.GetManifestResourceStream(Prefix + fileName);
    return stream is null ? throw new InvalidOperationException($"Resource not found: {Prefix}{fileName}") : stream;
  }
}
