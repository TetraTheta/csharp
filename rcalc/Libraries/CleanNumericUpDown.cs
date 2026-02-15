using System;
using System.Globalization;
using System.Windows.Forms;

namespace rcalc.Libraries {
public class CleanNumericUpDown : NumericUpDown {
  protected override void UpdateEditText() {
    this.Text = this.Value.ToString("0.############################", CultureInfo.CurrentCulture);
  }

  protected override void ValidateEditText() {
    if (decimal.TryParse(this.Text, out decimal parsed)) { this.Value = Math.Min(Math.Max(parsed, this.Minimum), this.Maximum); }

    base.ValidateEditText();
  }
}
}
