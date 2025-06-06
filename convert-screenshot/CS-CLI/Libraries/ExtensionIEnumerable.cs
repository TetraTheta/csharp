using System.Collections.Generic;
using System.Linq;

namespace CS_CLI.Libraries;
public static class ExtensionIEnumerable {
  public static bool IsEmpty<T>(this IEnumerable<T>? t) => t == null || t.Any();
}
