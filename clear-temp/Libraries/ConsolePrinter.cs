using System;
using System.IO;

namespace ClearTemp.Libraries {
public enum ResultKind {
  Info,
  Success,
  Locked,
  NoPerm
}

public static class ConsolePrinter {
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
        action = "[  DELETED ] ";
        color = ConsoleColor.Green;
        break;
      case ResultKind.Locked:
        action = "[  LOCKED  ] ";
        color = ConsoleColor.Red;
        break;
      case ResultKind.NoPerm:
        action = "[  NOPERM  ] ";
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
}
