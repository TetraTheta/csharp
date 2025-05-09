using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pathed.Libraries {
  [Verb("append", HelpText = "Append variable to environment variable")]
  public class AppendOptions {
    [Value(0, MetaName = "VAR", HelpText = "New variable to add to the end of the environment variable", Required = true)]
    public string Value { get; set; }

    [Option('e', "envvar", MetaValue = "ENVVAR", HelpText = "Name of the environment variable", Default = "PATH")]
    public string Key { get; set; }

    [Option('t', "target", MetaValue = "TARGET", Default = EnvironmentVariableTarget.User)]
    public EnvironmentVariableTarget Target { get; set; }

    // This is for named pipe
    [Option('p', "pipe", Default = "", Hidden = true)]
    public string Pipe { get; set; }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Key: {Key}");
      sb.AppendLine($"Value: {Value}");
      sb.AppendLine($"Target: {Target}");
      sb.AppendLine($"Pipe: {Pipe}");
      return sb.ToString();
    }

    [Usage(ApplicationAlias = "Pathed.exe")]
    public static IEnumerable<Example> Examples {
      get {
        yield return new Example("Append '%LocalAppData%\\Program\\MyProgram' to 'PATH' in User", new AppendOptions { Value = @"%LocalAppData%\Program\MyProgram", Key = "PATH" });
        yield return new Example("Append 'C:\\Windows\\System32' to 'MyPath' in Machine", new AppendOptions { Value = @"C:\Windows\System32", Key = "MyPath", Target = EnvironmentVariableTarget.Machine });
      }
    }
  }

  [Verb("prepend", HelpText = "Prepend variable to environment variable")]
  public class PrependOptions {
    [Value(0, MetaName = "VAR", HelpText = "New variable to add to the front of the environment variable", Required = true)]
    public string Value { get; set; }

    [Option('e', "envvar", MetaValue = "ENVVAR", HelpText = "Name of the environment variable", Default = "PATH")]
    public string Key { get; set; }

    [Option('t', "target", MetaValue = "TARGET", Default = EnvironmentVariableTarget.User)]
    public EnvironmentVariableTarget Target { get; set; }

    // This is for named pipe
    [Option('p', "pipe", Default = "", Hidden = true)]
    public string Pipe { get; set; }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Key: {Key}");
      sb.AppendLine($"Value: {Value}");
      sb.AppendLine($"Target: {Target}");
      sb.AppendLine($"Pipe: {Pipe}");
      return sb.ToString();
    }

    [Usage(ApplicationAlias = "Pathed.exe")]
    public static IEnumerable<Example> Examples {
      get {
        yield return new Example("Prepend '%LocalAppData%\\Program\\MyProgram' to 'PATH' in User", new PrependOptions { Value = @"%LocalAppData%\Program\MyProgram", Key = "PATH" });
        yield return new Example("Prepend 'C:\\Windows\\System32' to 'MyPath' in Machine", new PrependOptions { Value = @"C:\Windows\System32", Key = "MyPath", Target = EnvironmentVariableTarget.Machine });
      }
    }
  }

  [Verb("remove", HelpText = "Remove variable from environment variable")]
  public class RemoveOptions {
    [Value(0, MetaName = "VAR", HelpText = "Variable to remove from the environment variable", Required = true)]
    public string Value { get; set; }

    [Option('e', "envvar", MetaValue = "ENVVAR", HelpText = "Name of the environment variable", Default = "PATH")]
    public string Key { get; set; }

    [Option('t', "target", MetaValue = "TARGET", Default = EnvironmentVariableTarget.User)]
    public EnvironmentVariableTarget Target { get; set; }

    // This is for named pipe
    [Option('p', "pipe", Default = "", Hidden = true)]
    public string Pipe { get; set; }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Key: {Key}");
      sb.AppendLine($"Value: {Value}");
      sb.AppendLine($"Target: {Target}");
      sb.AppendLine($"Pipe: {Pipe}");
      return sb.ToString();
    }

    [Usage(ApplicationAlias = "Pathed.exe")]
    public static IEnumerable<Example> Examples {
      get {
        yield return new Example("Remove '%LocalAppData%\\Program\\MyProgram' from 'PATH' in User", new RemoveOptions { Value = @"%LocalAppData%\Program\MyProgram", Key = "PATH" });
        yield return new Example("Remove 'C:\\Windows\\System32' from 'MyPath' in Machine", new RemoveOptions { Value = @"C:\Windows\System32", Key = "MyPath", Target = EnvironmentVariableTarget.Machine });
      }
    }
  }

  [Verb("show", isDefault: true, HelpText = "Print environment variable as a list")]
  public class ShowOptions {
    [Option('e', "envvar", MetaValue = "ENVVAR", HelpText = "Name of the environment variable", Default = "PATH")]
    public string Key { get; set; }

    [Option('t', "target", MetaValue = "TARGET", HelpText = "", Default = EnvironmentVariableTarget.User)]
    public EnvironmentVariableTarget Target { get; set; }

    // This is for named pipe
    [Option('p', "pipe", Default = "", Hidden = true)]
    public string Pipe { get; set; }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Key: {Key}");
      sb.AppendLine($"Target: {Target}");
      sb.AppendLine($"Pipe: {Pipe}");
      return sb.ToString();
    }

    [Usage(ApplicationAlias = "Pathed.exe")]
    public static IEnumerable<Example> Examples {
      get {
        yield return new Example("Show paths of 'PATH' in User", new ShowOptions { Key = "PATH" });
        yield return new Example("Show paths of 'MyPath' in Machine", new ShowOptions { Key = "MyPath", Target = EnvironmentVariableTarget.Machine });
      }
    }
  }

  [Verb("slim", HelpText = "Remove duplicated or non-exeistent variable from environment variable")]
  public class SlimOptions {
    [Option('e', "envvar", MetaValue = "ENVVAR", HelpText = "Name of the environment variable", Default = "PATH")]
    public string Key { get; set; }

    [Option('t', "target", MetaValue = "TARGET", Default = EnvironmentVariableTarget.User)]
    public EnvironmentVariableTarget Target { get; set; }

    // This is for named pipe
    [Option('p', "pipe", Default = "", Hidden = true)]
    public string Pipe { get; set; }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Key: {Key}");
      sb.AppendLine($"Target: {Target}");
      sb.AppendLine($"Pipe: {Pipe}");
      return sb.ToString();
    }

    [Usage(ApplicationAlias = "Pathed.exe")]
    public static IEnumerable<Example> Examples {
      get {
        yield return new Example("Remove duplicated or invalid entries of 'PATH' in User", new SlimOptions { Key = "PATH" });
        yield return new Example("Remove duplicated or invalid entries of 'MyPath' in Machine", new SlimOptions { Key = "MyPath", Target = EnvironmentVariableTarget.Machine });
      }
    }
  }

  [Verb("sort", HelpText = "Sort environment variable alphabetically")]
  public class SortOptions {
    [Option('e', "envvar", MetaValue = "ENVVAR", HelpText = "Name of the environment variable", Default = "PATH")]
    public string Key { get; set; }

    [Option('t', "target", MetaValue = "TARGET", Default = EnvironmentVariableTarget.User)]
    public EnvironmentVariableTarget Target { get; set; }

    // This is for named pipe
    [Option('p', "pipe", Default = "", Hidden = true)]
    public string Pipe { get; set; }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Key: {Key}");
      sb.AppendLine($"Target: {Target}");
      sb.AppendLine($"Pipe: {Pipe}");
      return sb.ToString();
    }

    [Usage(ApplicationAlias = "Pathed.exe")]
    public static IEnumerable<Example> Examples {
      get {
        yield return new Example("Sort entries of 'PATH' in User", new SortOptions { Key = "PATH" });
        yield return new Example("Sort entries of 'MyPath' in Machine", new SortOptions { Key = "MyPath", Target = EnvironmentVariableTarget.Machine });
      }
    }
  }
}
