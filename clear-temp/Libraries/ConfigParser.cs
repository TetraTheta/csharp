using System;
using System.Collections.Generic;
using System.IO;

namespace ClearTemp.Libraries;

public static class ConfigParser {
  public static IEnumerable<ConfigEntry> Parse(string[] lines) {
    foreach (string raw in lines) {
      string line = raw.Trim();
      if (string.IsNullOrWhiteSpace(raw) || line.StartsWith(";") || line.StartsWith("#")) continue;

      string[] parts = line.Split(['|'], 3);
      if (parts.Length == 0) continue;

      string rawPath = parts[0].Trim();
      if (string.IsNullOrWhiteSpace(rawPath)) continue;
      rawPath = Environment.ExpandEnvironmentVariables(rawPath).Replace('/', Path.DirectorySeparatorChar);

      string partPattern = parts.Length >= 2 ? parts[1].Trim() : "*";
      string partOption = parts.Length == 3 ? parts[2].Trim().ToUpperInvariant() : "NONE";

      PatternSet patterns = PatternSet.Parse(partPattern);

      RemoveOption option;
      if (string.Equals(partOption, "RECURSE", StringComparison.OrdinalIgnoreCase)) {
        option = RemoveOption.Recurse;
      } else if (string.Equals(partOption, "REMOVESELF", StringComparison.OrdinalIgnoreCase)) {
        option = RemoveOption.RemoveSelf;
      } else {
        option = RemoveOption.None;
      }

      yield return new ConfigEntry(rawPath, patterns, option);
    }
  }
}
