using System.Windows.Forms;

namespace DarkModeForms;
public static class DMHelper {
  public static void Apply(Form form) => new DarkModeCS(form);
}
