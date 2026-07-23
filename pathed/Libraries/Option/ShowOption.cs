using System;
using System.Collections.Generic;
using System.Text;

namespace Pathed.Libraries.Option;

public class ShowOption {
  public string Name { get; init; } = "PATH";
  public EnvironmentVariableTarget Scope { get; init; } = EnvironmentVariableTarget.User;

  public override string ToString() => $"Name: {Name}\nScope: {Scope}\n";
}
