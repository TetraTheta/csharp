using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CommandLine;
using CommandLine.Text;
using Pathed.Libraries;

namespace Pathed {
public static class Program {
  public static int Main(string[] args) {
    Console.OutputEncoding = Encoding.GetEncoding(Encoding.Default.CodePage);

    Type[] types = {
      typeof(AppendOptions),
      typeof(PrependOptions),
      typeof(RemoveOptions),
      typeof(ShowOptions),
      typeof(SlimOptions),
      typeof(SortOptions)
    };

    var parser = new Parser(cfg => {
        cfg.HelpWriter = null;
        cfg.CaseSensitive = false;
        cfg.CaseInsensitiveEnumValues = true;
      }
    );
    ParserResult<object>? result = parser.ParseArguments(args, types);

    int exitCode = 0;

    result.WithParsed<AppendOptions>(o => exitCode = Runner.Append(o));
    result.WithParsed<PrependOptions>(o => exitCode = Runner.Prepend(o));
    result.WithParsed<RemoveOptions>(o => exitCode = Runner.Remove(o));
    result.WithParsed<ShowOptions>(o => exitCode = Runner.Show(o));
    result.WithParsed<SlimOptions>(o => exitCode = Runner.Slim(o));
    result.WithParsed<SortOptions>(o => exitCode = Runner.Sort(o));
    result.WithNotParsed(errs => exitCode = DisplayHelp(result, errs));

    return exitCode;
  }

  private static int DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs) {
    if (errs.IsVersion()) {
      // print version information directly, without using CommandLineParser
      Assembly asm = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
      Console.WriteLine($"{asm.GetName().Name} {asm.GetName().Version}");
      return 0;
    }

    int width;
    try { width = Console.WindowWidth; } catch { width = 80; }

    var help = HelpText.AutoBuild(
      result, ht => {
        HelpText? h = HelpText.DefaultParsingErrorsHandler(result, ht);
        h.Copyright = string.Empty;
        h.AddDashesToOption = true;
        h.AdditionalNewLineAfterOption = false;
        h.MaximumDisplayWidth = width;

        return h;
      }, e => e, true
    );
    Console.WriteLine(help);
    return 1;
  }
}
}
