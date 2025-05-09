using HammerLauncher.Forms;
using System;
using System.Windows.Forms;

namespace HammerLauncher;

internal static class Program {
  [STAThread]
  private static void Main(string[] args) {
    // Use first argument as 'file'
    string file = args.Length > 0 ? args[0] : string.Empty;

    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    Application.Run(new Launcher(file));
  }
}
