using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using HammerLauncher.Libraries;
using HammerLauncher.Libraries.Extension;

namespace HammerLauncher.Forms;

public partial class Launcher : Form {
  private readonly string _HL2Hammer = Hammer.GetHL2Hammer();
  private readonly string _HL2HammerPP = Hammer.GetHL2HammerPP();
  private readonly string _GModHammer = Hammer.GetGModHammer();
  private readonly string _GModHammerPP = Hammer.GetGModHammerPP();

  private readonly string _filePath = string.Empty;

  public Launcher(string file) {
    InitializeComponent();

    // Set Target
    if (!string.IsNullOrWhiteSpace(file)) {
      _filePath = Path.GetFullPath(file);
      textBoxTarget.Text = _filePath;
      textBoxTarget.SelectEnd();
    }

    // Check HL2
    if (string.IsNullOrWhiteSpace(SteamRegistry.GetHL2InstallPath())) DisableHl2();

    // Check HL2 Hammer
    if (string.IsNullOrWhiteSpace(_HL2Hammer)) buttonHL2Hammer.Enabled = false;

    // Check HL2 Hammer++
    if (string.IsNullOrWhiteSpace(_HL2HammerPP)) buttonHL2HammerPP.Enabled = false;

    // Check GMod
    if (string.IsNullOrWhiteSpace(SteamRegistry.GetGModInstallPath())) DisableGMod();

    // Check GMod Hammer
    if (string.IsNullOrWhiteSpace(_GModHammer)) buttonGModHammer.Enabled = false;

    // Check GMod Hammer++
    if (string.IsNullOrWhiteSpace(_GModHammerPP)) buttonGModHammerPP.Enabled = false;

    // Select Form itself
    Select();
  }

  private void DisableHl2() {
    pictureBoxHL2.Image = RuntimeResources.GameHL2Gray_32;
    buttonHL2Hammer.Enabled = false;
    buttonHL2HammerPP.Enabled = false;
  }

  private void DisableGMod() {
    pictureBoxGMod.Image = RuntimeResources.GameGModGray_32;
    buttonGModHammer.Enabled = false;
    buttonGModHammerPP.Enabled = false;
  }

  private void buttonHL2Hammer_Click(object sender, System.EventArgs e) {
    _ = string.IsNullOrWhiteSpace(_filePath) ? Process.Start(_HL2Hammer) : Process.Start(_HL2Hammer, $"\"{Path.GetFullPath(_filePath)}\"");
    Application.Exit();
  }

  private void buttonHL2HammerPP_Click(object sender, System.EventArgs e) {
    _ = string.IsNullOrWhiteSpace(_filePath) ? Process.Start(_HL2HammerPP) : Process.Start(_HL2HammerPP, $"\"{Path.GetFullPath(_filePath)}\"");
    Application.Exit();
  }

  private void buttonGModHammer_Click(object sender, System.EventArgs e) {
    _ = string.IsNullOrWhiteSpace(_filePath) ? Process.Start(_GModHammer) : Process.Start(_GModHammer, $"\"{Path.GetFullPath(_filePath)}\"");
    Application.Exit();
  }

  private void buttonGModHammerPP_Click(object sender, System.EventArgs e) {
    _ = string.IsNullOrWhiteSpace(_filePath) ? Process.Start(_GModHammerPP) : Process.Start(_GModHammerPP, $"\"{Path.GetFullPath(_filePath)}\"");
    Application.Exit();
  }

  private void buttonHL2Hammer_MouseHover(object sender, System.EventArgs e) {
    toolTipHL2Hammer.SetToolTip(buttonHL2Hammer, "Half-Life 2 Hammer");
    statusLabel.Text = "Half-Life 2 Hammer";
  }

  private void buttonHL2Hammer_MouseLeave(object sender, System.EventArgs e) {
    toolTipHL2Hammer.Hide(buttonHL2Hammer);
    statusLabel.Text = string.Empty;
  }

  private void buttonHL2HammerPP_MouseHover(object sender, System.EventArgs e) {
    toolTipHL2HammerPP.SetToolTip(buttonHL2HammerPP, "Half-Life 2 Hammer++");
    statusLabel.Text = "Half-Life 2 Hammer++";
  }

  private void buttonHL2HammerPP_MouseLeave(object sender, System.EventArgs e) {
    toolTipHL2HammerPP.Hide(buttonHL2HammerPP);
    statusLabel.Text = string.Empty;
  }

  private void buttonGModHammer_MouseHover(object sender, System.EventArgs e) {
    toolTipGModHammer.SetToolTip(buttonGModHammer, "Garry's Mod Hammer");
    statusLabel.Text = "Garry's Mod Hammer";
  }

  private void buttonGModHammer_MouseLeave(object sender, System.EventArgs e) {
    toolTipGModHammer.Hide(buttonGModHammer);
    statusLabel.Text = string.Empty;
  }

  private void buttonGModHammerPP_MouseHover(object sender, System.EventArgs e) {
    toolTipGModHammerPP.SetToolTip(buttonGModHammerPP, "Garry's Mod Hammer++");
    statusLabel.Text = "Garry's Mod Hammer++";
  }

  private void buttonGModHammerPP_MouseLeave(object sender, System.EventArgs e) {
    toolTipGModHammerPP.Hide(buttonGModHammerPP);
    statusLabel.Text = string.Empty;
  }

  private void Launcher_KeyDown(object sender, KeyEventArgs e) {
    switch (e.KeyCode) {
      case Keys.D1:
      case Keys.NumPad1:
        buttonHL2Hammer.PerformClick();
        break;
      case Keys.D2:
      case Keys.NumPad2:
        buttonHL2HammerPP.PerformClick();
        break;
      case Keys.D3:
      case Keys.NumPad3:
        buttonGModHammer.PerformClick();
        break;
      case Keys.D4:
      case Keys.NumPad4:
        buttonGModHammerPP.PerformClick();
        break;
      default:
        break;
    }
  }
}
