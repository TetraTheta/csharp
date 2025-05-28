using DarkModeForms;
using System;
using System.IO;
using System.Windows.Forms;

namespace NewFolder.Forms {
  public partial class NewFolderForm : Form {
    private readonly DarkModeCS dm;
    private readonly string baseDirectory;

    public NewFolderForm(string baseDirectory, string newName) {
      this.baseDirectory = baseDirectory;

      InitializeComponent();
      dm = new DarkModeCS(this);

      tbBaseDir.Text = baseDirectory;

      tbNewDirName.Text = newName;
      tbNewDirName.SelectAll();
    }

    private void TextBox_KeyDown(object sender, KeyEventArgs e) {
      if (e.KeyCode == Keys.Enter) {
        btnOK.PerformClick();
      } else if (e.KeyCode == Keys.Escape) {
        btnCancel.PerformClick();
      }
    }

    private void ButtonOK_Click(object sender, EventArgs e) {
      try {
        string path = Path.GetFullPath(Path.Combine(baseDirectory, tbNewDirName.Text));
        if (Directory.Exists(path)) throw new ApplicationException("이미 존재하는 경로입니다.");
        Directory.CreateDirectory(path);
        Application.Exit();
      } catch (Exception ex) {
        MessageBox.Show(ex.ToString(), ex.GetType().FullName);
        Application.Exit();
      }
    }

    private void ButtonCancel_Click(object sender, EventArgs e) {
      Application.Exit();
    }
  }
}
