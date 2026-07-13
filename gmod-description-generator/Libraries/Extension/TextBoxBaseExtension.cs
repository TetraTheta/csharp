using System.Linq;
using System.Windows.Forms;

namespace GModDescriptionGenerator.Libraries.Extension;

public static class TextBoxBaseExtension {
  // 'extension(TextBoxBase tb)' is available from C# 14 (.NET 10)
  extension(TextBoxBase tb) {
    public string[] TrimmedLines => tb.Lines.Where(line => !string.IsNullOrWhiteSpace(line)).Select(line => line.Trim()).ToArray();
  }
}
