using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Pathed.Libraries {
public sealed partial class EnvPath {
  private const uint FileFlagBackupSemantics = 0x02000000;
  private const uint FileNameNormalized = 0x0;

  private readonly string _key;
  private readonly EnvironmentVariableTarget _target;
  private readonly List<string> _paths;

  public EnvPath(string key, EnvironmentVariableTarget target) {
    this._key = key;
    this._target = target;

    string raw = Environment.GetEnvironmentVariable(key, target) ?? string.Empty;
    _paths = raw.Split([';'], StringSplitOptions.RemoveEmptyEntries).ToList();
  }

  public int Append(string value) {
    string normalized;
    try { normalized = Normalize(value); } catch (Win32Exception) {
      WriteError($"Failed to get full path of '{value}'.");
      return 1;
    }

    _paths.RemoveAll(p => string.Equals(p, normalized, StringComparison.OrdinalIgnoreCase));
    _paths.Add(normalized);
    Show();
    return 0;
  }

  public int Prepend(string value) {
    string normalized;
    try { normalized = Normalize(value); } catch (Win32Exception) {
      WriteError($"Failed to get full path of '{value}'.");
      return 1;
    }

    _paths.RemoveAll(p => string.Equals(p, normalized, StringComparison.OrdinalIgnoreCase));
    _paths.Insert(0, normalized);
    Show();
    return 0;
  }

  public int Remove(string value) {
    int removed = _paths.RemoveAll(p => string.Equals(p, value, StringComparison.OrdinalIgnoreCase));
    if (removed == 0) return 1;
    Show();
    return 0;
  }

  public int Show() {
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("Environment Variable: ");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(_key);
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("Variable Target: ");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(_target);
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("========================================");
    Console.ResetColor();

    if (_paths.Count == 0) return 0;
    int width = Math.Max(2, (int)Math.Ceiling(Math.Log10(_paths.Count + 1)));
    for (int i = 0; i < _paths.Count; i++) {
      string entry = _paths[i];
      if (string.IsNullOrEmpty(entry)) continue;

      string index = (i + 1).ToString().PadLeft(width, '0');
      if (DoesExist(entry)) Console.WriteLine($"{index} {entry}");
      else {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{index} {entry} [INVALID]");
        Console.ResetColor();
      }
    }

    return 0;
  }

  public int Slim() {
    var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    var cleaned = new List<string>();

    foreach (string p in _paths.Where(p => !string.IsNullOrWhiteSpace(p))) {
      try {
        string full = Normalize(p);
        if (!DoesExist(full)) continue;
        if (seen.Add(full)) cleaned.Add(full);
        // ReSharper disable once RedundantJumpStatement
      } catch { continue; }
    }

    _paths.Clear();
    _paths.AddRange(cleaned);
    Show();
    return 0;
  }

  public int Sort() {
    _paths.Sort(StringComparer.OrdinalIgnoreCase);
    Show();
    return 0;
  }

  public override string ToString() {
    if (_paths.Count == 0) return string.Empty;
    return string.Join(";", _paths) + ";";
  }

  private static void WriteError(string message) {
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Error.WriteLine(message);
    Console.ResetColor();
  }

  private static string Normalize(string path) {
    string expanded = Environment.ExpandEnvironmentVariables(path);
    return GetFinalPathName(expanded);
  }

  // Some dirty jobs for disabling File System Redirection
  private static string GetFinalPathName(string dirtyPath) {
    using SafeFileHandle handle = CreateFile(dirtyPath, 0, FileShare.ReadWrite | FileShare.Delete, IntPtr.Zero, FileMode.Open, (FileAttributes)FileFlagBackupSemantics, IntPtr.Zero);
    if (handle.IsInvalid) throw new Win32Exception(Marshal.GetLastWin32Error());
    char[] buffer = new char[1024];
    uint size = GetFinalPathNameByHandle(handle, buffer, (uint)buffer.Length, FileNameNormalized);
    if (size == 0) throw new Win32Exception(Marshal.GetLastWin32Error());
    if (size > buffer.Length) throw new InvalidOperationException("Path exceeded buffer capacity");
    string result = new string(buffer, 0, (int)size);
    return result.StartsWith(@"\\?\") ? result[4..] : result;
  }

  private static bool DoesExist(string path) {
    string expanded = Environment.ExpandEnvironmentVariables(path);
    return File.Exists(expanded) || Directory.Exists(expanded);
  }

  [LibraryImport("kernel32.dll", EntryPoint = "CreateFileW", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
  private static partial SafeFileHandle CreateFile(
    [MarshalAs(UnmanagedType.LPTStr)] string filename,
    FileAccess access,
    FileShare share,
    IntPtr securityAttributes,
    FileMode creationDisposition,
    FileAttributes flagsAndAttributes,
    IntPtr templateFile
  );

  [LibraryImport("kernel32.dll", EntryPoint = "GetFinalPathNameByHandleW", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
  private static partial uint GetFinalPathNameByHandle(SafeFileHandle hFile, [Out] char[] lpszFilePath, uint cchFilePath, uint dwFlags);
}
}
