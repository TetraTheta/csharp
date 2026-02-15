using System.Windows.Forms;

namespace DarkModeForms {
public static class DarkModeHelper {
  // ReSharper disable once ObjectCreationAsStatement
  public static void Apply(Form form) => new DarkModeForms(form, null);
}
}
