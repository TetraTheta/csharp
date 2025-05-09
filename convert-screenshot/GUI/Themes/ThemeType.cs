using System;

namespace GUI.Themes;

public enum ThemeType {
  DarkGreyTheme,
  DeepDark,
  GreyTheme,
  LightTheme,
  RedBlackTheme,
  SoftDark,
}

public static class ThemeTypeExtension {
  public static string GetName(this ThemeType type) {
    return type switch {
      ThemeType.DarkGreyTheme => "DarkGreyTheme",
      ThemeType.DeepDark => "DeepDark",
      ThemeType.GreyTheme => "GreyTheme",
      ThemeType.LightTheme => "LightTheme",
      ThemeType.RedBlackTheme => "RedBlackTheme",
      ThemeType.SoftDark => "SoftDark",
      _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
    };
  }
}
