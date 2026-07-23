namespace GModDescriptionGenerator.Libraries;

public class DescriptionSettings {
  public string Title { get; set; } = string.Empty;
  public string Author { get; set; } = string.Empty;
  public int RDDate { get; set; } = 0;
  public int RDMonth { get; set; } = 0;
  public int RDYear { get; set; } = 0;
  public bool NoRD { get; set; } = false;
  public string Description { get; set; } = string.Empty;
  public string Synopsis { get; set; } = string.Empty;
  public bool HL2 { get; set; } = true;
  public bool CSS { get; set; } = false;
  public bool TF2 { get; set; } = false;
  public bool L4D2 { get; set; } = false;
  public string MapList { get; set; } = string.Empty;
  public bool Subtitle { get; set; } = false;
  public bool SCTools { get; set; } = false;
  public bool Recompiled { get; set; } = false;
  public string Warning { get; set; } = string.Empty;

  public void TrimAll() {
    Title = Title.Trim() ?? string.Empty;
    Author = Author.Trim() ?? string.Empty;
    Description = Description.Trim() ?? string.Empty;
    Synopsis = Synopsis.Trim() ?? string.Empty;
    MapList = MapList.Trim() ?? string.Empty;
    Warning = Warning.Trim() ?? string.Empty;
  }
}
