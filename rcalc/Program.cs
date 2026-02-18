using System;
using System.Windows.Forms;
using rcalc.Forms;

namespace rcalc {
internal static class Program {
  [STAThread]
  private static void Main(string[] args) {
    string strX = args.Length >= 1 ? args[0] : "0";
    string strY = args.Length >= 2 ? args[1] : "0";
    string strA = args.Length >= 3 ? args[2] : "0";

    if (!decimal.TryParse(strX, out decimal x)) x = 0;
    if (!decimal.TryParse(strY, out decimal y)) y = 0;
    if (!decimal.TryParse(strA, out decimal a)) a = 0;

    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    Application.SetColorMode(SystemColorMode.Dark);
    Application.Run(new CalculatorForm(x, y, a));
  }
}
}
