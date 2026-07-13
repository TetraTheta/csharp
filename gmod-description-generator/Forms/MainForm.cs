using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

using GModDescriptionGenerator.Libraries;
using GModDescriptionGenerator.Libraries.Extension;

namespace GModDescriptionGenerator.Forms;

public partial class MainForm : Form {
  private DateTime today = DateTime.Today;
  private readonly JsonSerializerOptions jsonOpt = new JsonSerializerOptions {
    WriteIndented = true,
    IndentCharacter = ' ',
    IndentSize = 2
  };

  public MainForm() {
    InitializeComponent();

    comboBoxRDDate.SelectedIndex = comboBoxRDDate.FindStringExact(today.Day.ToString());
    comboBoxRDMonth.SelectedIndex = comboBoxRDMonth.FindStringExact(today.ToString("MMM", CultureInfo.InvariantCulture));
    comboBoxRDYear.SelectedIndex = comboBoxRDYear.FindStringExact(today.Year.ToString());

    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
  }

  private void buttonReset_Click(object sender, EventArgs e) {
    DialogResult result = MessageBox.Show("Are you sure want to reset?", "Important Question", MessageBoxButtons.YesNo);
    if (result == DialogResult.Yes) {
      textBoxTitle.Text = string.Empty;
      textBoxAuthor.Text = string.Empty;
      comboBoxRDDate.SelectedIndex = comboBoxRDDate.FindStringExact(today.Day.ToString());
      comboBoxRDMonth.SelectedIndex = comboBoxRDMonth.FindStringExact(today.ToString("MMM", CultureInfo.InvariantCulture));
      comboBoxRDYear.SelectedIndex = comboBoxRDYear.FindStringExact(today.Year.ToString());
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
    string preview = textBoxPreview.TrimmedText.NativeLine;
    if (preview.Length > 0) {
      Clipboard.SetDataObject(preview, true);
      MessageBox.Show("Copied Preview text.", "Copied", MessageBoxButtons.OK);
    }
  }

  private void buttonGenerate_Click(object sender, System.EventArgs e) {
    StringBuilder sb = new();
    // Title
    sb.Append($"[h1]{textBoxTitle.TrimmedText}[/h1]\n");
    // Author(s)
    sb.Append($"[i]created by {textBoxAuthor.TrimmedText}[/i]\n\n");
    // Release Date
    sb.Append($"Date of publish: {comboBoxRDDate.TrimmedText} {comboBoxRDMonth.TrimmedText} {comboBoxRDYear.TrimmedText}\n");
    // Horizontal Rule
    sb.Append("[hr][/hr]");
    // Description
    if (textBoxDescription.TrimmedText.Length > 0) sb.Append($"[h2]Description[/h2]\n{textBoxDescription.TrimmedText}\n");
    // Synopsis
    if (textBoxSynopsis.TrimmedText.Length > 0) sb.Append($"[h2]Synopsis[/h2]\n{textBoxSynopsis.TrimmedText}\n");
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
    string[] lines = textBoxMapList.TrimmedLines;
    if (lines.Length > 0) {
      sb.Append("[h2]Map List[/h2]\n");
      sb.Append("[list]\n");
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
    string warning = textBoxWarning.TrimmedText;
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
    // Search Tag
    sb.Append("[hr][/hr]Search Tag: [spoiler]half life hl2 custom campaign[/spoiler]");

    textBoxPreview.Text = sb.ToString().NativeLine;
  }

  private void tsmiOpen_Click(object sender, EventArgs e) {
    DialogResult result = openFileDialog.ShowDialog();
    if (result == DialogResult.OK) {
      try {
        string jsonString = File.ReadAllText(openFileDialog.FileName);
        DescriptionSettings settings = JsonSerializer.Deserialize<DescriptionSettings>(jsonString);
        if (settings == null) throw new InvalidDataException("Could not parse JSON file content");
        settings.TrimAll();
        // sanitize Release Date
        settings.RDDate = (settings.RDDate >= 0 && settings.RDDate < comboBoxRDDate.Items.Count) ? settings.RDDate : 0;
        settings.RDMonth = (settings.RDMonth >= 0 && settings.RDMonth < comboBoxRDMonth.Items.Count) ? settings.RDMonth : 0;
        settings.RDYear = (settings.RDYear >= 0 && settings.RDYear < comboBoxRDYear.Items.Count) ? settings.RDYear : 0;
        // apply data
        textBoxTitle.Text = settings.Title.NativeLine;
        textBoxAuthor.Text = settings.Author.NativeLine;
        comboBoxRDDate.SelectedIndex = settings.RDDate;
        comboBoxRDMonth.SelectedIndex = settings.RDMonth;
        comboBoxRDYear.SelectedIndex = settings.RDYear;
        textBoxDescription.Text = settings.Description.NativeLine;
        textBoxSynopsis.Text = settings.Synopsis.NativeLine;
        checkBoxHL2.Checked = settings.HL2;
        checkBoxCSS.Checked = settings.CSS;
        checkBoxTF2.Checked = settings.TF2;
        checkBoxL4D2.Checked = settings.L4D2;
        textBoxMapList.Text = settings.MapList.NativeLine;
        checkBoxSubtitle.Checked = settings.Subtitle;
        checkBoxSCTools.Checked = settings.SCTools;
        checkBoxRecompiled.Checked = settings.Recompiled;
        textBoxWarning.Text = settings.Warning.NativeLine;
      } catch (Exception ex) {
        MessageBox.Show($"Error occurred while opening file: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }

  private void tsmiSave_Click(object sender, EventArgs e) {
    DialogResult result = saveFileDialog.ShowDialog();
    if (result == DialogResult.OK) {
      DescriptionSettings settings = new DescriptionSettings {
        Title = textBoxTitle.TrimmedText.UnixLine,
        Author = textBoxAuthor.TrimmedText.UnixLine,
        RDDate = comboBoxRDDate.SelectedIndex,
        RDMonth = comboBoxRDMonth.SelectedIndex,
        RDYear = comboBoxRDYear.SelectedIndex,
        Description = textBoxDescription.TrimmedText.UnixLine,
        Synopsis = textBoxSynopsis.TrimmedText.UnixLine,
        HL2 = checkBoxHL2.Checked,
        CSS = checkBoxCSS.Checked,
        TF2 = checkBoxTF2.Checked,
        L4D2 = checkBoxL4D2.Checked,
        MapList = textBoxMapList.TrimmedText.UnixLine,
        Subtitle = checkBoxSubtitle.Checked,
        SCTools = checkBoxSCTools.Checked,
        Recompiled = checkBoxRecompiled.Checked,
        Warning = textBoxWarning.TrimmedText.UnixLine,
      };
      try {
        string jsonString = JsonSerializer.Serialize(settings, jsonOpt);
        File.WriteAllText(saveFileDialog.FileName, jsonString);
        MessageBox.Show("Save complete", "SAVE SUCCESSFUL", MessageBoxButtons.OK, MessageBoxIcon.Information);
      } catch (Exception ex) {
        MessageBox.Show($"Error occurred while saving file: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }

  private void tsmiExit_Click(object sender, EventArgs e) {
    Application.Exit();
  }
}
