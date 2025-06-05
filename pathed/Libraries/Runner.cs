using System;
using System.Security.Principal;

namespace Pathed.Libraries;

public static class Runner {
  /**
   * For slow operation:
   * https://stackoverflow.com/a/18581378
   * https://stackoverflow.com/a/31876740
   *
   * I think reboot is only solution.
   */

  public static int Append(AppendOptions opt) {
    if (opt.Target == EnvironmentVariableTarget.Machine && !IsAdmin()) Elevate();
    if (opt.Target == EnvironmentVariableTarget.Process) {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.Error.WriteLine("You cannot edit environment variable on Process target. It is temporary edit!");
      Console.ResetColor();
      return 1;
    }
    EnvPath envPath = new(opt.Key, opt.Target);
    envPath.Append(opt.Value);
    Environment.SetEnvironmentVariable(opt.Key, envPath.ToString(), opt.Target);
    Console.WriteLine($"\nAppended '{opt.Value}' to '{opt.Key}'.");
    return 0;
  }

  public static int Prepend(PrependOptions opt) {
    if (opt.Target == EnvironmentVariableTarget.Machine && !IsAdmin()) Elevate();
    if (opt.Target == EnvironmentVariableTarget.Process) {
      Console.Error.WriteLine("You cannot edit environment variable on Process target. It is temporary edit!");
      return 1;
    }
    EnvPath envPath = new(opt.Key, opt.Target);
    envPath.Prepend(opt.Value);
    Environment.SetEnvironmentVariable(opt.Key, envPath.ToString(), opt.Target);
    Console.WriteLine($"\nPrepended '{opt.Value}' to '{opt.Key}'.");
    return 0;
  }

  public static int Remove(RemoveOptions opt) {
    if (opt.Target == EnvironmentVariableTarget.Machine && !IsAdmin()) Elevate();
    if (opt.Target == EnvironmentVariableTarget.Process) {
      Console.Error.WriteLine("You cannot edit environment variable on Process target. It is temporary edit!");
      return 1;
    }
    EnvPath envPath = new(opt.Key, opt.Target);
    envPath.Remove(opt.Value);
    Environment.SetEnvironmentVariable(opt.Key, envPath.ToString(), opt.Target);
    Console.WriteLine($"\nRemoved '{opt.Value}' from '{opt.Key}'.");
    return 0;
  }

  public static int Show(ShowOptions opt) {
    EnvPath envPath = new(opt.Key, opt.Target);
    envPath.Show();
    return 0;
  }

  public static int Slim(SlimOptions opt) {
    if (opt.Target == EnvironmentVariableTarget.Machine && !IsAdmin()) Elevate();
    if (opt.Target == EnvironmentVariableTarget.Process) {
      Console.Error.WriteLine("You cannot edit environment variable on Process target. It is temporary edit!");
      return 1;
    }
    EnvPath envPath = new(opt.Key, opt.Target);
    envPath.Slim();
    Environment.SetEnvironmentVariable(opt.Key, envPath.ToString(), opt.Target);
    Console.WriteLine($"\nSlimmed '{opt.Key}'.");
    return 0;
  }

  public static int Sort(SortOptions opt) {
    if (opt.Target == EnvironmentVariableTarget.Machine && !IsAdmin()) Elevate();
    if (opt.Target == EnvironmentVariableTarget.Process) {
      Console.Error.WriteLine("You cannot edit environment variable on Process target. It is temporary edit!");
      return 1;
    }
    EnvPath envPath = new(opt.Key, opt.Target);
    envPath.Sort();
    Environment.SetEnvironmentVariable(opt.Key, envPath.ToString(), opt.Target);
    Console.WriteLine($"\nSorted '{opt.Key}'.");
    return 0;
  }

  // This doesn't actually create elevated process, but I'll use this name for later use.
  private static void Elevate() {
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Error.WriteLine("Administrator privilege is required.");
    Console.ResetColor();
    Environment.Exit(1);
  }
  private static bool IsAdmin() => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
  /**
   * TODO:
   * 1. When 'Elevate()' is called, create new named pipe with random name
   * 2. Start elevated process with additional '--pipe PIPE_NAME' argument
   * 3. Parent process will listen to that named pipe, and elevated child process will send message to the named pipe
   * 4. Parent process will print message to console
   */
}
