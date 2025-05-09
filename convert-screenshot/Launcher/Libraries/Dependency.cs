#pragma warning disable CRRSP06,CRRSP08

using System;
using System.IO;

namespace Launcher.Libraries;

public static class Dependency {
  private static bool FileExists(string path) {
    string? fullPath = null;
    if (File.Exists(path)) fullPath = Path.GetFullPath(path);
    string env = Environment.GetEnvironmentVariable("PATH");
    if (env == null) {
      return fullPath != null;
    } else {
      foreach (string p in env.Split(Path.PathSeparator)) {
        string pathPath = Path.Combine(p, path);
        if (File.Exists(pathPath)) fullPath = pathPath;
      }
    }
    return fullPath != null;
  }

  public static bool CheckFFmpeg() => FileExists("ffmpeg.exe") || FileExists("ffmpeg");
  public static bool CheckCwebp() => FileExists("cwebp.exe") || FileExists("cwebp");
  public static bool CheckGID() => FileExists("get-image-dimension.exe") || FileExists("get-image-dimension");
  public static bool CheckMagick() => FileExists("magick.exe") || FileExists("magick");
}
