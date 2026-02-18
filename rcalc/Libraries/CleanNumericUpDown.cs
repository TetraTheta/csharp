using System;
using System.Globalization;
using System.Windows.Forms;

namespace rcalc.Libraries {
public class CleanNumericUpDown : NumericUpDown {
  protected override void UpdateEditText() {
    Text = Value.ToString("0.############################", CultureInfo.CurrentCulture);
  }

  protected override void ValidateEditText() {
    if (decimal.TryParse(Text, out decimal parsed)) { Value = Math.Min(Math.Max(parsed, Minimum), Maximum); }

    base.ValidateEditText();
  }
}
}
