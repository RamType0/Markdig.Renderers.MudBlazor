using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vega.Embed;

#if NET9_0_OR_GREATER
[JsonConverter(typeof(JsonStringEnumConverter))]
#else
[JsonConverter(typeof(VegaEmbedModeJsonConverter))]
#endif
public enum VegaEmbedMode
{
#if NET9_0_OR_GREATER
    [JsonStringEnumMemberName("vega")]
#endif
    Vega,
#if NET9_0_OR_GREATER
    [JsonStringEnumMemberName("vega-lite")]
#endif
    VegaLite,
}


#if !NET9_0_OR_GREATER
internal sealed class VegaEmbedModeJsonConverter : JsonStringEnumConverter<VegaEmbedMode>
{
    public VegaEmbedModeJsonConverter() : base(new VegaEmbedModeJsonNamingPolicy())
    {
    }
}

internal sealed class VegaEmbedModeJsonNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name) => name switch
    {
        nameof(VegaEmbedMode.Vega) => "vega",
        nameof(VegaEmbedMode.VegaLite) => "vega-lite",
        _ => name,
    };
}
#endif

