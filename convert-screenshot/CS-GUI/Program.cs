using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using common;
using Newtonsoft.Json;

namespace CS_GUI;

internal static class Program {
  private static readonly string[] imgExt = [".bmp", ".jpeg", ".jpg", ".png", ".webp"];

  [STAThread]
  private static void Main() {
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);

    // receive data from ConvertScreenshot
    JobData jobData;
    using StreamReader r = new(Console.OpenStandardInput());
    jobData = JsonConvert.DeserializeObject<JobData>(r.ReadToEnd())!;

    // check JobData.Target
    string rootPath = jobData.Target;
    List<string> rootFiles = Directory.Exists(rootPath) ? [.. Directory.EnumerateFiles(rootPath).Where(f => imgExt.Contains(Path.GetExtension(f).ToLower()))] : [];
    if (rootFiles.Count == 0) {
      MessageBox.Show($"There is no image files in '{jobData.Target}'");
      return;
    }


    MessageBox.Show(jobData.ToString());
    //Application.Run();
  }
}
