using NewFolder.Forms;
using System;
using System.IO;
using System.Windows.Forms;

namespace NewFolder;

internal static class Program {
  private static readonly string defaultName = "새 폴더";

  [STAThread]
  private static void Main() {
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);

    try {
      string basePath = Environment.GetCommandLineArgs().Length > 1 ? Environment.GetCommandLineArgs()[1] : Environment.CurrentDirectory;
      basePath = Path.GetFullPath(basePath);

      string newFolderName = GetAvailableDirectoryName(basePath);
      Application.Run(new NewFolderForm(basePath, newFolderName));
    } catch (Exception ex) {
      MessageBox.Show(ex.ToString(), ex.GetType().FullName);
      Application.Exit();
    }
  }

  private static string GetAvailableDirectoryName(string basePath) {
    string candidate = Path.Combine(basePath, defaultName);
    if (!Directory.Exists(candidate)) return defaultName;

    for (int i = 2; i < int.MaxValue; i++) {
      string name = $"{defaultName} ({i})";
      string path = Path.Combine(basePath, name);
      if (!Directory.Exists(path)) return name;
    }

    throw new InvalidOperationException("Could not find an available folder name.");
  }
}
