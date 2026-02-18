namespace ClearTemp.Libraries {
public enum RemoveOption {
  None,
  Recurse,
  RemoveSelf
}

public class ConfigEntry(string pathPattern, PatternSet patterns, RemoveOption option) {
  public string PathPattern { get; } = pathPattern;
  public PatternSet Patterns { get; } = patterns;
  public RemoveOption Option { get; } = option;
}
}
