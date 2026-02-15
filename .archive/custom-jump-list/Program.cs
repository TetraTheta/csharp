using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Shell;
using CommandLine;
using CommandLine.Text;
using Application = System.Windows.Application;

namespace CustomJumpList {
internal static class Program {
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  private class Options {
    [Option('r', "register", Default = false, HelpText = "Do not launch Windows Explorer, only register Jump List")]
    public bool Register { get; set; }
  }

  private static void DisplayHelp<T>(ParserResult<T> result, IEnumerable<Error> errs) {
    HelpText helpText = errs.IsVersion() ? HelpText.AutoBuild(result, maxDisplayWidth: Console.WindowWidth) : HelpText.AutoBuild(result, h => HelpText.DefaultParsingErrorsHandler(result, h), e => e, true, maxDisplayWidth: Console.WindowWidth);

    Console.WriteLine(helpText);
  }

  [STAThread]
  private static void Main(string[] args) {
    var result = new Parser(c => {
        c.HelpWriter = null;
        c.CaseSensitive = false;
        c.CaseInsensitiveEnumValues = true;
      }
    ).ParseArguments(args, typeof(Options));
    result.WithParsed<Options>(opt => {
        if (!opt.Register) {
          ProcessStartInfo processStartInfo = new ProcessStartInfo() {
            FileName = Environment.ExpandEnvironmentVariables(@"%WinDir%\explorer.exe")
          };
          Process.Start(processStartInfo);
        }

        Application app = new Application();

        JumpList jl = new JumpList();

        JumpTask taskWebNewWindow = new JumpTask {
          ApplicationPath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe",
          Arguments = @"--window-size=1385,936",
          CustomCategory = @"웹 브라우저",
          Description = @"웹 브라우저의 새 창을 엽니다",
          IconResourceIndex = 0,
          IconResourcePath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe",
          Title = @"새 창",
        };

        JumpTask taskWebNewIncognitoWindow = new JumpTask() {
          ApplicationPath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe",
          Arguments = @"--window-size=1385,936 --incognito",
          CustomCategory = @"웹 브라우저",
          Description = @"웹 브라우저의 새 시크릿 창을 엽니다",
          IconResourceIndex = 1,
          IconResourcePath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe",
          Title = @"새 시크릿 창",
        };

        JumpTask taskFileExplorer = new JumpTask() {
          ApplicationPath = Environment.ExpandEnvironmentVariables(@"%WinDir%\explorer.exe"),
          CustomCategory = @"파일 탐색기",
          Description = @"Windows 탐색기의 새 창을 엽니다",
          IconResourceIndex = 0,
          IconResourcePath = Environment.ExpandEnvironmentVariables(@"%WinDir%\explorer.exe"),
          Title = @"파일 탐색기",
        };

        JumpTask taskGameWwmm = new JumpTask() {
          ApplicationPath = @"D:\WuWaMod\JASM - Just Another Skin Manager.exe",
          CustomCategory = @"게임",
          Description = @"Wuthering Waves 모드 매니저를 실행합니다",
          IconResourceIndex = 0,
          IconResourcePath = @"D:\WuWaMod\JASM - Just Another Skin Manager.exe",
          Title = @"JASM",
        };

        jl.JumpItems.Add(taskGameWwmm);
        jl.JumpItems.Add(taskFileExplorer);
        jl.JumpItems.Add(taskWebNewWindow);
        jl.JumpItems.Add(taskWebNewIncognitoWindow);

        JumpList.SetJumpList(Application.Current, jl);
        jl.Apply();

        app.Shutdown();
      }
    );
    result.WithNotParsed(errs => DisplayHelp(result, errs));
  }
}
}
