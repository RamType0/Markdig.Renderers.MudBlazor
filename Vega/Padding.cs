using System.Text.Json.Serialization;

namespace Vega;

public record Padding : IPadding
{
    [JsonPropertyName("top")]
    public double? Top { get; init; }
    [JsonPropertyName("bottom")]
    public double? Bottom { get; init; }
    [JsonPropertyName("left")]
    public double? Left {  get; init; }
    [JsonPropertyName("right")]
    public double? Right {  get; init; }

}