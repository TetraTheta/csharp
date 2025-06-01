using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TrayHotkey;
internal class HotkeyWindow : NativeWindow, IDisposable {
  private const int WM_HOTKEY = 0x0312;
  private static int nextId = 0;

  [DllImport("user32.dll", SetLastError = true, EntryPoint = "RegisterHotKey")]
  private static extern bool _RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

  [DllImport("user32.dll", SetLastError = true, EntryPoint = "UnregisterHotKey")]
  private static extern bool _UnregisterHotKey(IntPtr hWnd, int id);

  public event EventHandler<HotkeyEventArgs> HotkeyPressed = delegate { };

  public HotkeyWindow(string name) {
    CreateHandle(new CreateParams() {
      Caption = name + "_TrayHotkeyWindow",
      X = 0, Y = 0, Height = 0, Width = 0,
      Style = 0x800000 // WS_OVERLAPPED
    });
  }

  public int TryRegisterHotkey(Modifiers mods, Keys key) {
    int id = nextId + 1;
    if (!_RegisterHotKey(Handle, id, (uint)mods, (uint)key)) {
      int err = Marshal.GetLastWin32Error();
      MessageBox.Show($"RegisterHotKey failed: hr=0x{err:X}", "TrayHotkey Error");
      return -1;
    }
    nextId = id;
    return id;
  }

  protected override void WndProc(ref Message m) {
    if (m.Msg == WM_HOTKEY) {
      int id = m.WParam.ToInt32();
      HotkeyPressed?.Invoke(this, new HotkeyEventArgs(id));
    }
    base.WndProc(ref m);
  }

  public void Dispose() {
    for (int i = 1; i <= nextId; i++) _UnregisterHotKey(Handle, i);
    DestroyHandle();
  }
}
