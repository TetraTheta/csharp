using MarkdownHelper.Forms;
using System;
using System.Windows.Forms;
using TrayHotkey;

namespace MarkdownHelper;

internal static class Program {
  [STAThread]
  private static void Main() {
    SingleInstance.Ensure();

    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);

    // TODO: Create more complex context menu
    ContextMenu menu = new([
      new MenuItem("Exit2", ExitApp) { DefaultItem = true }
    ]);

    TrayApp app = new("Markdown Helper", Resources.Resources.MainIcon, menu);

    app.RegisterHotkey(Modifiers.Control | Modifiers.Alt, Keys.N, () => new NewPostForm().ShowAndActivate());

    Application.Run(app);
  }

  private static void ExitApp(object? sender, EventArgs e) {
    Application.ExitThread();
    Application.Exit();
  }
}
