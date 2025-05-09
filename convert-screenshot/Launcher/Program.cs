using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Pipes;
using System.Reflection;
using System.Text;
using System.Threading;
using ANSI;
using Launcher.Libraries;
using Launcher.Libraries.Help;
using Newtonsoft.Json;
using Option;
using A = ANSI.ANSI;
using R = Launcher.Resources.Locale.LConsole;

namespace Launcher;

internal static class Program {
  private const string PipeServerName = "ConvertScreenshotPipe";
  private static bool isDebug;

  private static void Main(string[] args) {
    ParsedOption popt = ArgumentParser.Parse(args);

    isDebug = popt.Verbose;
    DebugPrint($"{A.Style(Color.FB_YELLOW, null, [Style.UNDERLINE])}[DEBUG] Parsed arguments{A.RST}:\n{popt}");

    if (popt.English) {
      var ci = new CultureInfo("en-US");
      Thread.CurrentThread.CurrentCulture = ci;
      Thread.CurrentThread.CurrentUICulture = ci;
      CultureInfo.DefaultThreadCurrentCulture = ci;
      CultureInfo.DefaultThreadCurrentUICulture = ci;
    }

    if (popt.Version) {
      Help.PrintVersion();
      Environment.Exit(0);
    } else if (popt.Help) {
      Help.PrintHelp(HelpStep.All);
      Environment.Exit(0);
    }

    // Parsed result error handling
    if (popt.Game == null) {
      Console.Error.WriteLine($"{A.Style(Color.FB_RED)}ERROR{A.RST} {R.ErrorFailedToParse}<GAME>");
      Help.PrintHelp(HelpStep.Game);
      Environment.Exit(1);
    }
    if (popt.Operation == null) {
      Console.Error.WriteLine($"{A.Style(Color.FB_RED)}ERROR{A.RST} {R.ErrorFailedToParse}<OPERATION>");
      Help.PrintHelp(HelpStep.Operation);
      Environment.Exit(1);
    }
    popt.Target = popt.Target == null ? Directory.GetCurrentDirectory() : Path.GetFullPath(popt.Target);

    DebugPrint($"{A.Style(Color.FB_YELLOW, null, [Style.UNDERLINE])}[DEBUG] Parsed arguments 2{A.RST}:\n{popt}");

    MiniOption opt = new(popt.Game!.Value, popt.Operation!.Value, popt.Target);

    try {
      Process.Start(new ProcessStartInfo {
        FileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ConvertScreenshotGUI.exe"),
        UseShellExecute = true,
      });
    } catch (Exception e) {
      Console.Error.WriteLine($"Error launching ConvertScreenshotGUI: {e.Message}");
      Environment.Exit(2);
    }

    using var pipe = new NamedPipeServerStream(PipeServerName, PipeDirection.Out, 1, PipeTransmissionMode.Byte);
    Console.WriteLine("waiting GUI");

    if (!pipe.WaitForConnectionAsync().Wait(TimeSpan.FromSeconds(5))) {
      Console.Error.WriteLine("GUI connection timed out");
      Environment.Exit(3);
    }

    Console.WriteLine("connected");

    try {
      string data = JsonConvert.SerializeObject(opt);
      byte[] dataBytes = Encoding.UTF8.GetBytes(data);
      pipe.Write(dataBytes, 0, dataBytes.Length);
      pipe.Flush();
      Console.WriteLine("send complete");
    } catch (Exception ex) {
      Console.Error.WriteLine($"Pipe write error: {ex.Message}");
    }

    Thread.Sleep(1000);
  }

  private static void DebugPrint(string msg) {
    if (isDebug) Console.WriteLine(msg);
  }
}
