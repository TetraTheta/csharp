using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TrayHotkey;
public static class FormExtension {
  [DllImport("user32.dll")]
  static extern bool SetForegroundWindow(IntPtr hWnd);

  [DllImport("user32.dll")]
  static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
  const int SW_SHOWNORMAL = 1;

  public static void ShowAndActivate(this Form form) {
    form.Show();
    form.WindowState = FormWindowState.Normal;
    form.BringToFront();
    ShowWindowAsync(form.Handle, SW_SHOWNORMAL);
    SetForegroundWindow(form.Handle);
  }
}
