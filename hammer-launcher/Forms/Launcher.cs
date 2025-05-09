using DarkModeForms;
using HammerLauncher.Libraries;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace HammerLauncher.Forms;

public partial class Launcher : Form {
  private readonly DarkModeCS dm;

  private readonly string hl2h = Hammer.GetHL2H();
  private readonly string hl2hpp = Hammer.GetHL2HPP();
  private readonly string gmodh = Hammer.GetGModH();
  private readonly string gmodhpp = Hammer.GetGModHPP();

  private readonly string filePath = string.Empty;

  public Launcher(string file) {
    InitializeComponent();
    dm = new DarkModeCS(this);

    // Set Target
    if (!string.IsNullOrWhiteSpace(file)) {
      filePath = Path.GetFullPath(file);
      tbTarget.Text = filePath;
      tbTarget.SelectEnd();
    }

    // Check HL2
    if (string.IsNullOrWhiteSpace(Registry.GetHL2InstallPath())) DisableHL2();

    // Check HL2 Hammer
    if (string.IsNullOrWhiteSpace(hl2h)) btnHL2H.Enabled = false;

    // Check HL2 Hammer++
    if (string.IsNullOrWhiteSpace(hl2hpp)) btnHL2HPP.Enabled = false;

    // Check GMod
    if (string.IsNullOrWhiteSpace(Registry.GetGModInstallPath())) DisableGMod();

    // Check GMod Hammer
    if (string.IsNullOrWhiteSpace(gmodh)) btnGModH.Enabled = false;

    // Check GMod Hammer++
    if (string.IsNullOrWhiteSpace(gmodhpp)) btnGModHPP.Enabled = false;

    // Select Form itself
    Select();
  }

  private void DisableHL2() {
    picHL2.Image = Resources.Resources.game_hl2_gray_32;
    btnHL2H.Enabled = false;
    btnHL2HPP.Enabled = false;
  }

  private void DisableGMod() {
    picGMod.Image = Resources.Resources.game_gmod_gray_32;
    btnGModH.Enabled = false;
    btnGModHPP.Enabled = false;
  }

  private void btnHL2H_Click(object sender, System.EventArgs e) {
    if (string.IsNullOrWhiteSpace(filePath)) {
      Process.Start(hl2h);
    } else {
      Process.Start(hl2h, $"\"{Path.GetFullPath(filePath)}\"");
    }
    Application.Exit();
  }

  private void btnHL2HPP_Click(object sender, System.EventArgs e) {
    if (string.IsNullOrWhiteSpace(filePath)) {
      Process.Start(hl2hpp);
    } else {
      Process.Start(hl2hpp, $"\"{Path.GetFullPath(filePath)}\"");
    }
    Application.Exit();
  }

  private void btnGModH_Click(object sender, System.EventArgs e) {
    if (string.IsNullOrWhiteSpace(filePath)) {
      Process.Start(gmodh);
    } else {
      Process.Start(gmodh, $"\"{Path.GetFullPath(filePath)}\"");
    }
    Application.Exit();
  }

  private void btnGModHPP_Click(object sender, System.EventArgs e) {
    if (string.IsNullOrWhiteSpace(filePath)) {
      Process.Start(gmodhpp);
    } else {
      Process.Start(gmodhpp, $"\"{Path.GetFullPath(filePath)}\"");
    }
    Application.Exit();
  }

  private void btnHL2H_MouseHover(object sender, System.EventArgs e) {
    tooltipHL2H.SetToolTip(btnHL2H, "Half-Life 2 Hammer");
    statusLabel.Text = "Half-Life 2 Hammer";
  }

  private void btnHL2H_MouseLeave(object sender, System.EventArgs e) {
    tooltipHL2H.Hide(btnHL2H);
    statusLabel.Text = string.Empty;
  }

  private void btnHL2HPP_MouseHover(object sender, System.EventArgs e) {
    tooltipHL2HPP.SetToolTip(btnHL2HPP, "Half-Life 2 Hammer++");
    statusLabel.Text = "Half-Life 2 Hammer++";
  }

  private void btnHL2HPP_MouseLeave(object sender, System.EventArgs e) {
    tooltipHL2HPP.Hide(btnHL2HPP);
    statusLabel.Text = string.Empty;
  }

  private void btnGModH_MouseHover(object sender, System.EventArgs e) {
    tooltipGModH.SetToolTip(btnGModH, "Garry's Mod Hammer");
    statusLabel.Text = "Garry's Mod Hammer";
  }

  private void btnGModH_MouseLeave(object sender, System.EventArgs e) {
    tooltipGModH.Hide(btnGModH);
    statusLabel.Text = string.Empty;
  }

  private void btnGModHPP_MouseHover(object sender, System.EventArgs e) {
    tooltipGModHPP.SetToolTip(btnGModHPP, "Garry's Mod Hammer++");
    statusLabel.Text = "Garry's Mod Hammer++";
  }

  private void btnGModHPP_MouseLeave(object sender, System.EventArgs e) {
    tooltipGModHPP.Hide(btnGModHPP);
    statusLabel.Text = string.Empty;
  }
}
