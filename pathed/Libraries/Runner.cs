using System;
using System.Security.Principal;

namespace Pathed.Libraries {
public static class Runner {
  /*
   * For slow operation:
   * https://stackoverflow.com/a/18581378
   * https://stackoverflow.com/a/31876740
   *
   * I think reboot is only solution.
   */
  private static int ExecuteMutation(string key, EnvironmentVariableTarget target, Func<EnvPath, int> action, string message) {
    // ReSharper disable once ConvertIfStatementToSwitchStatement
    if (target == EnvironmentVariableTarget.Process) {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.Error.WriteLine("You cannot edit environment variable on Process target. It is temporary edit!");
      Console.ResetColor();
      return 1;
    }

    if (target == EnvironmentVariableTarget.Machine && !IsAdmin()) {
      Elevate();
      return 1;
    }

    var envPath = new EnvPath(key, target);
    int result = action(envPath);
    if (result != 0) return result;

    Environment.SetEnvironmentVariable(key, envPath.ToString(), target);
    Console.WriteLine("\n" + message);
    return 0;
  }

  public static int Append(AppendOptions opt) => ExecuteMutation(opt.Key, opt.Target, p => p.Append(opt.Value), $"Appended '{opt.Value}' to '{opt.Key}'.");

  public static int Prepend(PrependOptions opt) => ExecuteMutation(opt.Key, opt.Target, p => p.Prepend(opt.Value), $"Prepended '{opt.Value}' to '{opt.Key}'.");

  public static int Remove(RemoveOptions opt) => ExecuteMutation(opt.Key, opt.Target, p => p.Remove(opt.Value), $"Removed '{opt.Value}' from '{opt.Key}'.");

  public static int Show(ShowOptions opt) {
    EnvPath envPath = new EnvPath(opt.Key, opt.Target);
    envPath.Show();
    return 0;
  }

  public static int Slim(SlimOptions opt) => ExecuteMutation(opt.Key, opt.Target, p => p.Slim(), $"Slimmed '{opt.Key}'.");

  public static int Sort(SortOptions opt) => ExecuteMutation(opt.Key, opt.Target, p => p.Sort(), $"Sorted '{opt.Key}'.");

  // This doesn't actually create elevated process, but I'll use this name for later use.
  private static void Elevate() {
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Error.WriteLine("Administrator privilege is required.");
    Console.ResetColor();
    Environment.Exit(1);
  }

  private static bool IsAdmin() => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
  /*
   * TODO:
   * 1. When 'Elevate()' is called, create new named pipe with random name
   * 2. Start elevated process with additional '--pipe PIPE_NAME' argument
   * 3. Parent process will listen to that named pipe, and elevated child process will send message to the named pipe
   * 4. Parent process will print message to console
   */
}
}
