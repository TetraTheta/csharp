#pragma warning disable CRR0047

using System.ComponentModel;
using AliasEnum;

namespace Option.Enums {
  public class OperationTypeConverter : AliasEnumTypeConverter<Operation>;

  [TypeConverter(typeof(OperationTypeConverter))]
  public enum Operation {

    [Alias("background", "b", "bg")]
    Background,

    [Alias("center", "c")]
    Center,

    [Alias("foreground0", "f0", "fg0", "foreground-0")]
    Foreground0,

    [Alias("foreground1", "f1", "fg1", "foreground-1")]
    Foreground1,

    [Alias("foreground2", "f2", "fg2", "foreground-2")]
    Foreground2,

    [Alias("foreground3", "f3", "fg3", "foreground-3")]
    Foreground3,

    [Alias("foreground4", "f4", "fg4", "foreground-4")]
    Foreground4,

    [Alias("full", "f")]
    Full
  }
}
