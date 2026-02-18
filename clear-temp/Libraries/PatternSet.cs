using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ClearTemp.Libraries {
public sealed class PatternSet {
  private readonly List<string> _negatives;
  private readonly List<string> _positives;

  private PatternSet(List<string> positives, List<string> negatives) {
    _positives = positives;
    _negatives = negatives;
  }

  public static PatternSet Parse(string pattern) {
    if (string.IsNullOrWhiteSpace(pattern)) pattern = "*";

    string[] tokens = pattern.Split([';'], StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).Where(t => t.Length > 0).ToArray();

    var pos = new List<string>(tokens.Length);
    var neg = new List<string>(tokens.Length);

    foreach (string t in tokens) {
      if (t.StartsWith('!')) {
        string sub = t[1..].Trim();
        if (sub.Length > 0) neg.Add(sub);
      } else { pos.Add(t); }
    }

    if (pos.Count == 0) pos.Add("*");

    return new PatternSet(pos, neg);
  }

  public bool IsMatch(string path) {
    string filename = Path.GetFileName(path);
    string filenameWithoutExt = Path.GetFileNameWithoutExtension(path);

    bool positiveMatch = _positives.Any(p => WildcardMatches(filename, filenameWithoutExt, p));
    if (!positiveMatch) return false;

    bool negativeMatch = _negatives.Any(n => WildcardMatches(filename, filenameWithoutExt, n));
    return !negativeMatch;
  }

  private static bool WildcardMatches(string filename, string filenameWithoutExt, string pattern) {
    if (!pattern.Contains('*')) { return string.Equals(pattern.Contains('.') ? filename : filenameWithoutExt, pattern, StringComparison.OrdinalIgnoreCase); }

    string rx = WildcardToRegex(pattern);
    return Regex.IsMatch(filename, rx, RegexOptions.IgnoreCase);
  }

  private static string WildcardToRegex(string pattern) {
    string esc = Regex.Escape(pattern);
    esc = esc.Replace(@"\*", ".*");
    return "^" + esc + "$";
  }
}
}
