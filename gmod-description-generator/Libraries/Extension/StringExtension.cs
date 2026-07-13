#nullable enable
using System;

namespace GModDescriptionGenerator.Libraries.Extension;

public static class StringExtension {
  // 'extension(string str)' is available from C# 14 (.NET 10)
  extension(string? str) {
    public string NativeLine => string.IsNullOrEmpty(str) ? string.Empty : str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
    public string UnixLine => string.IsNullOrEmpty(str) ? string.Empty : str.Replace("\r\n", "\n").Replace("\r", "\n");
  }
}
