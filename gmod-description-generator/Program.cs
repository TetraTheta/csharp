using System;
using System.Windows.Forms;

using GModDescriptionGenerator.Forms;

namespace GModDescriptionGenerator;

internal static class Program {
  [STAThread]
  private static void Main() {
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    Application.SetColorMode(SystemColorMode.Dark);
    Application.Run(new MainForm());
  }
}
