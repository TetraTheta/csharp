using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

using GModDescriptionGenerator.Common.Description;
using GModDescriptionGenerator.Common.Text;
using GModDescriptionGenerator.Common.WinForm;

namespace GModDescriptionGenerator.UI;

public partial class MainForm : Form {
  private DateTime today = DateTime.Today;

  public MainForm() {
    InitializeComponent();

    comboBoxRDDate.SelectedIndex = comboBoxRDDate.FindStringExact(today.Day.ToString());
    comboBoxRDMonth.SelectedIndex = comboBoxRDMonth.FindStringExact(today.ToString("MMM", CultureInfo.InvariantCulture));
    comboBoxRDYear.SelectedIndex = comboBoxRDYear.FindStringExact(today.Year.ToString());

    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    RegisterDragDrop(this);
  }

  private void OpenFile(string filePath) {
    try {
      string jsonString = File.ReadAllText(filePath);
      DescriptionSettings settings = JsonSerializer.Deserialize(jsonString, DescriptionJsonContext.Default.DescriptionSettings);
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
      checkBoxNoRD.Checked = settings.NoRD;
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
      // set save
      saveFileDialog.FileName = filePath;
      // set directory
      string dir = Path.GetDirectoryName(filePath);
      if (!string.IsNullOrEmpty(dir)) {
        openFileDialog.InitialDirectory = dir;
        saveFileDialog.InitialDirectory = dir;
      }
    } catch (Exception ex) {
      MessageBox.Show($"Error occurred while opening file: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
  }

  private void RegisterDragDrop(Control ctrl) {
    ctrl.AllowDrop = true;
    ctrl.DragEnter += MainForm_DragEnter;
    ctrl.DragDrop += MainForm_DragDrop;
    foreach (Control c in ctrl.Controls) RegisterDragDrop(c);
  }

  private void buttonReset_Click(object sender, EventArgs e) {
    DialogResult result = MessageBox.Show("Are you sure want to reset?", "Important Question", MessageBoxButtons.YesNo);
    if (result == DialogResult.Yes) {
      textBoxTitle.Text = string.Empty;
      textBoxAuthor.Text = string.Empty;
      comboBoxRDDate.SelectedIndex = comboBoxRDDate.FindStringExact(today.Day.ToString());
      comboBoxRDMonth.SelectedIndex = comboBoxRDMonth.FindStringExact(today.ToString("MMM", CultureInfo.InvariantCulture));
      comboBoxRDYear.SelectedIndex = comboBoxRDYear.FindStringExact(today.Year.ToString());
      checkBoxNoRD.Checked = false;
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
    sb.Append($"[i]created by {textBoxAuthor.TrimmedText}[/i]\n");
    // Release Date
    if (!checkBoxNoRD.Checked) sb.Append($"\nDate of publish: {comboBoxRDDate.TrimmedText} {comboBoxRDMonth.TrimmedText} {comboBoxRDYear.TrimmedText}\n");
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
      sb.Append("[u]Mount them anyway; Garry's Mod's default assets lack [b]music and voice-overs[/b].[/u]\n");
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
    sb.Append("To prevent CTD or frame drops, follow these steps:\n");
    sb.Append("[list]\n");
    sb.Append("  [*]Use 'x86-64' beta for 64-bit Garry's Mod.\n");
    sb.Append("  [*]Disable or unsubscribe from non-essential add-ons.\n");
    sb.Append("  [*]If the game is still broken, factory reset it.\n");
    sb.Append("[/list]\n");
    sb.Append("The maps tested fine without lags or CTDs. Any issues you face are likely system-specific, which unfortunately I can't fix.\n");
    if (checkBoxSCTools.Checked) {
      sb.Append("\n[url=https://steamcommunity.com/sharedfiles/filedetails/?id=3207465120]SC Tools[/url] is required for level transitions. You can still play without it, but transitions won't work.\n");
    } else {
      sb.Append("\nI highly recommend subscribing to [url=https://steamcommunity.com/sharedfiles/filedetails/?id=3207465120]SC Tools[/url] for a [i]better[/i] experience.\n");
    }
    if (checkBoxSubtitle.Checked) {
      sb.Append("\nSubscribe to [url=https://steamcommunity.com/sharedfiles/filedetails/?id=3311765429]Simplest Subtitles Framework[/url] for closed captions (highly recommended).\n");
    }
    // Warning
    bool recompiled = checkBoxRecompiled.Checked;
    string warning = textBoxWarning.TrimmedText;
    if (recompiled || warning.Length > 0) {
      sb.Append("[h2]Warning[/h2]\n");
      if (recompiled) {
        sb.Append("This add-on includes recompiled maps for fixes and optimization.\n");
        sb.Append("Please report any [b]new[/b] glitches or issues; I will try to fix them.\n");
      }
      if (warning.Length > 0) {
        if (recompiled) sb.Append("\n");
        sb.Append($"{warning}\n");
      }
    }
    // Disclaimer
    sb.Append("[h2]Disclaimer[/h2]\n");
    sb.Append("Tested only in Singleplayer Sandbox. Compatibility with other gamemodes or Multiplayer is not guaranteed.\n\n");
    sb.Append("I did not create these maps; I only [i]ported[/i] them to Garry's Mod. All credit goes to the original authors.\n");
    // Search Tag
    sb.Append("[hr][/hr]Search Tag: [spoiler]half life hl2 custom campaign[/spoiler]");

    textBoxPreview.Text = sb.ToString().NativeLine;
  }

  private void tsmiOpen_Click(object sender, EventArgs e) {
    DialogResult result = openFileDialog.ShowDialog();
    if (result == DialogResult.OK) OpenFile(openFileDialog.FileName);
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
        NoRD = checkBoxNoRD.Checked,
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
        string jsonString = JsonSerializer.Serialize(settings, DescriptionJsonContext.Default.DescriptionSettings);
        File.WriteAllText(saveFileDialog.FileName, jsonString);
        // set directory
        string dir = Path.GetDirectoryName(saveFileDialog.FileName);
        if (!string.IsNullOrEmpty(dir)) {
          openFileDialog.InitialDirectory = dir;
          saveFileDialog.InitialDirectory = dir;
        }
        MessageBox.Show("Save complete", "SAVE SUCCESSFUL", MessageBoxButtons.OK, MessageBoxIcon.Information);
      } catch (Exception ex) {
        MessageBox.Show($"Error occurred while saving file: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }

  private void tsmiExit_Click(object sender, EventArgs e) => Application.Exit();

  private void MainForm_DragEnter(object sender, DragEventArgs e) {
    if (!e.Data.GetDataPresent(DataFormats.FileDrop)) {
      e.Effect = DragDropEffects.None;
      return;
    }
    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
    if (files.Length == 1 && Path.GetExtension(files[0]).Equals(".json", StringComparison.OrdinalIgnoreCase)) {
      e.Effect = DragDropEffects.Copy;
    } else {
      e.Effect = DragDropEffects.None;
    }
  }

  private void MainForm_DragDrop(object sender, DragEventArgs e) {
    if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
    if (files.Length == 1 && Path.GetExtension(files[0]).Equals(".json", StringComparison.OrdinalIgnoreCase)) {
      OpenFile(files[0]);
    }
  }
}
