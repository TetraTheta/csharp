using System;

namespace Launcher.Libraries.Help;

[Flags]
public enum HelpStep {
  None = 0,
  Game = 1,
  Operation = 2,
  Target = 4,
  All = Game | Operation | Target,
}
