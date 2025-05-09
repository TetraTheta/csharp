using System;
using System.Collections.Generic;
using AliasEnum;
using Option;
using Option.Enums;

namespace Launcher.Libraries;

public static class ArgumentParser {
  private static readonly HashSet<string> Flags = new(StringComparer.OrdinalIgnoreCase) { "--english", "--help", "--verbose", "--version", "-h", "-v" };

  public static ParsedOption Parse(string[] args) {
    // internal
    int currentStep = 0;
    bool skipSwitch = false;
    // option
    Game? game = null;
    Operation? operation = null;
    string? target = null;
    bool english = false, help = false, verbose = false, version = false;

    foreach (string arg in args) {
      if (!skipSwitch && arg.Equals("--")) {
        skipSwitch = true;
        continue;
      }

      if (!skipSwitch && Flags.Contains(arg)) {
        switch (arg.ToLower()) {
          case "--english":
            english = true;
            break;

          case "-h":
          case "--help":
            help = true;
            break; // Show help message for every positional arguments
          case "--verbose":
            verbose = true;
            break;

          case "-v":
          case "--version":
            version = true;
            break;
        }
        continue;
      }

      switch (currentStep) {
        case 0:
          game = AliasEnumHelper.ParseEnum<Game>(arg);
          if (game != null) currentStep++;
          else {
            game = null;
            operation = null;
            target = string.Empty;
            //help = true;
          }
          continue;
        case 1:
          operation = AliasEnumHelper.ParseEnum<Operation>(arg);
          if (operation != null) currentStep++;
          else {
            operation = null;
            target = string.Empty;
            //help = true;
          }
          continue;
        case 2:
          target = arg;
          currentStep++;
          continue;
      }
    }
    return new ParsedOption(game, operation, target, english, help, verbose, version);
  }
}
