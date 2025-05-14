using System.Collections.Generic;
using System.Linq;

namespace ANSI;

public static class ANSI {

  // Quick reference
  public static readonly string BLD = Style(null, null, [global::ANSI.Style.BOLD]);

  public static readonly string BUL = Style(null, null, [global::ANSI.Style.BOLD, global::ANSI.Style.UNDERLINE]);
  public static readonly string RST = Style(null, null, [global::ANSI.Style.RESET]);
  public static readonly string UL = Style(null, null, [global::ANSI.Style.UNDERLINE]);

  public static string Style(string? foreground = null, string? background = null, params string[] styles) {
    List<string> codes = [];

    if (styles.Contains(global::ANSI.Style.RESET))
      return "\u001b[0m"; // RESET cannot be used with other colors/styles

    if (foreground != null)
      codes.Add(foreground);

    if (background != null)
      codes.Add(background);

    codes.AddRange(styles);

    return $"\u001b[{string.Join(";", codes)}m";
  }
}
