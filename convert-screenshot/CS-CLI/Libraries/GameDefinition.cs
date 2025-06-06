using System.Collections.Generic;
using common;

namespace CS_CLI.Libraries;
public class GameDefinition {
  public interface IGD {
    Dictionary<Operation, (CropPosition, string, uint)> DefOp { get; }
    Dictionary<string, string> DefUID { get; }
  }

  public class WuWa : IGD {
    public Dictionary<Operation, (CropPosition, string, uint)> DefOp { get; }
    public Dictionary<string, string> DefUID { get; }

    public WuWa() {
      DefOp = new() {
        { Operation.Background,  (CropPosition.Bottom, "Crop_Background_Height",  360) },
        { Operation.Center,      (CropPosition.Center, "Crop_Center_Height",      220) },
        { Operation.Foreground0, (CropPosition.Bottom, "Crop_Foreground0_Height", 310) },
        { Operation.Foreground1, (CropPosition.Bottom, "Crop_Foreground1_Height", 420) },
        { Operation.Foreground2, (CropPosition.Bottom, "Crop_Foreground2_Height", 505) },
        { Operation.Foreground3, (CropPosition.Bottom, "Crop_Foreground3_Height", 580) },
        { Operation.Foreground4, (CropPosition.Bottom, "Crop_Foreground4_Height", 655) },
        { Operation.Full,        (CropPosition.Full,   string.Empty,              0)   }
      };
      DefUID = new() {
        { "UID_Area", "144,22" },
        { "UID_Position", "1744,1059" }
      };
    }
  }
}
