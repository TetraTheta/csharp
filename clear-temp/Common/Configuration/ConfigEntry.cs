namespace ClearTemp.Common.Configuration;

public record ConfigEntry(string PathPattern, PatternSet Patterns, RemoveOption Option);
