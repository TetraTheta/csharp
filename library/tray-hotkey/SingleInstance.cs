using System;
using System.IO;
using System.IO.Pipes;
using System.Media;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace TrayHotkey;
public static class SingleInstance {
  private static readonly string AppId = Assembly.GetEntryAssembly()!.GetName().Name!;
  private static readonly string MutexName = $"{AppId}_SingleInstanceMutex";
  private static readonly string PipeName = $"{AppId}_SingleInstancePipe";
  private static readonly string ThreadName = $"{AppId}_SingleInstancePipeServer";

  private static Mutex? mut;
  private static Thread? pipeServerThread;

  public static void Ensure() {
    bool isFirstInstance;
    mut = new(true, MutexName, out isFirstInstance);

    if (!isFirstInstance) {
      SignalExistingInstance();
      try {
        mut.WaitOne(TimeSpan.FromMilliseconds(300));
      } catch (AbandonedMutexException) { } // ignore
    }

    Application.ApplicationExit += (s, e) => {
      try { mut?.ReleaseMutex(); } catch { } // swallow
    };

    StartPipeServer();
  }

  private static void SignalExistingInstance() {
    try {
      using NamedPipeClientStream client = new(".", PipeName, PipeDirection.Out);
      client.Connect(300);
      using StreamWriter writer = new(client) { AutoFlush = true };
      writer.WriteLine("exit");
    } catch { } // pipe is not ready: ignore
  }

  private static void StartPipeServer() {
    pipeServerThread = new(() => {
      try {
        using NamedPipeServerStream server = new(PipeName, PipeDirection.In, 1, PipeTransmissionMode.Message);
        server.WaitForConnection();
        using StreamReader reader = new(server);
        string msg = reader.ReadLine();
        if (msg == "exit") {
          SystemSounds.Beep.Play();
          Application.Exit();
        }
      } catch { } // swallow
    }) {
      IsBackground = true,
      Name = ThreadName
    };
    pipeServerThread.Start();
  }
}
