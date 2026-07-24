using System;
using System.Text;

using ConsoleAppFramework;

using Pathed.Common.Command;

namespace Pathed;

public static class Program {
  public static void Main(string[] args) {
    Console.OutputEncoding = Encoding.GetEncoding(Encoding.Default.CodePage);

    var app = ConsoleApp.Create();
    app.Add<PathedCommand>();
    app.Run(args);
  }
}
