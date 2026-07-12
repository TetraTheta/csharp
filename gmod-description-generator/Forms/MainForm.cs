using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GModDescriptionGenerator;

public partial class MainForm : Form {
  private DateTime today = DateTime.Today;
  private string nl = Environment.NewLine;

  public MainForm() {
    InitializeComponent();

    comboBoxRDDate.Text = today.Day.ToString();
    comboBoxRDMonth.Text = today.ToString("MMM", CultureInfo.InvariantCulture);
    comboBoxRDYear.Text = today.Year.ToString();
  }

  private string ReplaceNewLine(string original) => original.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", nl);
  private string GetText(Control control) => control.Text.Trim();
  private string[] GetLines(TextBoxBase control) => control.Lines.Where(line => !string.IsNullOrWhiteSpace(line)).Select(line => line.Trim()).ToArray();

  private void buttonGenerate_Click(object sender, System.EventArgs e) {
    StringBuilder sb = new();
    // Title
    sb.Append($"[h1]{GetText(textBoxTitle)}[/h1]\n");
    // Author(s)
    sb.Append($"[i]created by {GetText(textBoxAuthor)}[/i]\n\n");
    // Release Date
    sb.Append($"Date of publish: {GetText(comboBoxRDDate)} {GetText(comboBoxRDMonth)} {GetText(comboBoxRDYear)}\n");
    // Horizontal Rule
    sb.Append("[hr]");
    // Description
    if (GetText(textBoxDescription).Length > 0) sb.Append($"[h2]Description[/h2]\n{GetText(textBoxDescription)}\n");
    // Synopsis
    if (GetText(textBoxSynopsis).Length > 0) sb.Append($"[h2]Synopsis[/h2]\n{GetText(textBoxSynopsis)}\n");
    // Requirements
    bool hl2 = checkBoxHL2.Checked;
    bool css = checkBoxCSS.Checked;
    bool tf2 = checkBoxTF2.Checked;
    bool l4d2 = checkBoxL4D2.Checked;
    if (hl2 || css || tf2 || l4d2) {
      sb.Append("[h2]Requirements[/h2]\n[list]\n");
      if (hl2) sb.Append("  [*]Half-Life 2 (+ Episode One, Episode Two)\n");
      if (css) sb.Append("  [*]Counter-Strike: Source\n");
      if (tf2) sb.Append("  [*]Team Fortress 2\n");
      if (l4d2) sb.Append("  [*]Left 4 Dead 2\n");
      sb.Append("[/list]\n");
      sb.Append("[u]You should mount them anyway, because assets that are included in Garry's Mod doesn't have [b]music[/b] or voice-over.[/u]\n");
    }
    // Map List
    string[] lines = GetLines(textBoxMapList);
    if (lines.Length > 0) {
      sb.Append("[h2]Map List[/h2]\n");
      foreach (string line in lines) sb.Append($"  [*]{line}\n");
      sb.Append("[/list]\n");
    }
    // Recommendations
    sb.Append("[h2]Recommendations[/h2]\n");
    sb.Append("To avoid CTD(Crash To Desktop) or frame drop, please follow these instructions:\n");
    sb.Append("[list]\n");
    sb.Append("  [*]Use the 'x86-64' branch of Garry's Mod if you are using 64-bit OS (You would already using 64-bit OS).\n");
    sb.Append("  [*]Keep enabled add-ons minimum. Disable or unsubscribe add-ons you don't use.\n");
    sb.Append("  [*](only if the game is still broken) Factory reset Garry's Mod.\n");
    sb.Append("[/list]\n");
    sb.Append("I didn't experience any frame drops or CTD while testing this map, so if you experienced any, that would be your environment's problem, which I can't fix.\n");
    if (checkBoxSCTools.Checked) {
      sb.Append("\nYou should also subscribe [url=https://steamcommunity.com/sharedfiles/filedetails/?id=3207465120]SC Tools[/url] for level transition. You can still play the map without it, but you won't have any level transition.\n");
    } else {
      sb.Append("\nI personally recommend you to also subscribe [url=https://steamcommunity.com/sharedfiles/filedetails/?id=3207465120]SC Tools[/url] for [i]better[/i] experience.\n");
    }
    if (checkBoxSubtitle.Checked) {
      sb.Append("\nYou can get Closed Caption if you subscribe [url=https://steamcommunity.com/sharedfiles/filedetails/?id=3311765429]Simplest Subtitles Framework[/url]. I highly recommend you to subscribe it.\n");
    }
    // Warning
    bool recompiled = checkBoxRecompiled.Checked;
    string warning = GetText(textBoxWarning);
    if (recompiled || warning.Length > 0) {
      sb.Append("[h2]Warning[/h2]\n");
      if (recompiled) {
        sb.Append("This add-on contains recompiled map(s), which was necessary to fix and optimize the map(s).\n");
        sb.Append("If you find any glitch or weird thing(s) that aren't in the original map(s), please report to me. I'll see if I can fix it or not.\n");
      }
      if (warning.Length > 0) {
        if (recompiled) sb.Append("\n");
        sb.Append($"{warning}\n");
      }
    }
    // Disclaimer
    sb.Append("[h2]Disclaimer[/h2]\n");
    sb.Append("I only test map(s) with Sandbox gamemode with Singleplay environment. I do not guarantee that this add-on will work with any gamemode or Multiplay environment.\n\n");
    sb.Append("I didn't make these map(s). I just [i]ported[/i] these map(s) to Garry's Mod. All credits should go to original author(s).\n");
    sb.Append("[hr]Search Tag: [spoiler]half life hl2 custom campaign[/spoiler]");

    textBoxPreview.Text = ReplaceNewLine(sb.ToString());
  }

  private void buttonReset_Click(object sender, EventArgs e) {
    DialogResult result = MessageBox.Show("Are you sure want to reset?", "Important Question", MessageBoxButtons.YesNo);
    if (result == DialogResult.Yes) {
      textBoxTitle.Text = string.Empty;
      textBoxAuthor.Text = string.Empty;
      comboBoxRDDate.Text = today.Day.ToString();
      comboBoxRDMonth.Text = today.ToString("MMM", CultureInfo.InvariantCulture);
      comboBoxRDYear.Text = today.Year.ToString();
      textBoxDescription.Text = string.Empty;
      textBoxSynopsis.Text = string.Empty;
      checkBoxHL2.Checked = true;
      checkBoxCSS.Checked = false;
      checkBoxTF2.Checked = false;
      checkBoxL4D2.Checked = false;
      textBoxMapList.Text = string.Empty;
      checkBoxSubtitle.Checked = false;
      checkBoxSCTools.Checked = false;
      checkBoxRecompiled.Checked = false;
      textBoxWarning.Text = string.Empty;
      textBoxPreview.Text = string.Empty;
    }
  }

  private void buttonCopy_Click(object sender, EventArgs e) {
    string preview = ReplaceNewLine(GetText(textBoxPreview));
    if (preview.Length > 0) {
      Clipboard.SetDataObject(preview, true);
      MessageBox.Show("Copied Preview text.", "Copied", MessageBoxButtons.OK);
    }
  }
}
