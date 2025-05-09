using Option.Enums;

namespace Option;

public class MiniOption(Game game, Operation operation, string target) {
  public Game Game = game;
  public Operation Operation = operation;
  public string Target = target;
}
