using System.Drawing;

namespace NewFolder.Forms {
  partial class NewFolderForm {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewFolderForm));
      this.pbIcon = new System.Windows.Forms.PictureBox();
      this.lbInfo = new System.Windows.Forms.Label();
      this.tbNewDirName = new System.Windows.Forms.TextBox();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.tbBaseDir = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
      this.SuspendLayout();
      // 
      // pbIcon
      // 
      this.pbIcon.Image = ((System.Drawing.Image)(resources.GetObject("pbIcon.Image")));
      this.pbIcon.Location = new System.Drawing.Point(12, 12);
      this.pbIcon.Name = "pbIcon";
      this.pbIcon.Size = new System.Drawing.Size(32, 32);
      this.pbIcon.TabIndex = 0;
      this.pbIcon.TabStop = false;
      // 
      // lbInfo
      // 
      this.lbInfo.AutoSize = true;
      this.lbInfo.Location = new System.Drawing.Point(50, 12);
      this.lbInfo.Name = "lbInfo";
      this.lbInfo.Size = new System.Drawing.Size(151, 12);
      this.lbInfo.TabIndex = 1;
      this.lbInfo.Text = "Enter name of new folder.";
      // 
      // tbNewDirName
      // 
      this.tbNewDirName.Location = new System.Drawing.Point(12, 63);
      this.tbNewDirName.Name = "tbNewDirName";
      this.tbNewDirName.Size = new System.Drawing.Size(362, 21);
      this.tbNewDirName.TabIndex = 2;
      this.tbNewDirName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(218, 90);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 3;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.ButtonOK_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(299, 90);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(50, 32);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(0, 12);
      this.label1.TabIndex = 5;
      // 
      // tbBaseDir
      // 
      this.tbBaseDir.Location = new System.Drawing.Point(50, 27);
      this.tbBaseDir.Name = "tbBaseDir";
      this.tbBaseDir.ReadOnly = true;
      this.tbBaseDir.Size = new System.Drawing.Size(324, 21);
      this.tbBaseDir.TabIndex = 7;
      this.tbBaseDir.TabStop = false;
      // 
      // NewFolderForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(386, 125);
      this.Controls.Add(this.tbBaseDir);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.tbNewDirName);
      this.Controls.Add(this.lbInfo);
      this.Controls.Add(this.pbIcon);
      this.Icon = global::NewFolder.Resources.Resources.MainIcon;
      this.Name = "NewFolderForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "New Folder";
      ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pbIcon;
    private System.Windows.Forms.Label lbInfo;
    private System.Windows.Forms.TextBox tbNewDirName;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox tbBaseDir;
  }
}
