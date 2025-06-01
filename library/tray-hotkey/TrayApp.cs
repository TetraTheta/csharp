using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace TrayHotkey;

public class TrayApp : ApplicationContext {
  private readonly NotifyIcon trayIcon;
  private readonly HotkeyWindow HW;
  private readonly Dictionary<int, Action> callbacks = [];

  public TrayApp(string tooltip, Icon? icon, ContextMenu? contextMenu) {
    trayIcon = new() {
      Icon = icon ?? SystemIcons.Application,
      Text = tooltip ?? string.Empty,
      ContextMenu = contextMenu ?? new([
        new MenuItem("Exit", OnExit)
      ]),
      Visible = true
    };

    HW = new HotkeyWindow(Assembly.GetEntryAssembly()!.GetName().Name!);
    HW.HotkeyPressed += OnHotkeyPressed;
  }

  public int RegisterHotkey(Modifiers mods, Keys key, Action callback) {
    int id = HW.TryRegisterHotkey(mods, key);
    if (id > 0) callbacks[id] = callback;
    return id;
  }

  public void OnExit(object? sender, EventArgs e) => ExitThread();

  private void OnHotkeyPressed(object? sender, HotkeyEventArgs e) {
    if (callbacks.TryGetValue(e.HotkeyId, out Action action)) {
      try { action(); } catch (Exception ex) { MessageBox.Show(ex.ToString(), ex.GetType().FullName); } 
    }
  }

  protected override void Dispose(bool disposing) {
    if (disposing) {
      try { HW.Dispose(); } catch { }
      try { trayIcon.Visible = false; } catch { }
      try { trayIcon.Dispose(); } catch { }
    }
    base.Dispose(disposing);
  }
}
