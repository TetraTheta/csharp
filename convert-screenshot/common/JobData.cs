using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace common;

public class JobData {
  public uint? CropHeight { get; set; } // no default value

  [Required]
  public CropPosition CropPos { get; set; } // no default value

  [Required]
  public string JobName { get; set; } = string.Empty; // invalid default value

  [Required]
  public string Target { get; set; } = string.Empty; // invalid default value

  public IEnumerable<uint>? UidArea { get; set; } // no default value

  public IEnumerable<uint>? UidPos { get; set; } // no default value


  public uint? WidthFrom { get; set; } // no default value

  public uint? WidthTo { get; set; } // no default value

#if DEBUG
  public new string ToString() {
    StringBuilder sb = new();
    sb.AppendLine($"CropHeight = {CropHeight}");
    sb.AppendLine($"CropPos = {CropPos}");
    sb.AppendLine($"JobName = {JobName}");
    sb.AppendLine($"Target = {Target}");
    sb.AppendLine($"UidArea = [{(UidArea == null ? "err0r" : string.Join(", ", UidArea))}]");
    sb.AppendLine($"UidPos = [{(UidPos == null ? "err0r" : string.Join(", ", UidPos))}]");
    sb.AppendLine($"WidthFrom = {WidthFrom}");
    sb.Append($"WidthTo = {WidthTo}");
    return sb.ToString();
  }
#endif
}
