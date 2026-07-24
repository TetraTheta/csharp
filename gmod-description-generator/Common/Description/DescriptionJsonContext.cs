using System.Text.Json.Serialization;

namespace GModDescriptionGenerator.Common.Description;

[JsonSourceGenerationOptions(
  WriteIndented = true,
  IndentCharacter = ' ',
  IndentSize = 2
)]
[JsonSerializable(typeof(DescriptionSettings))]
internal partial class DescriptionJsonContext : JsonSerializerContext {}
