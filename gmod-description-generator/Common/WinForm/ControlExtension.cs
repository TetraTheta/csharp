#nullable enable
using System.Windows.Forms;

namespace GModDescriptionGenerator.Common.WinForm;

public static class ControlExtension {
  // 'extension(Control ctrl)' is available from C# 14 (.NET 10)
  extension(Control? ctrl) {
    public string TrimmedText => ctrl?.Text?.Trim() ?? string.Empty;
  }
}
