using Option.Enums;

namespace Option;

public class ParsedOption(Game? game, Operation? operation, string? target, bool english = false, bool help = false, bool verbose = false, bool version = false) {
  public override string ToString() => $"Game: {(object?)Game ?? "null"} | Operation: {(object?)Operation ?? "null"} | Target: {(object?)Target ?? "null"}\nEnglish: {English} | Help: {Help} | Verbose: {Verbose} | Version: {Version}";

  public Game? Game = game;
  public Operation? Operation = operation;
  public string? Target = target;
  public bool English = english;
  public bool Help = help;
  public bool Verbose = verbose;
  public bool Version = version;
}
