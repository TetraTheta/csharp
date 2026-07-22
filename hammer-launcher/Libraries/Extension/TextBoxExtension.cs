using System.Windows.Forms;

namespace HammerLauncher.Libraries.Extension;

public static class TextBoxExtension {
  extension(TextBox textBox) {
    public void SelectEnd() => textBox.Select(textBox.Text.Length, 0);
  }
}
