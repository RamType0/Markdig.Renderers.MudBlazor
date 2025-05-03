using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vega.Embed;

#if NET9_0_OR_GREATER
[JsonConverter(typeof(JsonStringEnumConverter))]
#else
using System.Text.Json.Serialization;
using System.Text.Json;

[JsonConverter(typeof(RendererJsonConverter))]
#endif
public enum Renderer
{
#if NET9_0_OR_GREATER
    [JsonStringEnumMemberName("canvas")]
#endif
    Canvas,
#if NET9_0_OR_GREATER
    [JsonStringEnumMemberName("svg")]
#endif
    Svg,
}


#if !NET9_0_OR_GREATER
internal sealed class RendererJsonConverter : JsonStringEnumConverter<Renderer>
{
    public RendererJsonConverter() : base(new RendererJsonNamingPolicy())
    {
    }
}

internal sealed class RendererJsonNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name) => name switch
    {
        nameof(Renderer.Canvas) => "canvas",
        nameof(Renderer.Svg) => "svg",
        _ => name,
    };
}
#endif