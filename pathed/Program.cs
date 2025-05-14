using CommandLine;
using CommandLine.Text;
using Pathed.Libraries;
using System;
using System.Collections.Generic;
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
    HelpText? helpText = null;
    if (errs.IsVersion()) {
      helpText = HelpText.AutoBuild(result);
    } else {
      helpText = HelpText.AutoBuild(result, h => HelpText.DefaultParsingErrorsHandler(result, h), e => e, true);
    }
    Console.WriteLine(helpText);
    return 1;
  }
}
