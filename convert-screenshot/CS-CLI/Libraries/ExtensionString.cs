using System.Collections.Generic;
using System.Linq;

namespace CS_CLI.Libraries;
public static class ExtensionString {
  public static IEnumerable<uint> ToUintEnumerable(this string original) => original.Split(',').Select(uint.Parse);
}
