using DarkModeForms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace rcalc {
  public partial class CalculatorForm : Form {
    public CalculatorForm(decimal x, decimal y, decimal a) {
      InitializeComponent();
      _ = new DarkModeCS(this);

      numericUpDownA.Text = a.ToString();
      numericUpDownX.Text = x.ToString();
      numericUpDownY.Text = y.ToString();

      Calculate(true);
    }

    private void ButtonCalculate_Click(object _, System.EventArgs e) {
      Calculate(true);
    }

    private void ListBoxResult_MouseDoubleClick(object _, MouseEventArgs e) {
      if (listBoxResult.SelectedItem != null) {
        string pattern = @"\[(\d+(\.\d+)?)\s*:\s*(\d+(\.\d+)?)\]\s*=\s*\[(\d+(\.\d+)?)\s*:\s*(\d+(\.\d+)?)\]";
        Match match = Regex.Match(listBoxResult.SelectedItem.ToString(), pattern);
        if (match.Success) {
          List<decimal> arr = new List<decimal>();
          for (int i = 1; i < match.Groups.Count; i += 2) {
            decimal.TryParse(match.Groups[i].Value, out decimal k);
            arr.Add(k);
          }
          if (arr.Count != 4) {
            MessageBox.Show("Failed to parse result to list!");
          } else {
            numericUpDownX.Value = arr[0];
            numericUpDownY.Value = arr[1];
            numericUpDownA.Value = arr[2];
            Calculate(false);
          }
        } else {
          MessageBox.Show("Failed to parse result with regex!");
        }
      }
    }

    private void Calculate(bool addToList = false) {
      decimal x = numericUpDownX.Value;
      decimal y = numericUpDownY.Value;
      decimal a = numericUpDownA.Value;

      if (x == 0 || y == 0 || a == 0) {
        string result = "ERROR: You can't have 0 for ratio calculation!";
        textBoxResult.Text = result;
      } else {
        decimal b = y / x * a;
        string result = "Result: " + b.ToString("F3") + " / Equation: [" + x + " : " + y + "] = [" + a + " : " + b.ToString("F10") + "]";

        numericUpDownB.Value = b;
        textBoxResult.Text = result;
        if (addToList) AddList(result);
      }
    }

    private void AddList(string s) {
      for (int i = 0; i < listBoxResult.Items.Count; i++) {
        if (s.Equals(listBoxResult.Items[i])) {
          listBoxResult.SelectedIndex = i;
          return;
        }
      }
      listBoxResult.Items.Add(s);
      listBoxResult.SelectedItem = s;
    }
  }
}
