using System.Windows.Forms;
using HammerLauncher.Properties;

namespace HammerLauncher.Forms {
  partial class Launcher {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.components = new System.ComponentModel.Container();
      this.tbTarget = new System.Windows.Forms.TextBox();
      this.picHL2 = new System.Windows.Forms.PictureBox();
      this.btnHL2H = new System.Windows.Forms.Button();
      this.btnHL2HPP = new System.Windows.Forms.Button();
      this.btnGModHPP = new System.Windows.Forms.Button();
      this.btnGModH = new System.Windows.Forms.Button();
      this.picGMod = new System.Windows.Forms.PictureBox();
      this.statusStrip = new System.Windows.Forms.StatusStrip();
      this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.tooltipHL2H = new System.Windows.Forms.ToolTip(this.components);
      this.tooltipHL2HPP = new System.Windows.Forms.ToolTip(this.components);
      this.tooltipGModH = new System.Windows.Forms.ToolTip(this.components);
      this.tooltipGModHPP = new System.Windows.Forms.ToolTip(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.picHL2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.picGMod)).BeginInit();
      this.statusStrip.SuspendLayout();
      this.SuspendLayout();
      //
      // tbTarget
      //
      this.tbTarget.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.tbTarget.Location = new System.Drawing.Point(12, 13);
      this.tbTarget.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.tbTarget.Name = "tbTarget";
      this.tbTarget.ReadOnly = true;
      this.tbTarget.Size = new System.Drawing.Size(162, 25);
      this.tbTarget.TabIndex = 10;
      //
      // picHL2
      //
      this.picHL2.Image = Resources.game_hl2_32;
      this.picHL2.Location = new System.Drawing.Point(12, 45);
      this.picHL2.Name = "picHL2";
      this.picHL2.Size = new System.Drawing.Size(50, 50);
      this.picHL2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.picHL2.TabIndex = 1;
      this.picHL2.TabStop = false;
      //
      // btnHL2H
      //
      this.btnHL2H.Image = Resources.hammer_hl2_32;
      this.btnHL2H.Location = new System.Drawing.Point(68, 45);
      this.btnHL2H.Name = "btnHL2H";
      this.btnHL2H.Size = new System.Drawing.Size(50, 50);
      this.btnHL2H.TabIndex = 1;
      this.btnHL2H.UseVisualStyleBackColor = true;
      this.btnHL2H.Click += new System.EventHandler(this.btnHL2H_Click);
      this.btnHL2H.MouseLeave += new System.EventHandler(this.btnHL2H_MouseLeave);
      this.btnHL2H.MouseHover += new System.EventHandler(this.btnHL2H_MouseHover);
      //
      // btnHL2HPP
      //
      this.btnHL2HPP.Image = Resources.hammer_hpp_32;
      this.btnHL2HPP.Location = new System.Drawing.Point(124, 45);
      this.btnHL2HPP.Name = "btnHL2HPP";
      this.btnHL2HPP.Size = new System.Drawing.Size(50, 50);
      this.btnHL2HPP.TabIndex = 2;
      this.btnHL2HPP.UseVisualStyleBackColor = true;
      this.btnHL2HPP.Click += new System.EventHandler(this.btnHL2HPP_Click);
      this.btnHL2HPP.MouseLeave += new System.EventHandler(this.btnHL2HPP_MouseLeave);
      this.btnHL2HPP.MouseHover += new System.EventHandler(this.btnHL2HPP_MouseHover);
      //
      // btnGModHPP
      //
      this.btnGModHPP.Image = Resources.hammer_hpp_32;
      this.btnGModHPP.Location = new System.Drawing.Point(124, 101);
      this.btnGModHPP.Name = "btnGModHPP";
      this.btnGModHPP.Size = new System.Drawing.Size(50, 50);
      this.btnGModHPP.TabIndex = 4;
      this.btnGModHPP.UseVisualStyleBackColor = true;
      this.btnGModHPP.Click += new System.EventHandler(this.btnGModHPP_Click);
      this.btnGModHPP.MouseLeave += new System.EventHandler(this.btnGModHPP_MouseLeave);
      this.btnGModHPP.MouseHover += new System.EventHandler(this.btnGModHPP_MouseHover);
      //
      // btnGModH
      //
      this.btnGModH.Image = Resources.hammer_gmod_32;
      this.btnGModH.Location = new System.Drawing.Point(68, 101);
      this.btnGModH.Name = "btnGModH";
      this.btnGModH.Size = new System.Drawing.Size(50, 50);
      this.btnGModH.TabIndex = 3;
      this.btnGModH.UseVisualStyleBackColor = true;
      this.btnGModH.Click += new System.EventHandler(this.btnGModH_Click);
      this.btnGModH.MouseLeave += new System.EventHandler(this.btnGModH_MouseLeave);
      this.btnGModH.MouseHover += new System.EventHandler(this.btnGModH_MouseHover);
      //
      // picGMod
      //
      this.picGMod.Image = Resources.game_gmod_32;
      this.picGMod.Location = new System.Drawing.Point(12, 101);
      this.picGMod.Name = "picGMod";
      this.picGMod.Size = new System.Drawing.Size(50, 50);
      this.picGMod.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.picGMod.TabIndex = 4;
      this.picGMod.TabStop = false;
      //
      // statusStrip
      //
      this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
      this.statusStrip.Location = new System.Drawing.Point(0, 154);
      this.statusStrip.Name = "statusStrip";
      this.statusStrip.Size = new System.Drawing.Size(186, 22);
      this.statusStrip.SizingGrip = false;
      this.statusStrip.TabIndex = 7;
      //
      // statusLabel
      //
      this.statusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.statusLabel.Name = "statusLabel";
      this.statusLabel.Size = new System.Drawing.Size(107, 17);
      this.statusLabel.Text = "Hammer Launcher";
      //
      // tooltipHL2H
      //
      this.tooltipHL2H.AutomaticDelay = 100;
      this.tooltipHL2H.AutoPopDelay = 5000;
      this.tooltipHL2H.InitialDelay = 0;
      this.tooltipHL2H.ReshowDelay = 0;
      this.tooltipHL2H.ShowAlways = true;
      //
      // tooltipHL2HPP
      //
      this.tooltipHL2HPP.AutomaticDelay = 100;
      this.tooltipHL2HPP.AutoPopDelay = 5000;
      this.tooltipHL2HPP.InitialDelay = 0;
      this.tooltipHL2HPP.ReshowDelay = 0;
      this.tooltipHL2HPP.ShowAlways = true;
      //
      // tooltipGModH
      //
      this.tooltipGModH.AutomaticDelay = 100;
      this.tooltipGModH.AutoPopDelay = 5000;
      this.tooltipGModH.InitialDelay = 0;
      this.tooltipGModH.ReshowDelay = 0;
      this.tooltipGModH.ShowAlways = true;
      //
      // tooltipGModHPP
      //
      this.tooltipGModHPP.AutomaticDelay = 100;
      this.tooltipGModHPP.AutoPopDelay = 5000;
      this.tooltipGModHPP.InitialDelay = 0;
      this.tooltipGModHPP.ReshowDelay = 0;
      this.tooltipGModHPP.ShowAlways = true;
      //
      // Launcher
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(186, 176);
      this.Controls.Add(this.statusStrip);
      this.Controls.Add(this.btnGModHPP);
      this.Controls.Add(this.btnGModH);
      this.Controls.Add(this.picGMod);
      this.Controls.Add(this.btnHL2HPP);
      this.Controls.Add(this.btnHL2H);
      this.Controls.Add(this.picHL2);
      this.Controls.Add(this.tbTarget);
      this.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = Resources.MainIcon;
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Launcher";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Hammer Launcher";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Launcher_KeyDown);
      ((System.ComponentModel.ISupportInitialize)(this.picHL2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.picGMod)).EndInit();
      this.statusStrip.ResumeLayout(false);
      this.statusStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tbTarget;
    private System.Windows.Forms.PictureBox picHL2;
    private System.Windows.Forms.Button btnHL2H;
    private System.Windows.Forms.Button btnHL2HPP;
    private System.Windows.Forms.PictureBox picGMod;
    private System.Windows.Forms.Button btnGModH;
    private System.Windows.Forms.Button btnGModHPP;
    private System.Windows.Forms.StatusStrip statusStrip;
    private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    private System.Windows.Forms.ToolTip tooltipHL2H;
    private System.Windows.Forms.ToolTip tooltipHL2HPP;
    private System.Windows.Forms.ToolTip tooltipGModH;
    private System.Windows.Forms.ToolTip tooltipGModHPP;
  }
}
