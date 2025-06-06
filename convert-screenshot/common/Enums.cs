using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace common;

public enum CropPosition {
  Bottom,
  Center,
  Full
}

[TypeConverter(typeof(GameConverter))]
public enum Game {
  None,
  WuWa
}

[TypeConverter(typeof(OperationConverter))]
public enum Operation {
  All,
  Background,
  Center,
  CreateDirectory,
  Foreground0,
  Foreground1,
  Foreground2,
  Foreground3,
  Foreground4,
  Full
}

// NOTE: CommandLineParser does not accept TypeConverter. These code are useless.
public class GameConverter : EnumConverter {
  private static readonly List<string> GameCandidate = ["wuwa", "ww"];
  private static readonly List<string> NoneCandidate = ["nil", "none", "null", "undefined"];

  public GameConverter() : base(typeof(Game)) { }

  public override object ConvertFrom(ITypeDescriptorContext? ctx, CultureInfo? ci, object value) {
    Console.WriteLine("gameconverter started");
    if (value is string s) {
      string l = s.Trim().ToLowerInvariant();
      if (GameCandidate.Contains(l)) return Game.WuWa;
      if (NoneCandidate.Contains(l)) return Game.None;
      // fallback
      if (Enum.TryParse<Game>(s, out Game parsed)) return parsed;
      Console.WriteLine("gameconverter failed 1");
      throw new ArgumentException($"Cannot convert '{s}' to {nameof(Game)}");
    }
    Console.WriteLine("gameconverter failed 2");
    return base.ConvertFrom(ctx, ci, value);
  }

  public Game 

  public Game ConvertString(string game) {
    return (Game) ConvertFrom(null, null, game);
  }
}

public class OperationConverter : EnumConverter {
  private static readonly List<string> AllCandidate = ["all", "a"];
  private static readonly List<string> BackgroundCandidate = ["background", "b", "bg"];
  private static readonly List<string> CenterCandidate = ["center", "c"];
  private static readonly List<string> CreateDirectoryCandidate = ["createdirectory", "cd"];
  private static readonly List<string> Foreground0Candidate = ["foreground0", "f0", "fg0"];
  private static readonly List<string> Foreground1Candidate = ["foreground1", "f1", "fg1"];
  private static readonly List<string> Foreground2Candidate = ["foreground2", "f2", "fg2"];
  private static readonly List<string> Foreground3Candidate = ["foreground3", "f3", "fg3"];
  private static readonly List<string> Foreground4Candidate = ["foreground4", "f4", "fg4"];
  private static readonly List<string> FullCandidate = ["full", "f"];

  public OperationConverter() : base(typeof(Operation)) { }

  public override object ConvertFrom(ITypeDescriptorContext? ctx, CultureInfo? ci, object value) {
    if (value is string s) {
      Console.WriteLine("operationconverter started");
      string l = s.Trim().ToLowerInvariant();
      if (AllCandidate.Contains(l)) return Operation.All;
      if (BackgroundCandidate.Contains(l)) return Operation.Background;
      if (CenterCandidate.Contains(l)) return Operation.Center;
      if (CreateDirectoryCandidate.Contains(l)) return Operation.CreateDirectory;
      if (Foreground0Candidate.Contains(l)) return Operation.Foreground0;
      if (Foreground1Candidate.Contains(l)) return Operation.Foreground1;
      if (Foreground2Candidate.Contains(l)) return Operation.Foreground2;
      if (Foreground3Candidate.Contains(l)) return Operation.Foreground3;
      if (Foreground4Candidate.Contains(l)) return Operation.Foreground4;
      if (FullCandidate.Contains(l)) return Operation.Full;
      // fallback
      if (Enum.TryParse<Operation>(s, out Operation parsed)) return parsed;
      Console.WriteLine("operationconverter failed 1");
      throw new ArgumentException($"Cannot convert '{s}' to {nameof(Operation)}");
    }
    Console.WriteLine("operationconverter failed 2");
    return base.ConvertFrom(ctx, ci, value);
  }

  public Operation ConvertString(string operation) {
    return (Operation)ConvertFrom(null, null, operation);
  }
}
