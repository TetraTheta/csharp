using System.Windows.Forms;

namespace HammerLauncher.Common.WinForm;

public static class TextBoxExtension {
  extension(TextBox textBox) {
    public void SelectEnd() => textBox.Select(textBox.Text.Length, 0);
  }
}
