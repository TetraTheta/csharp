using System;

namespace TrayHotkey;

internal class HotkeyEventArgs(int hotkeyId) : EventArgs {
  public int HotkeyId { get; } = hotkeyId;
}
