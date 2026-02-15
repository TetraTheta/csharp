namespace ClearTemp.Libraries {
public enum RemoveOption {
  None,
  Recurse,
  RemoveSelf
}

public class ConfigEntry {
  public string PathPattern { get; }
  public PatternSet Patterns { get; }
  public RemoveOption Option { get; }

  public ConfigEntry(string pathPattern, PatternSet patterns, RemoveOption option) {
    this.PathPattern = pathPattern;
    this.Patterns = patterns;
    this.Option = option;
  }
}
}
