using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Vega;
public record Spec
{
    [JsonPropertyName("$schema")]
    public Uri? Schema { get; init; }
    [JsonPropertyName("description")]
    public string? Description { get; init; }
    [JsonPropertyName("background")]
    public IBackground? Background { get; init; }

    [JsonPropertyName("width")]
    public IWidth? Width { get; init; }
    [JsonPropertyName("height")]
    public IHeight? Height { get; init; }
    [JsonPropertyName("padding")]
    public IPadding? Padding { get; init; }
    [JsonPropertyName("usermeta")]
    public JsonObject? UserMeta { get; init; }
}
