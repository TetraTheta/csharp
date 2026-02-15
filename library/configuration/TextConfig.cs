using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Configuration {
  public class TextConfig {
    public static ProcessStartInfo[] ParseCommandLine(string path) {
      if (!File.Exists(path)) throw new FileNotFoundException("Config file not found", path);

      string[] lines = File.ReadAllLines(path);
      var list = new List<ProcessStartInfo>();

      foreach (string raw in lines.Where(l => !string.IsNullOrWhiteSpace(l))) {
        (string exePath, string args) = TokenizeCommandLine(raw);

        if (!Path.IsPathRooted(exePath) || exePath.IndexOfAny(Path.GetInvalidPathChars()) >= 0) throw new InvalidDataException($"Invalid path for executable: {exePath}");

        var psi = new ProcessStartInfo() {
          FileName = exePath,
          Arguments = args,
          UseShellExecute = true,
          WorkingDirectory = Path.GetDirectoryName(exePath) ?? Path.GetPathRoot(exePath)
        };

        list.Add(psi);
      }

      return list.ToArray();
    }

    public static string[] ParseSinglePath(string path) {
      if (!File.Exists(path)) throw new FileNotFoundException("Config file not found", path);

      string[] lines = File.ReadAllLines(path);
      var list = new List<string>();

      foreach (string raw in lines.Where(l => !string.IsNullOrWhiteSpace(l))) {
        string trimmed = raw.Trim();
        if (trimmed.StartsWith("\"") && trimmed.EndsWith("\"")) trimmed = trimmed.Substring(1, trimmed.Length - 2);

        if (trimmed.IndexOfAny(Path.GetInvalidPathChars()) >= 0) throw new InvalidDataException($"Invalid path: {trimmed}");

        list.Add(trimmed);
      }

      return list.ToArray();
    }

    private static (string exePath, string arguments) TokenizeCommandLine(string commandLine) {
      const string pattern = @"\s*(?:""(?<quoted>[^""]*)""|(?<token>[^ \t""]+))";
      MatchCollection matches = Regex.Matches(commandLine, pattern);
      if (matches.Count == 0) return (string.Empty, string.Empty);

      string first = matches[0].Groups["quoted"].Success ? matches[0].Groups["quoted"].Value : matches[0].Groups["token"].Value;
      string args = commandLine.Remove(0, matches[0].Length).Trim();

      return (first, args);
    }
  }
}
