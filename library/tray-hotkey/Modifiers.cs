using System;

namespace TrayHotkey;
[Flags]
public enum Modifiers : uint {
  None = 0,
  Alt = 1,
  Control = 2,
  Shift = 4,
  Win = 8
}
