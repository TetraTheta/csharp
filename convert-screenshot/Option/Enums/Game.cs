#pragma warning disable CRR0047,CRRSP06

using System.ComponentModel;
using AliasEnum;

namespace Option.Enums {
  public class GameTypeConverter : AliasEnumTypeConverter<Game>;

  [TypeConverter(typeof(GameTypeConverter))]
  public enum Game {

    [Alias("tower-of-fantasy", "tf", "tof")]
    TOF,

    [Alias("wuthering-waves", "wuwa", "ww")]
    WW
  }
}
