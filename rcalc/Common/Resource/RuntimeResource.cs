#nullable enable
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;

using R = rcalc.Property.Resource;

namespace rcalc.Common.Resource;

internal static class RuntimeResource {
  private const string Prefix = "rcalc.Resources.";
  private static readonly Assembly Assembly = typeof(RuntimeResource).Assembly;

  internal static Icon MainIcon => IsDesigner() ? R.MainIcon : LoadIcon("MainIcon.ico");

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
