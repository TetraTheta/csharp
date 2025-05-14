using Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ClearTemp;

internal class Program {
  private enum Result {
    SUCCESS,
    LOCKED,
    NOPERM
  }

  private static void Main() {
    // Get absolute path of 'ClearTemp.txt'
    string currentDir = Path.Combine(Environment.CurrentDirectory, "ClearTemp.txt");
    string binaryDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ClearTemp.txt");

    string configFile;
    if (File.Exists(currentDir)) {
      configFile = Path.GetFullPath(currentDir);
    } else if (File.Exists(binaryDir)) {
      configFile = Path.GetFullPath(binaryDir);
    } else {
      throw new FileNotFoundException("'ClearTemp.txt' is not found");
    }

    List<string> list = [.. TextConfig.ParseSinglePath(configFile)
      .Select(path => Environment.ExpandEnvironmentVariables(path))
      .Select(path => Path.GetFullPath(path))
      .Distinct(StringComparer.OrdinalIgnoreCase)];

    list.ForEach(path => ClearDirectory(path, false));

    Console.WriteLine();
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey(true);
  }

  private static void ClearDirectory(string path, bool deleteSelf = true) {
    // Delete all files in current directory. Catch per-file exceptions.
    foreach (var file in SafeEnumerateFiles(path)) {
      try {
        File.Delete(file);
        PrintResult(Result.SUCCESS, file);
      } catch (Exception ex) {
        if (ex is IOException) {
          PrintResult(Result.LOCKED, file);
        } else if (ex is UnauthorizedAccessException) {
          PrintResult(Result.NOPERM, file);
        } else {
          Console.WriteLine($"      {ex.GetType().FullName}");
          Console.WriteLine($"      {ex.Message}");
        }
      }
    }

    // Recurse into each sub-directories
    foreach (var dir in SafeEnumerateDirectories(path)) {
      ClearDirectory(dir, true);
    }

    // Attempt to delete directory itself
    if (deleteSelf) {
      try {
        Directory.Delete(path);
        PrintResult(Result.SUCCESS, path);
      } catch (Exception ex) {
        if (ex is IOException) {
          PrintResult(Result.LOCKED, path);
        } else if (ex is UnauthorizedAccessException) {
          PrintResult(Result.NOPERM, path);
        } else {
          Console.WriteLine($"      {ex.GetType().FullName}");
          Console.WriteLine($"      {ex.Message}");
        }
      }
    }
  }

  private static IEnumerable<string> SafeEnumerateFiles(string path) {
    try {
      return Directory.EnumerateFiles(path);
    } catch {
      return [];
    }
  }

  private static IEnumerable<string> SafeEnumerateDirectories(string path) {
    try {
      return Directory.EnumerateDirectories(path);
    } catch {
      return [];
    }
  }

  private static void PrintResult(Result result, string path) {
    string name = Path.GetFileName(path) ?? path;
    string prefix = Path.GetDirectoryName(path) ?? string.Empty;

    ConsoleColor color;
    string action;
    switch (result) {
      case Result.SUCCESS:
        action = "DELETED ";
        color = ConsoleColor.Green;
        break;

      case Result.LOCKED:
        action = "LOCKED  ";
        color = ConsoleColor.Red;
        break;

      case Result.NOPERM:
        action = "NOPERM  ";
        color = ConsoleColor.Red;
        break;

      default:
        throw new NotImplementedException();
    }

    Console.ForegroundColor = color;
    Console.Write(action);
    Console.ResetColor();

    if (!string.IsNullOrEmpty(prefix)) Console.Write(prefix + Path.DirectorySeparatorChar);

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(name);
    Console.ResetColor();
  }
}
