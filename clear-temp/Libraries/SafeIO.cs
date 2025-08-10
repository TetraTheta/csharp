using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ClearTemp.Libraries;

public static class SafeIO {
  public static IEnumerable<string> EnumerateFiles(string path) {
    try {
      return Directory.EnumerateFiles(path).ToArray();
    } catch {
      return [];
    }
  }

  public static IEnumerable<string> EnumerateDirectories(string path) {
    try {
      return Directory.EnumerateDirectories(path).ToArray();
    } catch {
      return [];
    }
  }

  public static IEnumerable<string> EnumerateDirectoriesWithPattern(string path, string searchPattern) {
    try {
      return Directory.EnumerateDirectories(path, searchPattern).ToArray();
    } catch {
      return [];
    }
  }
}
