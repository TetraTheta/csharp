using System;
using System.IO;

using C = System.Console;

namespace ClearTemp.Common.Console;

public static class ConsolePrinter {
  private const int HrDirNotEmpty = unchecked((int)0x80070091);
  private const int HrLockViolation = unchecked((int)0x80070021);
  private const int HrSharingViolation = unchecked((int)0x80070020);
  private const int HrUserMappedFile = unchecked((int)0x800704C8);

  public static void PrintResult(ResultKind result, string path) {
    string name = Path.GetFileName(path);
    string prefix = Path.GetDirectoryName(path) ?? string.Empty;

    ConsoleColor color;
    string action;
    switch (result) {
      case ResultKind.Info:
        action = "[PROCESSING] ";
        color = ConsoleColor.Green;
        break;
      case ResultKind.Success:
        action = "[DELETED   ] ";
        color = ConsoleColor.Green;
        break;
      case ResultKind.Locked:
        action = "[LOCKED    ] ";
        color = ConsoleColor.Red;
        break;
      case ResultKind.NotEmpty:
        action = "[NOT EMPTY ] ";
        color = ConsoleColor.Red;
        break;
      case ResultKind.NoPerm:
        action = "[NO PERM   ] ";
        color = ConsoleColor.Red;
        break;
      case ResultKind.NotFound:
        action = "[NOT FOUND ] ";
        color = ConsoleColor.DarkYellow;
        break;
      case ResultKind.TooLong:
        action = "[LONG PATH ] ";
        color = ConsoleColor.Red;
        break;
      case ResultKind.BadPath:
        action = "[BAD PATH  ] ";
        color = ConsoleColor.Red;
        break;
      case ResultKind.IoError:
        action = "[IO ERROR  ] ";
        color = ConsoleColor.Red;
        break;
      case ResultKind.Error:
        action = "[ERROR     ] ";
        color = ConsoleColor.Red;
        break;
      default:
        throw new NotImplementedException();
    }

    C.ForegroundColor = color;
    C.Write(action);
    C.ResetColor();

    if (!string.IsNullOrEmpty(prefix)) C.Write(prefix + Path.DirectorySeparatorChar);

    C.ForegroundColor = ConsoleColor.Yellow;
    C.WriteLine(name);
    C.ResetColor();
  }

  public static void PrintException(Exception e, string path) {
    Exception ex = e.GetBaseException();
    ResultKind kind = Classify(ex);
    PrintResult(kind, path);

    if (kind is ResultKind.IoError or ResultKind.BadPath or ResultKind.Error) {
      C.WriteLine($"      {ex.GetType().FullName} (0x{ex.HResult:X8})");
      C.WriteLine($"      {ex.Message}");
    }
  }

  private static ResultKind Classify(Exception ex) {
    return ex switch {
      UnauthorizedAccessException => ResultKind.NoPerm,
      DirectoryNotFoundException or FileNotFoundException => ResultKind.NotFound,
      PathTooLongException => ResultKind.TooLong,
      ArgumentException or NotSupportedException => ResultKind.BadPath,
      IOException io => ClassifyIo(io),
      _ => ResultKind.Error
    };
  }

  private static ResultKind ClassifyIo(IOException ex) {
    return ex.HResult switch {
      HrDirNotEmpty => ResultKind.NotEmpty,
      HrSharingViolation or HrLockViolation or HrUserMappedFile => ResultKind.Locked,
      _ => ResultKind.IoError
    };
  }
}
