using System.Windows.Forms;

namespace HammerLauncher.Libraries {
public static class TextBoxExtension {
  public static void SelectEnd(this TextBox textBox) {
    textBox.Select(textBox.Text.Length, 0);
  }
}
}
