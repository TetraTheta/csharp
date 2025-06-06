using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using CommandLine;

namespace common;

public class Options : ICloneable {
  [Option("crop-height", HelpText = "Crop Height.")]
  public uint? CropHeight { get; set; } // no default value

  [Option("crop-pos", HelpText = "Crop Position.\nChoices: full, center, bottom")]
  public CropPosition? CropPos { get; set; } // no default value

  [Option('g', "game", HelpText = "Game which the image file(s) are taken from.")]
  [TypeConverter(typeof(GameConverter))] // NOTE: CommandLineParser does not accept TypeConverter. This code is useless.
  public Game? Game { get; set; } = common.Game.None;

  [Option('o', "operation", Default = common.Operation.Full, HelpText = "Operation to perform to the image(s).\nChoices: full, background, center, foreground0, foreground1, foreground2, foreground3, foreground4.")]
  [TypeConverter(typeof(OperationConverter))] // NOTE: CommandLineParser does not accept TypeConverter. This code is useless.
  public Operation Operation { get; set; } = common.Operation.Full;

  [Value(0, MetaName = "target", Required = false, HelpText = "Target directory. If omitted, current working directory will be used.")]
  public string Target { get; set; } = Directory.GetCurrentDirectory();

  [Option("uid-area", Separator = ',', HelpText = "Dimension of UID area, represented as 'width,height'.")]
  public IEnumerable<uint>? UidArea { get; set; } // no default value

  [Option("uid-pos", Separator = ',', HelpText = "Top-left coordinate of UID area, represented as 'x,y'.")]
  public IEnumerable<uint>? UidPos { get; set; } // no default value

  [Option("width-from", HelpText = "Width of source image(s).")]

  public uint? WidthFrom { get; set; } // no default value

  [Option("width-to", HelpText = "Width of converted image(s).")]
  public uint? WidthTo { get; set; } // no default value

  public object Clone() {
    return new Options {
      CropHeight = this.CropHeight,
      CropPos = this.CropPos,
      Game = this.Game,
      Operation = this.Operation,
      Target = this.Target,
      UidArea = this.UidArea,
      UidPos = this.UidPos,
      WidthFrom = this.WidthFrom,
      WidthTo = this.WidthTo,
    };
  }

  public JobData ToJobData() {
    return new JobData {
      CropHeight = this.CropHeight,
      CropPos = this.CropPos ?? throw new InvalidDataException(nameof(CropPos)),
      JobName = this.Operation.ToString(),
      Target = this.Target ?? throw new InvalidDataException(nameof(Target)),
      UidArea = this.UidArea,
      UidPos = this.UidPos,
      WidthFrom = this.WidthFrom,
      WidthTo = this.WidthTo
    };
  }

#if DEBUG
  public new string ToString() {
    StringBuilder sb = new();
    sb.AppendLine($"CropHeight = {CropHeight}");
    sb.AppendLine($"CropPos = {CropPos}");
    sb.AppendLine($"Game = {Game}");
    sb.AppendLine($"Operation = {Operation}");
    sb.AppendLine($"Target = {Target}");
    sb.AppendLine($"UidArea = [{(UidArea == null ? "err0r" : string.Join(", ", UidArea))}]");
    sb.AppendLine($"UidPos = [{(UidPos == null ? "err0r" : string.Join(", ", UidPos))}]");
    sb.AppendLine($"WidthFrom = {WidthFrom}");
    sb.Append($"WidthTo = {WidthTo}");
    return sb.ToString();
  }
#endif

  public bool Validate(out IEnumerable<string> errors) {
    List<string> errs = [];
    
    // validate 'target'
    if (string.IsNullOrEmpty(Target)) Target = Directory.GetCurrentDirectory();
    if (!Directory.Exists(Target)) errs.Add($"Target '{Target}' is not a valid directory.");
    // subdirectory search will happen later :(

    // validate 'game'
    if (Game == common.Game.None && Operation != Operation.Full) errs.Add($"The '--game' option must be set when setting '--operation' to value other than 'full'.");

    errors = errs;
    return errs.Count == 0;
  }
}
