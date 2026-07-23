using System;
using System.Collections.Generic;
using System.Text;

namespace Pathed.Libraries.Option;

public class RemoveOption {
  public string Value { get; init; } = string.Empty;
  public string Name { get; init; } = "PATH";
  public EnvironmentVariableTarget Scope { get; init; } = EnvironmentVariableTarget.User;

  public override string ToString() => $"Name: {Name}\nScope: {Scope}\nValue: {Value}\n";
}
