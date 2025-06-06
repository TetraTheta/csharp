using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Configuration.Libraries;

namespace Configuration;

public class INI {
  private static readonly NaturalStringComparer _nc = new();
  private readonly SortedDictionary<string, SortedDictionary<string, string>> _data;
  private readonly string _filePath;

  public INI(string iniPath) {
    string baseDir = AppContext.BaseDirectory;
    _filePath = Path.Combine(baseDir, iniPath);

    _data = new(_nc);

    if (!File.Exists(_filePath)) {
      Directory.CreateDirectory(Path.GetDirectoryName(_filePath) ?? baseDir);
      using (File.Create(_filePath)) { }
    }

    LoadFromFile();
  }

  public string Get(string section, string key, string? defaultValue) {
    if (!_data.ContainsKey(section)) {
      if (defaultValue == null) throw new MissingFieldException($"There is no default value of '{section}/{key}'");
      _data[section] = new(_nc) {
        [key] = defaultValue
      };
      WriteToFile();
      return defaultValue;
    }

    var sectionDict = _data[section];
    if (!sectionDict.ContainsKey(key)) {
      if (defaultValue == null) throw new MissingFieldException($"There is no default value of '{section}/{key}'");
      sectionDict[key] = defaultValue;
      WriteToFile();
      return defaultValue;
    }

    return sectionDict[key];
  }

  public void Set(string section, string key, string value) {
    if (!_data.ContainsKey(section)) _data[section] = new(_nc);
    _data[section][key] = value;
    WriteToFile();
  }

  public override string ToString() {
    StringBuilder sb = new();
    foreach (var sectionPair in _data) {
      string sectionName = sectionPair.Key;
      sb.AppendLine(sectionName);

      int count = sectionPair.Value.Count;
      int i = 0;
      foreach (var kv in sectionPair.Value) {
        bool isLast = (++i == count);
        string prefix = isLast ? "└" : "├";
        sb.AppendLine($"{prefix} {kv.Key} = {kv.Value}");
      }
    }
    if (sb.Length > 0 && sb[sb.Length - 1] == '\n') sb.Length--;
    return sb.ToString();
  }

  private void LoadFromFile() {
    string? currentSection = null;
    foreach (var rawLine in File.ReadAllLines(_filePath, Encoding.UTF8)) {
      string line = rawLine.Trim();
      if (string.IsNullOrEmpty(line) || line.StartsWith(";") || line.StartsWith("#")) continue;
      if (line.StartsWith("[") && line.EndsWith("]")) {
        currentSection = line.Substring(1, line.Length - 2).Trim();
        if (!_data.ContainsKey(currentSection)) {
          _data[currentSection] = new(_nc);
        }
      } else if (currentSection != null && line.Contains("=")) {
        int idx = line.IndexOf('=');
        string key = line.Substring(0, idx).Trim();
        string value = line.Substring(idx + 1).Trim();
        _data[currentSection][key] = value;
      }
    }
  }

  private void WriteToFile() {
    using StreamWriter writer = new(_filePath, false, Encoding.UTF8);
    foreach (var sectionPair in _data) {
      string sectionName = sectionPair.Key;
      writer.WriteLine($"[{sectionName}]");
      foreach (var keyPair in sectionPair.Value) {
        writer.WriteLine($"{keyPair.Key}={keyPair.Value}");
      }
    }
  }
}
