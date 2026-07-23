using System;
using System.IO;

namespace ClearTemp.Libraries;

public static class Remover {
  public static void Process(string path, PatternSet patterns, RemoveOption option) {
    if (!Directory.Exists(path)) return;

    switch (option) {
      case RemoveOption.None:
        DeleteFilesInDirectory(path, patterns);
        break;
      case RemoveOption.Recurse:
        ClearRecursive(path, patterns, false);
        break;
      case RemoveOption.RemoveSelf:
        ClearRecursive(path, patterns, true);
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(option), option, null);
    }
  }

  private static void DeleteFilesInDirectory(string path, PatternSet patterns) {
    foreach (string file in SafeIo.EnumerateFiles(path)) {
      try {
        if (!patterns.IsMatch(file)) continue;
        File.Delete(file);
        ConsolePrinter.PrintResult(ResultKind.Success, file);
      } catch (Exception e) {
        ConsolePrinter.PrintException(e, file);
      }
    }
  }

  private static void ClearRecursive(string path, PatternSet patterns, bool deleteThisDir) {
    // Delete file in current dir
    foreach (string file in SafeIo.EnumerateFiles(path)) {
      try {
        if (!patterns.IsMatch(file)) continue;
        File.Delete(file);
        ConsolePrinter.PrintResult(ResultKind.Success, file);
      } catch (Exception e) {
        ConsolePrinter.PrintException(e, file);
      }
    }

    // Recurse into subdirectories
    foreach (string sub in SafeIo.EnumerateDirectories(path)) {
      try {
        ClearRecursive(sub, patterns, deleteThisDir);
      } catch {
        // ignored
      }
    }

    if (!deleteThisDir) return;
    try {
      Directory.Delete(path);
      ConsolePrinter.PrintResult(ResultKind.Success, path);
    } catch (Exception e) {
      ConsolePrinter.PrintException(e, path);
    }
  }
}
