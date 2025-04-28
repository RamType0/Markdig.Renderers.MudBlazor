using System.Text.Json.Serialization;

namespace Kroki;
public class KrokiRequest
{
    public required string DiagramSource { get; set; }
    public required string DiagramType { get; set; }
    public required FileFormat OutputFormat { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyDictionary<string, string>? DiagramOptions { get; set; }
}
