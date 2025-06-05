using CommandLine;
using CommandLine.Text;
using Pathed.Libraries;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Pathed;

internal static class Program {

  // For later use
  public static string[]? arguments;

  public static int Main(string[] args) {
    arguments = args;
    Console.OutputEncoding = Encoding.GetEncoding(Encoding.Default.CodePage);

    Type[] _types = [
      typeof(AppendOptions),
      typeof(PrependOptions),
      typeof(RemoveOptions),
      typeof(ShowOptions),
      typeof(SlimOptions),
      typeof(SortOptions)
    ];

    var result = new Parser(c => {
      c.HelpWriter = null;
      c.CaseSensitive = false;
      c.CaseInsensitiveEnumValues = true;
    }).ParseArguments(args, _types);
    result.WithParsed<AppendOptions>(opt => Runner.Append(opt));
    result.WithParsed<PrependOptions>(opt => Runner.Prepend(opt));
    result.WithParsed<RemoveOptions>(opt => Runner.Remove(opt));
    result.WithParsed<ShowOptions>(opt => Runner.Show(opt));
    result.WithParsed<SlimOptions>(opt => Runner.Slim(opt));
    result.WithParsed<SortOptions>(opt => Runner.Sort(opt));
    result.WithNotParsed(errs => DisplayHelp(result, errs));
    return 0;
  }

  private static int DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs) {
    if (errs.IsVersion()) {
      // print version information directly, without using CommandLineParser
      Assembly asm = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
      string name = asm.GetName().Name;
      string version = asm.GetName().Version?.ToString() ?? "0.0.0.0";
      Console.WriteLine($"{name} {version}");
      return 1;
    } else {
      HelpText ht = HelpText.AutoBuild(result, ht => {
        var h = HelpText.DefaultParsingErrorsHandler(result, ht);
        h.Copyright = string.Empty;
        h.AddDashesToOption = true;
        h.AdditionalNewLineAfterOption = false;
        h.MaximumDisplayWidth = Console.WindowWidth;
        return h;
      }, e => e, true);
      Console.WriteLine(ht);
      return 1;
    }
  }
}
