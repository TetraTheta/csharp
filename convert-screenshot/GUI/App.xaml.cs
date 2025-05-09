using System;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI;

public partial class App : Application {
  private const string PipeServerName = "ConvertScreenshotPipe";

  private async void OnAppLaunchAsync(object sender, StartupEventArgs e) {
    //MessageBox.Show("GUI Started!");

    try {
      using var pipe = new NamedPipeClientStream(".", PipeServerName, PipeDirection.In);
      await Task.Run(() => pipe.Connect(5000)).ConfigureAwait(true);

      byte[] buffer = new byte[4096];
      int bytesRead = pipe.Read(buffer, 0, buffer.Length);

      if (bytesRead > 0) {
        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        MessageBox.Show($"Received:\n{receivedData}");
      } else {
        MessageBox.Show("No data received");
      }
    } catch (Exception ex) {
      MessageBox.Show($"Error: {ex.Message}");
    } finally {
      Shutdown();
    }
  }
}
