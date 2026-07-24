using System.Windows.Forms;

using HammerLauncher.Common.Resource;

namespace HammerLauncher.UI;

partial class Launcher {
  private System.ComponentModel.IContainer components = null;

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
    components = new System.ComponentModel.Container();
    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
    buttonGModHammer = new Button();
    buttonGModHammerPP = new Button();
    buttonHL2Hammer = new Button();
    buttonHL2HammerPP = new Button();
    pictureBoxGMod = new PictureBox();
    pictureBoxHL2 = new PictureBox();
    statusLabel = new ToolStripStatusLabel();
    statusStrip = new StatusStrip();
    textBoxTarget = new TextBox();
    toolTipGModHammer = new ToolTip(components);
    toolTipGModHammerPP = new ToolTip(components);
    toolTipHL2Hammer = new ToolTip(components);
    toolTipHL2HammerPP = new ToolTip(components);
    ((System.ComponentModel.ISupportInitialize)pictureBoxGMod).BeginInit();
    ((System.ComponentModel.ISupportInitialize)pictureBoxHL2).BeginInit();
    statusStrip.SuspendLayout();
    SuspendLayout();
    // 
    // buttonGModHammer
    // 
    buttonGModHammer.Image = RuntimeResource.HammerGMod_32;
    buttonGModHammer.Location = new System.Drawing.Point(68, 101);
    buttonGModHammer.Name = "buttonGModHammer";
    buttonGModHammer.Size = new System.Drawing.Size(50, 50);
    buttonGModHammer.TabIndex = 3;
    buttonGModHammer.UseVisualStyleBackColor = true;
    buttonGModHammer.Click += buttonGModHammer_Click;
    buttonGModHammer.MouseLeave += buttonGModHammer_MouseLeave;
    buttonGModHammer.MouseHover += buttonGModHammer_MouseHover;
    // 
    // buttonGModHammerPP
    // 
    buttonGModHammerPP.Image = RuntimeResource.HammerHPP_32;
    buttonGModHammerPP.Location = new System.Drawing.Point(124, 101);
    buttonGModHammerPP.Name = "buttonGModHammerPP";
    buttonGModHammerPP.Size = new System.Drawing.Size(50, 50);
    buttonGModHammerPP.TabIndex = 4;
    buttonGModHammerPP.UseVisualStyleBackColor = true;
    buttonGModHammerPP.Click += buttonGModHammerPP_Click;
    buttonGModHammerPP.MouseLeave += buttonGModHammerPP_MouseLeave;
    buttonGModHammerPP.MouseHover += buttonGModHammerPP_MouseHover;
    // 
    // buttonHL2Hammer
    // 
    buttonHL2Hammer.Image = RuntimeResource.HammerHL2_32;
    buttonHL2Hammer.Location = new System.Drawing.Point(68, 45);
    buttonHL2Hammer.Name = "buttonHL2Hammer";
    buttonHL2Hammer.Size = new System.Drawing.Size(50, 50);
    buttonHL2Hammer.TabIndex = 1;
    buttonHL2Hammer.UseVisualStyleBackColor = true;
    buttonHL2Hammer.Click += buttonHL2Hammer_Click;
    buttonHL2Hammer.MouseLeave += buttonHL2Hammer_MouseLeave;
    buttonHL2Hammer.MouseHover += buttonHL2Hammer_MouseHover;
    // 
    // buttonHL2HammerPP
    // 
    buttonHL2HammerPP.Image = RuntimeResource.HammerHPP_32;
    buttonHL2HammerPP.Location = new System.Drawing.Point(124, 45);
    buttonHL2HammerPP.Name = "buttonHL2HammerPP";
    buttonHL2HammerPP.Size = new System.Drawing.Size(50, 50);
    buttonHL2HammerPP.TabIndex = 2;
    buttonHL2HammerPP.UseVisualStyleBackColor = true;
    buttonHL2HammerPP.Click += buttonHL2HammerPP_Click;
    buttonHL2HammerPP.MouseLeave += buttonHL2HammerPP_MouseLeave;
    buttonHL2HammerPP.MouseHover += buttonHL2HammerPP_MouseHover;
    // 
    // pictureBoxGMod
    // 
    pictureBoxGMod.Image = RuntimeResource.GameGMod_32;
    pictureBoxGMod.Location = new System.Drawing.Point(12, 101);
    pictureBoxGMod.Name = "pictureBoxGMod";
    pictureBoxGMod.Size = new System.Drawing.Size(50, 50);
    pictureBoxGMod.SizeMode = PictureBoxSizeMode.CenterImage;
    pictureBoxGMod.TabIndex = 4;
    pictureBoxGMod.TabStop = false;
    // 
    // pictureBoxHL2
    // 
    pictureBoxHL2.Image = RuntimeResource.GameHL2_32;
    pictureBoxHL2.Location = new System.Drawing.Point(12, 45);
    pictureBoxHL2.Name = "pictureBoxHL2";
    pictureBoxHL2.Size = new System.Drawing.Size(50, 50);
    pictureBoxHL2.SizeMode = PictureBoxSizeMode.CenterImage;
    pictureBoxHL2.TabIndex = 1;
    pictureBoxHL2.TabStop = false;
    // 
    // statusLabel
    // 
    statusLabel.DisplayStyle = ToolStripItemDisplayStyle.Text;
    statusLabel.Name = "statusLabel";
    statusLabel.Size = new System.Drawing.Size(107, 17);
    statusLabel.Text = "Hammer Launcher";
    // 
    // statusStrip
    // 
    statusStrip.Items.AddRange(new ToolStripItem[] { statusLabel });
    statusStrip.Location = new System.Drawing.Point(0, 154);
    statusStrip.Name = "statusStrip";
    statusStrip.Size = new System.Drawing.Size(186, 22);
    statusStrip.SizingGrip = false;
    statusStrip.TabIndex = 7;
    // 
    // textBoxTarget
    // 
    textBoxTarget.BorderStyle = BorderStyle.FixedSingle;
    textBoxTarget.Location = new System.Drawing.Point(12, 13);
    textBoxTarget.Margin = new Padding(3, 4, 3, 4);
    textBoxTarget.Name = "textBoxTarget";
    textBoxTarget.ReadOnly = true;
    textBoxTarget.Size = new System.Drawing.Size(162, 25);
    textBoxTarget.TabIndex = 10;
    // 
    // toolTipGModHammer
    // 
    toolTipGModHammer.AutomaticDelay = 100;
    toolTipGModHammer.AutoPopDelay = 5000;
    toolTipGModHammer.InitialDelay = 0;
    toolTipGModHammer.ReshowDelay = 0;
    toolTipGModHammer.ShowAlways = true;
    // 
    // toolTipGModHammerPP
    // 
    toolTipGModHammerPP.AutomaticDelay = 100;
    toolTipGModHammerPP.AutoPopDelay = 5000;
    toolTipGModHammerPP.InitialDelay = 0;
    toolTipGModHammerPP.ReshowDelay = 0;
    toolTipGModHammerPP.ShowAlways = true;
    // 
    // toolTipHL2Hammer
    // 
    toolTipHL2Hammer.AutomaticDelay = 100;
    toolTipHL2Hammer.AutoPopDelay = 5000;
    toolTipHL2Hammer.InitialDelay = 0;
    toolTipHL2Hammer.ReshowDelay = 0;
    toolTipHL2Hammer.ShowAlways = true;
    // 
    // toolTipHL2HammerPP
    // 
    toolTipHL2HammerPP.AutomaticDelay = 100;
    toolTipHL2HammerPP.AutoPopDelay = 5000;
    toolTipHL2HammerPP.InitialDelay = 0;
    toolTipHL2HammerPP.ReshowDelay = 0;
    toolTipHL2HammerPP.ShowAlways = true;
    // 
    // Launcher
    // 
    AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
    AutoScaleMode = AutoScaleMode.Font;
    ClientSize = new System.Drawing.Size(186, 176);
    Controls.Add(statusStrip);
    Controls.Add(buttonGModHammerPP);
    Controls.Add(buttonGModHammer);
    Controls.Add(pictureBoxGMod);
    Controls.Add(buttonHL2HammerPP);
    Controls.Add(buttonHL2Hammer);
    Controls.Add(pictureBoxHL2);
    Controls.Add(textBoxTarget);
    Font = new System.Drawing.Font("Segoe UI", 10F);
    FormBorderStyle = FormBorderStyle.FixedDialog;
    Icon = RuntimeResource.MainIcon;
    Margin = new Padding(3, 4, 3, 4);
    MaximizeBox = false;
    MinimizeBox = false;
    Name = "Launcher";
    StartPosition = FormStartPosition.CenterScreen;
    Text = "Hammer Launcher";
    KeyDown += Launcher_KeyDown;
    ((System.ComponentModel.ISupportInitialize)pictureBoxGMod).EndInit();
    ((System.ComponentModel.ISupportInitialize)pictureBoxHL2).EndInit();
    statusStrip.ResumeLayout(false);
    statusStrip.PerformLayout();
    ResumeLayout(false);
    PerformLayout();
  }
  #endregion

  private Button buttonGModHammer;
  private Button buttonGModHammerPP;
  private Button buttonHL2Hammer;
  private Button buttonHL2HammerPP;
  private PictureBox pictureBoxGMod;
  private PictureBox pictureBoxHL2;
  private StatusStrip statusStrip;
  private TextBox textBoxTarget;
  private ToolStripStatusLabel statusLabel;
  private ToolTip toolTipGModHammer;
  private ToolTip toolTipGModHammerPP;
  private ToolTip toolTipHL2Hammer;
  private ToolTip toolTipHL2HammerPP;
}
