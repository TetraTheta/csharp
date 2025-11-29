using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClearTemp.Libraries;

namespace ClearTemp;

internal static class Program {
  private static void Main() {
    string currentDir = Path.Combine(Environment.CurrentDirectory, "ClearTemp.txt");
    string binaryDir = Path.Combine(AppContext.BaseDirectory, "ClearTemp.txt");

    string configFile;
    if (File.Exists(currentDir)) configFile = Path.GetFullPath(currentDir);
    else if (File.Exists(binaryDir)) configFile = Path.GetFullPath(binaryDir);
    else throw new FileNotFoundException("'ClearTemp.txt' is not found");

    string[] lines = File.ReadAllLines(configFile);
    IEnumerable<ConfigEntry> entries = ConfigParser.Parse(lines);

    foreach (ConfigEntry? entry in entries) {
      IEnumerable<string> matchedDir = PathExpander.Expand(entry.PathPattern).Distinct(StringComparer.OrdinalIgnoreCase);
      foreach (string? dir in matchedDir) {
        Console.WriteLine();
        ConsolePrinter.PrintResult(ResultKind.Info, dir);
        Remover.Process(dir, entry.Patterns, entry.Option);
      }
    }

    Console.WriteLine();
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey(true);
  }
}
