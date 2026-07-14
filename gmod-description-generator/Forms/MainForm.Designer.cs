using System.Drawing;
using System.Windows.Forms;

namespace GModDescriptionGenerator.Forms;

partial class MainForm {
  private System.ComponentModel.IContainer components = null;

  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing) {
    if (disposing && (components != null)) components.Dispose();
    base.Dispose(disposing);
  }

  #region Windows Form Designer generated code

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent() {
    buttonCopy = new Button();
    buttonGenerate = new Button();
    buttonReset = new Button();
    checkBoxCSS = new CheckBox();
    checkBoxHL2 = new CheckBox();
    checkBoxL4D2 = new CheckBox();
    checkBoxRecompiled = new CheckBox();
    checkBoxSCTools = new CheckBox();
    checkBoxSubtitle = new CheckBox();
    checkBoxTF2 = new CheckBox();
    comboBoxRDDate = new ComboBox();
    comboBoxRDMonth = new ComboBox();
    comboBoxRDYear = new ComboBox();
    fileToolStripMenuItem = new ToolStripMenuItem();
    tsmiOpen = new ToolStripMenuItem();
    tsmiSave = new ToolStripMenuItem();
    sepSave = new ToolStripSeparator();
    tsmiExit = new ToolStripMenuItem();
    groupBoxOption = new GroupBox();
    labelAuthor = new Label();
    labelDescription = new Label();
    labelMapList = new Label();
    labelRecommendation = new Label();
    labelReleaseDate = new Label();
    labelRequirements = new Label();
    labelSynopsis = new Label();
    labelTitle = new Label();
    labelWarning = new Label();
    textBoxAuthor = new TextBox();
    textBoxDescription = new TextBox();
    textBoxMapList = new TextBox();
    textBoxSynopsis = new TextBox();
    textBoxTitle = new TextBox();
    textBoxWarning = new TextBox();
    groupBoxPreview = new GroupBox();
    textBoxPreview = new TextBox();
    menuStrip = new MenuStrip();
    openFileDialog = new OpenFileDialog();
    saveFileDialog = new SaveFileDialog();
    groupBoxOption.SuspendLayout();
    groupBoxPreview.SuspendLayout();
    menuStrip.SuspendLayout();
    SuspendLayout();
    // 
    // buttonCopy
    // 
    buttonCopy.Location = new Point(791, 724);
    buttonCopy.Name = "buttonCopy";
    buttonCopy.Size = new Size(75, 33);
    buttonCopy.TabIndex = 20;
    buttonCopy.Text = "Copy";
    buttonCopy.UseVisualStyleBackColor = true;
    buttonCopy.Click += buttonCopy_Click;
    // 
    // buttonGenerate
    // 
    buttonGenerate.Location = new Point(872, 724);
    buttonGenerate.Name = "buttonGenerate";
    buttonGenerate.Size = new Size(100, 33);
    buttonGenerate.TabIndex = 18;
    buttonGenerate.Text = "Generate";
    buttonGenerate.UseVisualStyleBackColor = true;
    buttonGenerate.Click += buttonGenerate_Click;
    // 
    // buttonReset
    // 
    buttonReset.Location = new Point(12, 724);
    buttonReset.Name = "buttonReset";
    buttonReset.Size = new Size(75, 33);
    buttonReset.TabIndex = 19;
    buttonReset.Text = "Reset";
    buttonReset.UseVisualStyleBackColor = true;
    buttonReset.Click += buttonReset_Click;
    // 
    // checkBoxCSS
    // 
    checkBoxCSS.Location = new Point(188, 367);
    checkBoxCSS.Name = "checkBoxCSS";
    checkBoxCSS.Size = new Size(70, 24);
    checkBoxCSS.TabIndex = 9;
    checkBoxCSS.Text = "CS:S";
    checkBoxCSS.UseVisualStyleBackColor = true;
    // 
    // checkBoxHL2
    // 
    checkBoxHL2.Checked = true;
    checkBoxHL2.CheckState = CheckState.Checked;
    checkBoxHL2.Location = new Point(112, 367);
    checkBoxHL2.Name = "checkBoxHL2";
    checkBoxHL2.Size = new Size(70, 24);
    checkBoxHL2.TabIndex = 8;
    checkBoxHL2.Text = "HL2";
    checkBoxHL2.UseVisualStyleBackColor = true;
    // 
    // checkBoxL4D2
    // 
    checkBoxL4D2.Location = new Point(340, 367);
    checkBoxL4D2.Name = "checkBoxL4D2";
    checkBoxL4D2.Size = new Size(70, 24);
    checkBoxL4D2.TabIndex = 11;
    checkBoxL4D2.Text = "L4D2";
    checkBoxL4D2.UseVisualStyleBackColor = true;
    // 
    // checkBoxRecompiled
    // 
    checkBoxRecompiled.Location = new Point(112, 550);
    checkBoxRecompiled.Name = "checkBoxRecompiled";
    checkBoxRecompiled.Size = new Size(150, 24);
    checkBoxRecompiled.TabIndex = 15;
    checkBoxRecompiled.Text = "Recompiled Map(s)";
    checkBoxRecompiled.UseVisualStyleBackColor = true;
    // 
    // checkBoxSCTools
    // 
    checkBoxSCTools.Location = new Point(213, 520);
    checkBoxSCTools.Name = "checkBoxSCTools";
    checkBoxSCTools.Size = new Size(150, 24);
    checkBoxSCTools.TabIndex = 14;
    checkBoxSCTools.Text = "Must Have SC Tools";
    checkBoxSCTools.UseVisualStyleBackColor = true;
    // 
    // checkBoxSubtitle
    // 
    checkBoxSubtitle.Location = new Point(132, 520);
    checkBoxSubtitle.Name = "checkBoxSubtitle";
    checkBoxSubtitle.Size = new Size(75, 24);
    checkBoxSubtitle.TabIndex = 13;
    checkBoxSubtitle.Text = "Subtitle";
    checkBoxSubtitle.UseVisualStyleBackColor = true;
    // 
    // checkBoxTF2
    // 
    checkBoxTF2.Location = new Point(264, 367);
    checkBoxTF2.Name = "checkBoxTF2";
    checkBoxTF2.Size = new Size(70, 24);
    checkBoxTF2.TabIndex = 10;
    checkBoxTF2.Text = "TF2";
    checkBoxTF2.UseVisualStyleBackColor = true;
    // 
    // comboBoxRDDate
    // 
    comboBoxRDDate.AutoCompleteMode = AutoCompleteMode.Suggest;
    comboBoxRDDate.AutoCompleteSource = AutoCompleteSource.ListItems;
    comboBoxRDDate.FormattingEnabled = true;
    comboBoxRDDate.IntegralHeight = false;
    comboBoxRDDate.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" });
    comboBoxRDDate.Location = new Point(112, 83);
    comboBoxRDDate.MaxLength = 2;
    comboBoxRDDate.Name = "comboBoxRDDate";
    comboBoxRDDate.Size = new Size(70, 25);
    comboBoxRDDate.TabIndex = 3;
    // 
    // comboBoxRDMonth
    // 
    comboBoxRDMonth.AutoCompleteMode = AutoCompleteMode.Append;
    comboBoxRDMonth.AutoCompleteSource = AutoCompleteSource.ListItems;
    comboBoxRDMonth.FormattingEnabled = true;
    comboBoxRDMonth.Items.AddRange(new object[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" });
    comboBoxRDMonth.Location = new Point(188, 83);
    comboBoxRDMonth.MaxLength = 3;
    comboBoxRDMonth.Name = "comboBoxRDMonth";
    comboBoxRDMonth.Size = new Size(70, 25);
    comboBoxRDMonth.TabIndex = 4;
    // 
    // comboBoxRDYear
    // 
    comboBoxRDYear.AutoCompleteMode = AutoCompleteMode.Suggest;
    comboBoxRDYear.AutoCompleteSource = AutoCompleteSource.ListItems;
    comboBoxRDYear.FormattingEnabled = true;
    comboBoxRDYear.Items.AddRange(new object[] { "2003", "2004", "2005", "2006", "2007", "2008", "2009", "2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022", "2023", "2024", "2025", "2026", "2027", "2028", "2029", "2030" });
    comboBoxRDYear.Location = new Point(264, 83);
    comboBoxRDYear.MaxLength = 4;
    comboBoxRDYear.Name = "comboBoxRDYear";
    comboBoxRDYear.Size = new Size(70, 25);
    comboBoxRDYear.TabIndex = 5;
    // 
    // fileToolStripMenuItem
    // 
    fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmiOpen, tsmiSave, sepSave, tsmiExit });
    fileToolStripMenuItem.Name = "fileToolStripMenuItem";
    fileToolStripMenuItem.Size = new Size(37, 20);
    fileToolStripMenuItem.Text = "&File";
    // 
    // tsmiOpen
    // 
    tsmiOpen.Name = "tsmiOpen";
    tsmiOpen.ShortcutKeyDisplayString = "Ctrl + O";
    tsmiOpen.ShortcutKeys = Keys.Control | Keys.O;
    tsmiOpen.Size = new Size(154, 22);
    tsmiOpen.Text = "&Open";
    tsmiOpen.Click += tsmiOpen_Click;
    // 
    // tsmiSave
    // 
    tsmiSave.Name = "tsmiSave";
    tsmiSave.ShortcutKeyDisplayString = "Ctrl + S";
    tsmiSave.ShortcutKeys = Keys.Control | Keys.S;
    tsmiSave.Size = new Size(154, 22);
    tsmiSave.Text = "&Save";
    tsmiSave.Click += tsmiSave_Click;
    // 
    // sepSave
    // 
    sepSave.Name = "sepSave";
    sepSave.Size = new Size(151, 6);
    // 
    // tsmiExit
    // 
    tsmiExit.Name = "tsmiExit";
    tsmiExit.ShortcutKeyDisplayString = "Alt + F4";
    tsmiExit.ShortcutKeys = Keys.Alt | Keys.F4;
    tsmiExit.Size = new Size(154, 22);
    tsmiExit.Text = "&Exit";
    tsmiExit.Click += tsmiExit_Click;
    // 
    // groupBoxOption
    // 
    groupBoxOption.Controls.Add(checkBoxCSS);
    groupBoxOption.Controls.Add(checkBoxHL2);
    groupBoxOption.Controls.Add(checkBoxL4D2);
    groupBoxOption.Controls.Add(checkBoxRecompiled);
    groupBoxOption.Controls.Add(checkBoxSCTools);
    groupBoxOption.Controls.Add(checkBoxSubtitle);
    groupBoxOption.Controls.Add(checkBoxTF2);
    groupBoxOption.Controls.Add(comboBoxRDDate);
    groupBoxOption.Controls.Add(comboBoxRDMonth);
    groupBoxOption.Controls.Add(comboBoxRDYear);
    groupBoxOption.Controls.Add(labelAuthor);
    groupBoxOption.Controls.Add(labelDescription);
    groupBoxOption.Controls.Add(labelMapList);
    groupBoxOption.Controls.Add(labelRecommendation);
    groupBoxOption.Controls.Add(labelReleaseDate);
    groupBoxOption.Controls.Add(labelRequirements);
    groupBoxOption.Controls.Add(labelSynopsis);
    groupBoxOption.Controls.Add(labelTitle);
    groupBoxOption.Controls.Add(labelWarning);
    groupBoxOption.Controls.Add(textBoxAuthor);
    groupBoxOption.Controls.Add(textBoxDescription);
    groupBoxOption.Controls.Add(textBoxMapList);
    groupBoxOption.Controls.Add(textBoxSynopsis);
    groupBoxOption.Controls.Add(textBoxTitle);
    groupBoxOption.Controls.Add(textBoxWarning);
    groupBoxOption.Location = new Point(12, 27);
    groupBoxOption.Name = "groupBoxOption";
    groupBoxOption.Size = new Size(477, 691);
    groupBoxOption.TabIndex = 1;
    groupBoxOption.TabStop = false;
    groupBoxOption.Text = "Option";
    // 
    // labelAuthor
    // 
    labelAuthor.Location = new Point(6, 55);
    labelAuthor.Name = "labelAuthor";
    labelAuthor.Size = new Size(100, 22);
    labelAuthor.TabIndex = 0;
    labelAuthor.Text = "Author(s)";
    // 
    // labelDescription
    // 
    labelDescription.Location = new Point(6, 111);
    labelDescription.Name = "labelDescription";
    labelDescription.Size = new Size(100, 22);
    labelDescription.TabIndex = 0;
    labelDescription.Text = "Description";
    // 
    // labelMapList
    // 
    labelMapList.Location = new Point(6, 389);
    labelMapList.Name = "labelMapList";
    labelMapList.Size = new Size(100, 22);
    labelMapList.TabIndex = 0;
    labelMapList.Text = "Map List";
    // 
    // labelRecommendation
    // 
    labelRecommendation.Location = new Point(6, 520);
    labelRecommendation.Name = "labelRecommendation";
    labelRecommendation.Size = new Size(120, 22);
    labelRecommendation.TabIndex = 0;
    labelRecommendation.Text = "Recommendations";
    // 
    // labelReleaseDate
    // 
    labelReleaseDate.Location = new Point(6, 85);
    labelReleaseDate.Name = "labelReleaseDate";
    labelReleaseDate.Size = new Size(100, 22);
    labelReleaseDate.TabIndex = 0;
    labelReleaseDate.Text = "Release Date";
    // 
    // labelRequirements
    // 
    labelRequirements.Location = new Point(6, 367);
    labelRequirements.Name = "labelRequirements";
    labelRequirements.Size = new Size(100, 22);
    labelRequirements.TabIndex = 0;
    labelRequirements.Text = "Requirements";
    // 
    // labelSynopsis
    // 
    labelSynopsis.Location = new Point(6, 239);
    labelSynopsis.Name = "labelSynopsis";
    labelSynopsis.Size = new Size(100, 22);
    labelSynopsis.TabIndex = 0;
    labelSynopsis.Text = "Synopsis";
    // 
    // labelTitle
    // 
    labelTitle.Location = new Point(6, 24);
    labelTitle.Name = "labelTitle";
    labelTitle.Size = new Size(100, 22);
    labelTitle.TabIndex = 0;
    labelTitle.Text = "Title";
    // 
    // labelWarning
    // 
    labelWarning.Location = new Point(6, 550);
    labelWarning.Name = "labelWarning";
    labelWarning.Size = new Size(100, 22);
    labelWarning.TabIndex = 0;
    labelWarning.Text = "Warning";
    // 
    // textBoxAuthor
    // 
    textBoxAuthor.Location = new Point(112, 52);
    textBoxAuthor.Name = "textBoxAuthor";
    textBoxAuthor.Size = new Size(359, 25);
    textBoxAuthor.TabIndex = 2;
    // 
    // textBoxDescription
    // 
    textBoxDescription.Location = new Point(6, 136);
    textBoxDescription.Multiline = true;
    textBoxDescription.Name = "textBoxDescription";
    textBoxDescription.ScrollBars = ScrollBars.Both;
    textBoxDescription.Size = new Size(465, 100);
    textBoxDescription.TabIndex = 6;
    // 
    // textBoxMapList
    // 
    textBoxMapList.Location = new Point(6, 414);
    textBoxMapList.Multiline = true;
    textBoxMapList.Name = "textBoxMapList";
    textBoxMapList.ScrollBars = ScrollBars.Both;
    textBoxMapList.Size = new Size(465, 100);
    textBoxMapList.TabIndex = 12;
    // 
    // textBoxSynopsis
    // 
    textBoxSynopsis.Location = new Point(6, 264);
    textBoxSynopsis.Multiline = true;
    textBoxSynopsis.Name = "textBoxSynopsis";
    textBoxSynopsis.ScrollBars = ScrollBars.Both;
    textBoxSynopsis.Size = new Size(465, 100);
    textBoxSynopsis.TabIndex = 7;
    // 
    // textBoxTitle
    // 
    textBoxTitle.Location = new Point(112, 21);
    textBoxTitle.Name = "textBoxTitle";
    textBoxTitle.Size = new Size(359, 25);
    textBoxTitle.TabIndex = 1;
    // 
    // textBoxWarning
    // 
    textBoxWarning.Location = new Point(6, 580);
    textBoxWarning.Multiline = true;
    textBoxWarning.Name = "textBoxWarning";
    textBoxWarning.ScrollBars = ScrollBars.Both;
    textBoxWarning.Size = new Size(465, 100);
    textBoxWarning.TabIndex = 16;
    // 
    // groupBoxPreview
    // 
    groupBoxPreview.Controls.Add(textBoxPreview);
    groupBoxPreview.Location = new Point(495, 27);
    groupBoxPreview.Name = "groupBoxPreview";
    groupBoxPreview.Size = new Size(477, 691);
    groupBoxPreview.TabIndex = 2;
    groupBoxPreview.TabStop = false;
    groupBoxPreview.Text = "Preview";
    // 
    // textBoxPreview
    // 
    textBoxPreview.BorderStyle = BorderStyle.None;
    textBoxPreview.Location = new Point(6, 21);
    textBoxPreview.Multiline = true;
    textBoxPreview.Name = "textBoxPreview";
    textBoxPreview.ReadOnly = true;
    textBoxPreview.ScrollBars = ScrollBars.Both;
    textBoxPreview.Size = new Size(465, 664);
    textBoxPreview.TabIndex = 17;
    textBoxPreview.WordWrap = false;
    // 
    // menuStrip
    // 
    menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
    menuStrip.Location = new Point(0, 0);
    menuStrip.Name = "menuStrip";
    menuStrip.Size = new Size(984, 24);
    menuStrip.TabIndex = 21;
    menuStrip.Text = "menuStrip1";
    // 
    // openFileDialog
    // 
    openFileDialog.AddToRecent = false;
    openFileDialog.Filter = "JSON 파일 (*.json)|*.json|모든 파일 (*.*)|*.*";
    // 
    // saveFileDialog
    // 
    saveFileDialog.AddToRecent = false;
    saveFileDialog.DefaultExt = "json";
    saveFileDialog.Filter = "JSON 파일 (*.json)|*.json";
    // 
    // MainForm
    // 
    AllowDrop = true;
    AutoScaleDimensions = new SizeF(7F, 17F);
    AutoScaleMode = AutoScaleMode.Font;
    ClientSize = new Size(984, 769);
    Controls.Add(buttonCopy);
    Controls.Add(buttonGenerate);
    Controls.Add(buttonReset);
    Controls.Add(groupBoxOption);
    Controls.Add(groupBoxPreview);
    Controls.Add(menuStrip);
    Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
    FormBorderStyle = FormBorderStyle.FixedDialog;
    KeyPreview = true;
    MainMenuStrip = menuStrip;
    MaximizeBox = false;
    Name = "MainForm";
    StartPosition = FormStartPosition.CenterScreen;
    Text = "Garry's Mod Addon Description Generator";
    DragDrop += MainForm_DragDrop;
    DragEnter += MainForm_DragEnter;
    groupBoxOption.ResumeLayout(false);
    groupBoxOption.PerformLayout();
    groupBoxPreview.ResumeLayout(false);
    groupBoxPreview.PerformLayout();
    menuStrip.ResumeLayout(false);
    menuStrip.PerformLayout();
    ResumeLayout(false);
    PerformLayout();
  }
  #endregion

  private Button buttonCopy;
  private Button buttonGenerate;
  private Button buttonReset;
  private CheckBox checkBoxCSS;
  private CheckBox checkBoxHL2;
  private CheckBox checkBoxL4D2;
  private CheckBox checkBoxRecompiled;
  private CheckBox checkBoxSCTools;
  private CheckBox checkBoxSubtitle;
  private CheckBox checkBoxTF2;
  private ComboBox comboBoxRDDate;
  private ComboBox comboBoxRDMonth;
  private ComboBox comboBoxRDYear;
  private GroupBox groupBoxOption;
  private GroupBox groupBoxPreview;
  private Label labelAuthor;
  private Label labelDescription;
  private Label labelMapList;
  private Label labelRecommendation;
  private Label labelReleaseDate;
  private Label labelRequirements;
  private Label labelSynopsis;
  private Label labelTitle;
  private Label labelWarning;
  private MenuStrip menuStrip;
  private OpenFileDialog openFileDialog;
  private SaveFileDialog saveFileDialog;
  private TextBox textBoxAuthor;
  private TextBox textBoxDescription;
  private TextBox textBoxMapList;
  private TextBox textBoxPreview;
  private TextBox textBoxSynopsis;
  private TextBox textBoxTitle;
  private TextBox textBoxWarning;
  private ToolStripMenuItem fileToolStripMenuItem;
  private ToolStripMenuItem tsmiExit;
  private ToolStripMenuItem tsmiOpen;
  private ToolStripMenuItem tsmiSave;
  private ToolStripSeparator sepSave;
}
