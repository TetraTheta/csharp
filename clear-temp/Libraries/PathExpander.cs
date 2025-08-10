using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ClearTemp.Libraries;

public static class PathExpander {
  private static readonly char Sep = Path.DirectorySeparatorChar;

  public static IEnumerable<string> Expand(string pathPattern) {
    if (string.IsNullOrWhiteSpace(pathPattern)) yield break;

    string norm = pathPattern.Trim().Replace('/', Sep);

    string root = Path.GetPathRoot(norm);
    if (string.IsNullOrWhiteSpace(root)) root = Path.GetFullPath(Environment.CurrentDirectory);

    string rest = norm.Substring(root.Length).Trim(Sep);

    string[] segments = rest.Length == 0 ? [] : rest.Split(Sep);
    List<string> currentBases = new() { root.TrimEnd(Sep) + Sep };

    foreach (string seg in segments) {
      List<string> next = new();
      foreach (string? baseDir in currentBases) {
        try {
          if (seg.Contains("*")) {
            next.AddRange(SafeIO.EnumerateDirectoriesWithPattern(baseDir, seg));
          } else {
            string combined = Path.Combine(baseDir, seg);
            if (Directory.Exists(combined)) next.Add(combined);
          }
        } catch {
          // ignored
        }
      }

      if (next.Count == 0) yield break;
      currentBases = next;
    }

    if (segments.Length == 0) {
      if (Directory.Exists(currentBases[0])) yield return currentBases[0];
      yield break;
    }

    foreach (string? d in currentBases.Where(Directory.Exists)) {
      yield return d;
    }
  }
}
