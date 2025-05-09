using System;
using System.IO;
using System.Reflection;
using ANSI;
using A = ANSI.ANSI;
using R = Launcher.Resources.Locale.LConsole;

namespace Launcher.Libraries.Help;

public static class Help {
  private const int KeyWidth = 26; // including ANSI escape sequence length
  private const int Indent = KeyWidth - 8;

  public static void PrintHelp(HelpStep step = HelpStep.All) {
    // Version
    PrintVersion();
    // Description
    PrintHelpDescription();
    // Usage
    PrintHelpUsage();
    // Switch
    PrintHelpSwitch();
    // Game
    if (step.HasFlag(HelpStep.Game)) PrintHelpGame();
    // Operation
    if (step.HasFlag(HelpStep.Operation)) PrintHelpOperation();
    // Target
    if (step.HasFlag(HelpStep.Target)) PrintHelpTarget();
  }

  private static void PrintHelpDescription() {
    Print(R.HelpProgramDescription);
    Print();
  }

  private static void PrintHelpUsage() {
    string name = Assembly.GetExecutingAssembly().GetName().Name;

    Print($"{A.BUL}Usage{A.RST}:");
    Print($"  {name} [OPTIONS] <GAME> <OPERATION> <TARGET>");
    Print();
  }

  private static void PrintHelpSwitch() {
    Print($"{A.BUL}Options{A.RST}:");
    Print();
    Print($"  {$"{A.BLD}--english{A.RST}",-(KeyWidth - 2)}{R.HelpSwtEnglish_1}");
    Print();
    Print($"  {$"{A.BLD}-h, --help{A.RST}",-(KeyWidth - 2)}{R.HelpSwtHelp_1}");
    Print();
    Print($"  {$"{A.BLD}--verbose{A.RST}",-(KeyWidth - 2)}{R.HelpSwtVerbose_1}");
    Print();
    Print($"  {$"{A.BLD}-v, --version{A.RST}",-(KeyWidth - 2)}{R.HelpSwtVersion_1}");
    Print();
  }

  private static void PrintHelpGame() {
    Print($"{A.BUL}Game{A.RST}:");
    Print(R.HelpGameDescription);
    Print();
    Print($"  {$"{A.BLD}TOF{A.RST}",-(KeyWidth - 2)}{R.HelpGameTOF_1}");
    Print($"{string.Empty,-Indent}{A.UL}{R.HelpValidValues}{A.RST}{R.HelpGameTOF_2}");
    Print();
    Print($"  {$"{A.BLD}WW{A.RST}",-(KeyWidth - 2)}{R.HelpGameWuWa_1}");
    Print($"{string.Empty,-Indent}{A.UL}{R.HelpValidValues}{A.RST}{R.HelpGameWuWa_2}");
    Print();
  }

  private static void PrintHelpOperation() {
    Print($"{A.BUL}Operation{A.RST}:");
    Print(R.HelpOperationDescription);
    Print();
    Print($"  {$"{A.BLD}Background{A.RST}",-(KeyWidth - 2)}{R.HelpOpBackground_1}");
    Print($"{string.Empty,-Indent}{R.HelpOpBackground_2}");
    Print($"{string.Empty,-Indent}{A.UL}{R.HelpValidValues}{A.RST}{R.HelpOpBackground_3}");
    Print();
    Print($"  {$"{A.BLD}Center{A.RST}",-(KeyWidth - 2)}{R.HelpOpCenter_1}");
    Print($"{string.Empty,-Indent}{R.HelpOpCenter_2}");
    Print($"{string.Empty,-Indent}{A.UL}{R.HelpValidValues}{A.RST}{R.HelpOpCenter_3}");
    Print();
    Print($"  {$"{A.BLD}Foreground0{A.RST}",-(KeyWidth - 2)}{R.HelpOpForeground0_1}");
    Print($"{string.Empty,-Indent}{R.HelpOpForeground0_2}");
    Print($"{string.Empty,-Indent}{A.UL}{R.HelpValidValues}{A.RST}{R.HelpOpForeground0_3}");
    Print();
    Print($"  {$"{A.BLD}Foreground1{A.RST}",-(KeyWidth - 2)}{R.HelpOpForeground1_1}");
    Print($"{string.Empty,-Indent}{R.HelpOpForeground1_2}");
    Print($"{string.Empty,-Indent}{A.UL}{R.HelpValidValues}{A.RST}{R.HelpOpForeground1_3}");
    Print();
    Print($"  {$"{A.BLD}Foreground2{A.RST}",-(KeyWidth - 2)}{R.HelpOpForeground2_1}");
    Print($"{string.Empty,-Indent}{R.HelpOpForeground2_2}");
    Print($"{string.Empty,-Indent}{A.UL}{R.HelpValidValues}{A.RST}{R.HelpOpForeground2_3}");
    Print();
    Print($"  {$"{A.BLD}Foreground3{A.RST}",-(KeyWidth - 2)}{R.HelpOpForeground3_1}");
    Print($"{string.Empty,-Indent}{R.HelpOpForeground3_2}");
    Print($"{string.Empty,-Indent}{A.UL}{R.HelpValidValues}{A.RST}{R.HelpOpForeground3_3}");
    Print();
    Print($"  {$"{A.BLD}Foreground4{A.RST}",-(KeyWidth - 2)}{R.HelpOpForeground4_1}");
    Print($"{string.Empty,-Indent}{R.HelpOpForeground4_2}");
    Print($"{string.Empty,-Indent}{A.UL}{R.HelpValidValues}{A.RST}{R.HelpOpForeground4_3}");
    Print();
    Print($"  {$"{A.BLD}Full{A.RST}",-(KeyWidth - 2)}{R.HelpOpFull_1}");
    Print($"{string.Empty,-Indent}{A.UL}{R.HelpValidValues}{A.RST}{R.HelpOpFull_2}");
    Print();
  }

  private static void PrintHelpTarget() {
    string cwd = Directory.GetCurrentDirectory();

    Print($"{A.BUL}Target{A.RST}:");
    Print(R.HelpTargetDescription);
    Print();
    Print($"  {$"{A.BLD}TARGET{A.RST}",-(KeyWidth - 2)}{R.HelpTgtTarget_1}");
    Print($"{string.Empty,-Indent}{R.HelpTgtTarget_2}");
    Print($"{string.Empty,-Indent}{A.UL}{R.HelpDefaultValue} ({R.HelpTgtTarget_3}){A.RST}: {cwd}", false);
  }

  public static void PrintVersion() {
    string name = Assembly.GetExecutingAssembly().GetName().Name;
    Version version = Assembly.GetExecutingAssembly().GetName().Version;

    Print($"{A.Style(Color.FB_CYAN)}{name}{A.RST} v{version.Major}.{version.Minor}.{version.Build}");
  }

  private static void Print(string? s = null, bool convert = true) {
    if (s == null) Console.WriteLine();
    else if (convert) {
      Console.WriteLine(s.Replace("\\n", "\n").Replace("\\t", "\t"));
    } else {
      Console.WriteLine(s);
    }
  }
}
