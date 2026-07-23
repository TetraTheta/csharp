using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace rcalc.Libraries;

[ToolboxItem(true)]
public class CleanNumericUpDown : NumericUpDown {
  protected override void UpdateEditText() {
    if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) {
      base.UpdateEditText();
      return;
    }

    Text = Value.ToString("0.############################", CultureInfo.CurrentCulture);
  }

  protected override void ValidateEditText() {
    if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) {
      base.ValidateEditText();
      return;
    }

    if (decimal.TryParse(Text, out decimal parsed)) Value = Math.Min(Math.Max(parsed, Minimum), Maximum);

    base.ValidateEditText();
  }
}
