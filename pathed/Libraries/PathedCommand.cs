using System;
using System.Text;

using ConsoleAppFramework;

using Pathed.Libraries.Option;

namespace Pathed.Libraries;

public class PathedCommand {
  /// <summary>Append variable to environment variable</summary>
  /// <param name="value">New variable to add to the end of the environment variable</param>
  /// <param name="name">-n, Name of the environment variable</param>
  /// <param name="scope">-s, Target environment variable scope</param>
  [Command("append")]
  public int Append(
    [Argument] string value,
    string name = "PATH",
    EnvironmentVariableTarget scope = EnvironmentVariableTarget.User
  ) {
    var opt = new AppendOption { Value = value, Name = name, Scope = scope };
    return Runner.Append(opt);
  }

  /// <summary>Prepend variable to environment variable</summary>
  /// <param name="value">New variable to add to the front of the environment variable</param>
  /// <param name="name">-n, Name of the environment variable</param>
  /// <param name="scope">-s, Target environment variable scope</param>
  [Command("prepend")]
  public int Prepend(
    [Argument] string value,
    string name = "PATH",
    EnvironmentVariableTarget scope = EnvironmentVariableTarget.User
  ) {
    var opt = new PrependOption { Value = value, Name = name, Scope = scope };
    return Runner.Prepend(opt);
  }

  /// <summary>Remove variable from environment variable</summary>
  /// <param name="value">Variable to remove from the environment variable</param>
  /// <param name="name">-n, Name of the environment variable</param>
  /// <param name="scope">-s, Target environment variable scope</param>
  [Command("remove")]
  public int Remove(
    [Argument] string value,
    string name = "PATH",
    EnvironmentVariableTarget scope = EnvironmentVariableTarget.User
  ) {
    var opt = new RemoveOption { Value = value, Name = name, Scope = scope };
    return Runner.Remove(opt);
  }

  /// <summary>Print environment variable as a list</summary>
  /// <param name="name">-n, Name of the environment variable</param>
  /// <param name="scope">-t, Target environment variable scope</param>
  [Command("show")]
  public int Show(
    string name = "PATH",
    EnvironmentVariableTarget scope = EnvironmentVariableTarget.User
  ) {
    var opt = new ShowOption { Name = name, Scope = scope };
    return Runner.Show(opt);
  }

  /// <summary>Remove duplicated or non-existent variable from environment variable</summary>
  /// <param name="name">-n, Name of the environment variable</param>
  /// <param name="scope">-s, Target environment variable scope</param>
  [Command("slim")]
  public int Slim(
    string name = "PATH",
    EnvironmentVariableTarget scope = EnvironmentVariableTarget.User
  ) {
    var opt = new SlimOption { Name = name, Scope = scope };
    return Runner.Slim(opt);
  }

  /// <summary>Sort environment variable alphabetically</summary>
  /// <param name="name">-n, Name of the environment variable</param>
  /// <param name="scope">-s, Target environment variable scope</param>
  [Command("sort")]
  public int Sort(
    string name = "PATH",
    EnvironmentVariableTarget scope = EnvironmentVariableTarget.User
  ) {
    var opt = new SortOption { Name = name, Scope = scope };
    return Runner.Sort(opt);
  }
}
