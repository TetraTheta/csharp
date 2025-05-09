#pragma warning disable CRR0047

using System;

namespace OutputColorizer;

public interface IOutputWriter {
  void Write(string text);
  void WriteLine(string text);

  ConsoleColor ForegroundColor { get; set; }
}

public class ConsoleWriter : IOutputWriter {
  public ConsoleColor ForegroundColor {
    get => Console.ForegroundColor;
    set => Console.ForegroundColor = value;
  }

  public void Write(string text) => Console.Write(text);

  public void WriteLine(string text) => Console.WriteLine(text);
}

public class ConsoleErrorWriter : IOutputWriter {
  public ConsoleColor ForegroundColor {
    get => Console.ForegroundColor;
    set => Console.ForegroundColor = value;
  }

  public void Write(string text) => Console.Error.Write(text);

  public void WriteLine(string text) => Console.Error.WriteLine(text);
}
