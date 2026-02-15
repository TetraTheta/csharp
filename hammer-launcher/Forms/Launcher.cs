using HammerLauncher.Libraries;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DarkModeForms;
using HammerLauncher.Properties;

namespace HammerLauncher.Forms {
public partial class Launcher : Form {
  private readonly string _hl2H = Hammer.GetHl2H();
  private readonly string _hl2Hpp = Hammer.GetHl2Hpp();
  private readonly string _gmodh = Hammer.GetGModH();
  private readonly string _gmodhpp = Hammer.GetGModHpp();

  private readonly string _filePath = string.Empty;

  public Launcher(string file) {
    InitializeComponent();
    DarkModeHelper.Apply(this);

    // Set Target
    if (!string.IsNullOrWhiteSpace(file)) {
      _filePath = Path.GetFullPath(file);
      tbTarget.Text = _filePath;
      tbTarget.SelectEnd();
    }

    // Check HL2
    if (string.IsNullOrWhiteSpace(SteamRegistry.GetHl2InstallPath())) DisableHl2();

    // Check HL2 Hammer
    if (string.IsNullOrWhiteSpace(_hl2H)) btnHL2H.Enabled = false;

    // Check HL2 Hammer++
    if (string.IsNullOrWhiteSpace(_hl2Hpp)) btnHL2HPP.Enabled = false;

    // Check GMod
    if (string.IsNullOrWhiteSpace(SteamRegistry.GetGModInstallPath())) DisableGMod();

    // Check GMod Hammer
    if (string.IsNullOrWhiteSpace(_gmodh)) btnGModH.Enabled = false;

    // Check GMod Hammer++
    if (string.IsNullOrWhiteSpace(_gmodhpp)) btnGModHPP.Enabled = false;

    // Select Form itself
    Select();
  }

  private void DisableHl2() {
    picHL2.Image = Resources.game_hl2_gray_32;
    btnHL2H.Enabled = false;
    btnHL2HPP.Enabled = false;
  }

  private void DisableGMod() {
    picGMod.Image = Resources.game_gmod_gray_32;
    btnGModH.Enabled = false;
    btnGModHPP.Enabled = false;
  }

  private void btnHL2H_Click(object sender, System.EventArgs e) {
    if (string.IsNullOrWhiteSpace(_filePath)) { Process.Start(_hl2H); } else { Process.Start(_hl2H, $"\"{Path.GetFullPath(_filePath)}\""); }

    Application.Exit();
  }

  private void btnHL2HPP_Click(object sender, System.EventArgs e) {
    if (string.IsNullOrWhiteSpace(_filePath)) { Process.Start(_hl2Hpp); } else { Process.Start(_hl2Hpp, $"\"{Path.GetFullPath(_filePath)}\""); }

    Application.Exit();
  }

  private void btnGModH_Click(object sender, System.EventArgs e) {
    if (string.IsNullOrWhiteSpace(_filePath)) { Process.Start(_gmodh); } else { Process.Start(_gmodh, $"\"{Path.GetFullPath(_filePath)}\""); }

    Application.Exit();
  }

  private void btnGModHPP_Click(object sender, System.EventArgs e) {
    if (string.IsNullOrWhiteSpace(_filePath)) { Process.Start(_gmodhpp); } else { Process.Start(_gmodhpp, $"\"{Path.GetFullPath(_filePath)}\""); }

    Application.Exit();
  }

  private void btnHL2H_MouseHover(object sender, System.EventArgs e) {
    tooltipHL2H.SetToolTip(btnHL2H, "Half-Life 2 Hammer");
    // ReSharper disable once LocalizableElement
    statusLabel.Text = "Half-Life 2 Hammer";
  }

  private void btnHL2H_MouseLeave(object sender, System.EventArgs e) {
    tooltipHL2H.Hide(btnHL2H);
    statusLabel.Text = string.Empty;
  }

  private void btnHL2HPP_MouseHover(object sender, System.EventArgs e) {
    tooltipHL2HPP.SetToolTip(btnHL2HPP, "Half-Life 2 Hammer++");
    // ReSharper disable once LocalizableElement
    statusLabel.Text = "Half-Life 2 Hammer++";
  }

  private void btnHL2HPP_MouseLeave(object sender, System.EventArgs e) {
    tooltipHL2HPP.Hide(btnHL2HPP);
    statusLabel.Text = string.Empty;
  }

  private void btnGModH_MouseHover(object sender, System.EventArgs e) {
    tooltipGModH.SetToolTip(btnGModH, "Garry's Mod Hammer");
    // ReSharper disable once LocalizableElement
    statusLabel.Text = "Garry's Mod Hammer";
  }

  private void btnGModH_MouseLeave(object sender, System.EventArgs e) {
    tooltipGModH.Hide(btnGModH);
    statusLabel.Text = string.Empty;
  }

  private void btnGModHPP_MouseHover(object sender, System.EventArgs e) {
    tooltipGModHPP.SetToolTip(btnGModHPP, "Garry's Mod Hammer++");
    // ReSharper disable once LocalizableElement
    statusLabel.Text = "Garry's Mod Hammer++";
  }

  private void btnGModHPP_MouseLeave(object sender, System.EventArgs e) {
    tooltipGModHPP.Hide(btnGModHPP);
    statusLabel.Text = string.Empty;
  }

  private void Launcher_KeyDown(object sender, KeyEventArgs e) {
    // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
    // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
    switch (e.KeyCode) {
      case Keys.D1:
      case Keys.NumPad1:
        btnHL2H.PerformClick();
        break;
      case Keys.D2:
      case Keys.NumPad2:
        btnHL2HPP.PerformClick();
        break;
      case Keys.D3:
      case Keys.NumPad3:
        btnGModH.PerformClick();
        break;
      case Keys.D4:
      case Keys.NumPad4:
        btnGModHPP.PerformClick();
        break;
    }
  }
}
}
