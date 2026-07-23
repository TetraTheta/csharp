using System.Text.Json.Serialization;

namespace GModDescriptionGenerator.Libraries;

[JsonSourceGenerationOptions(
  WriteIndented = true,
  IndentCharacter = ' ',
  IndentSize = 2
)]
[JsonSerializable(typeof(DescriptionSettings))]
internal partial class DescriptionJsonContext : JsonSerializerContext {}
