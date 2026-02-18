using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Configuration.Libraries;

namespace Configuration {
public class Ini {
  private static readonly NaturalStringComparer Comparer = new();
  private readonly SortedDictionary<string, SortedDictionary<string, string>> _data;
  private readonly string _filePath;

  public Ini(string iniPath) {
    string baseDir = AppContext.BaseDirectory;
    _filePath = Path.Combine(baseDir, iniPath);

    _data = new SortedDictionary<string, SortedDictionary<string, string>>(Comparer);

    if (!File.Exists(_filePath)) {
      Directory.CreateDirectory(Path.GetDirectoryName(_filePath) ?? baseDir);
      using (File.Create(_filePath)) {
        // empty
      }
    }

    LoadFromFile();
  }

  public string Get(string section, string key, string defaultValue) {
    if (!_data.TryGetValue(section, out SortedDictionary<string, string>? sectionDict)) {
      if (defaultValue == null) throw new MissingFieldException($"There is no default value of '{section}/{key}'");
      _data[section] = new SortedDictionary<string, string>(Comparer) { [key] = defaultValue };
      WriteToFile();
      return defaultValue;
    }

    if (sectionDict.TryGetValue(key, out string? value)) return value;
    sectionDict[key] = defaultValue ?? throw new MissingFieldException($"There is no default value of '{section}/{key}'");
    WriteToFile();
    return defaultValue;
  }

  public void Set(string section, string key, string value) {
    if (!_data.ContainsKey(section)) _data[section] = new SortedDictionary<string, string>(Comparer);
    _data[section][key] = value;
    WriteToFile();
  }

  public override string ToString() {
    var sb = new StringBuilder();
    foreach ((string sectionName, SortedDictionary<string, string> value) in _data) {
      sb.AppendLine(sectionName);

      int count = value.Count;
      int i = 0;
      foreach (KeyValuePair<string, string> kv in value) {
        bool isLast = (++i == count);
        string prefix = isLast ? "└" : "├";
        sb.AppendLine($"{prefix} {kv.Key} = {kv.Value}");
      }
    }

    if (sb.Length > 0 && sb[^1] == '\n') sb.Length--;
    return sb.ToString();
  }

  private void LoadFromFile() {
    string? currentSection = null;
    foreach (string rawLine in File.ReadAllLines(_filePath, Encoding.UTF8)) {
      string line = rawLine.Trim();
      if (string.IsNullOrEmpty(line) || line.StartsWith(';') || line.StartsWith('#')) continue;
      if (line.StartsWith('[') && line.EndsWith(']')) {
        currentSection = line.Substring(1, line.Length - 2).Trim();
        if (!_data.ContainsKey(currentSection)) { _data[currentSection] = new SortedDictionary<string, string>(Comparer); }
      } else if (currentSection != null && line.Contains('=')) {
        int idx = line.IndexOf('=');
        string key = line[..idx].Trim();
        string value = line[(idx + 1)..].Trim();
        _data[currentSection][key] = value;
      }
    }
  }

  private void WriteToFile() {
    using var writer = new StreamWriter(_filePath, false, Encoding.UTF8);
    foreach (KeyValuePair<string, SortedDictionary<string, string>> sectionPair in _data) {
      string sectionName = sectionPair.Key;
      writer.WriteLine($"[{sectionName}]");
      foreach (KeyValuePair<string, string> keyPair in sectionPair.Value) { writer.WriteLine($"{keyPair.Key}={keyPair.Value}"); }
    }
  }
}
}
