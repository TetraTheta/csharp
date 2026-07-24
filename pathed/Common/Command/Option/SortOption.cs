using System;

namespace Pathed.Common.Command.Option;

public class SortOption {
  public string Name { get; init; } = "PATH";
  public EnvironmentVariableTarget Scope { get; init; } = EnvironmentVariableTarget.User;

  public override string ToString() => $"Name: {Name}\nScope: {Scope}\n";
}
