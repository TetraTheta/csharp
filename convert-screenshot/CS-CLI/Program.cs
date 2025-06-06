using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using CommandLine;
using CommandLine.Text;
using common;
using Configuration;
using CS_CLI.Libraries;
using Newtonsoft.Json;
using static CS_CLI.Libraries.GameDefinition;

namespace CS_CLI;

internal class Program {
  private static readonly INI ini;

  // 'ini' must be initialized by static ctor
  static Program() {
    string name = Assembly.GetEntryAssembly().GetName().Name;
    ini = new($"{name}.ini");
  }

  private static Options ApplyDefaults(Options opt) {
    static Options FillOptions(Options opt, Dictionary<Operation, (CropPosition, string, uint)> defOpDict, Dictionary<string, string> defUIDDict) {
      // UID
      if (opt.UidArea.IsEmpty()) opt.UidArea = ini.Get(opt.Game.ToString(), "UID_Area", defUIDDict["UID_Area"]).ToUintEnumerable();
      if (opt.UidPos.IsEmpty()) opt.UidPos = ini.Get(opt.Game.ToString(), "UID_Position", defUIDDict["UID_Position"]).ToUintEnumerable();

      // WidthTo
      opt.WidthTo ??= 1280;

      // return early if Operation is All
      if (opt.Operation == Operation.All) return opt;

      // WidthFrom
      opt.WidthFrom ??= 1920;

      // fill default values (CropPosition, crop height)
      if (defOpDict.TryGetValue(opt.Operation, out var defaults)) {
        var (cp, key, def) = defaults;
        opt.CropPos = cp;
        if (!string.IsNullOrEmpty(key) && opt.CropHeight == null) opt.CropHeight = uint.Parse(ini.Get(opt.Game.ToString(), key, def.ToString()));
        return opt;
      } else {
        throw new NotImplementedException($"Unhandled {opt.Game} Operation: {opt.Operation}");
      }
    }

    if (opt.Game == Game.None) { // if no game is specified, just set CropPos to Full
      opt.CropPos ??= CropPosition.Full;
      return opt;
    } else if (opt.Game == Game.WuWa) { // game: wuwa
      IGD w = new WuWa();
      return FillOptions(opt, w.DefOp, w.DefUID);
    } else { // swallow everything else
      throw new NotImplementedException($"Unhandled Game: {opt.Game}");
    }
  }

  private static Options? CheckTarget(Options opt, Operation op) {
    try {
      opt.Target = FindTargetOrSubDir(opt, op);
      return opt;
    } catch (DirectoryNotFoundException e) {
      Console.Error.WriteLine($"\x1b[91mERROR\x1b[0m {e.Message}");
      Environment.Exit(1);
      return null;
    }
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

  private static string FindTargetOrSubDir(Options opt, Operation op) {
    if (Directory.Exists(opt.Target) && HasImageFile(opt.Target)) return opt.Target;

    string key = $"Folder_{op}";
    string def = $"CS-{OperationFolderName(op)}";
    string subName = ini.Get("General", key, def);
    string subPath = Path.Combine(opt.Target, subName);

    if (Directory.Exists(subPath) && HasImageFile(subPath)) return subPath;

    throw new DirectoryNotFoundException($"No images found in '{opt.Target}' or its subdirectory '{subPath}'");
  }

  private static bool HasImageFile(string path) {
    string[] ext = [".bmp", ".jpeg", ".jpg", ".png", ".webp"];
    return Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly).Any(f => ext.Contains(Path.GetExtension(f).ToLowerInvariant()));
  }

  private static void Main(string[] args) {
    // parse command-line
    var result = new Parser(c => {
      c.HelpWriter = null;
      c.CaseSensitive = false;
      c.CaseInsensitiveEnumValues = true;
      c.EnableDashDash = true;
      c.MaximumDisplayWidth = Console.WindowWidth;
    }).ParseArguments<Options>(args);

    // command-line parse error: print help message and exit
    result.WithNotParsed(e => DisplayHelp(result, e));

    result.WithParsed(opt => {
      // invalid value in Options: print error message and exit
      if (!opt.Validate(out var err)) {
        string prefix = "\x1b[91mERROR\x1b[0m";
        foreach (string e in err) Console.Error.WriteLine($"{prefix} {e}");
        Environment.Exit(1);
      }

      // apply default values from INI file
      opt = ApplyDefaults(opt);

      // handle 'Operation.All'
      if (opt.Operation == Operation.All) {
        RunAllOperation(opt);
      } else {
        opt = CheckTarget(opt, opt.Operation)!;
        StartGUI(opt.ToJobData());
      }
    });
  }

  private static string OperationFolderName(Operation op) {
    string name = op.ToString();
    int idx = name.Length - 1;
    while (idx >= 0 && char.IsDigit(name[idx])) idx--;
    if (idx == name.Length - 1) {
      return name;
    } else {
      string letters = name.Substring(0, idx + 1);
      string digit = name.Substring(idx + 1);
      return $"{letters}-{digit}";
    }
  }

  private static void RunAllOperation(Options opt) {
    string baseTarget = opt.Target;

    foreach (Operation op in Enum.GetValues(typeof(Operation)).Cast<Operation>()) {
      if (op == Operation.All || op == Operation.CreateDirectory) continue;

      var clone = (Options)opt.Clone();
      clone.Operation = op;
      clone = ApplyDefaults(clone);

      string key = $"Folder_{op}";
      string def = $"CS-{OperationFolderName(op)}";
      string subName = ini.Get("General", key, def);
      string subPath = Path.Combine(baseTarget, subName);

      if (Directory.Exists(subPath)) {
        if (HasImageFile(subPath)) {
          clone.Target = subPath;
          StartGUI(clone.ToJobData());
        } else {
          // i won't care about file/directory in subPath
          try { Directory.Delete(subPath, true); } catch { }
        }
      }
    }
  }

  private static void StartGUI(JobData jd) {
    // launch GUI app and pass data via STDIN
    ProcessStartInfo psi = new() {
      FileName = Path.Combine(AppContext.BaseDirectory, "ConvertScreenshotGUI.exe"),
      RedirectStandardInput = true,
      UseShellExecute = false,
      CreateNoWindow = true,
    };

    using Process p = Process.Start(psi);
    if (p == null) {
      Console.Error.WriteLine("Failed to start ConvertScreenshotGUI.exe");
      return;
    }
    using StreamWriter w = p.StandardInput;
    w.WriteLine(JsonConvert.SerializeObject(jd));
  }
}
