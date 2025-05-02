using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace Vega.Embed;

internal class Result : IAsyncDisposable
{
    bool disposed = false;
    internal Result(IJSObjectReference jsResult)
    {
        JsResult = jsResult;
    }

    private IJSObjectReference JsResult { get; }

    public async ValueTask DisposeAsync()
    {
        if (!disposed)
        {
            disposed = true;
            await JsResult.InvokeVoidAsync("finalize");
        }
        await JsResult.DisposeAsync();
    }
}

public record EmbedOptions
{
    [JsonPropertyName("mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VegaEmbedMode? Mode { get; init; }
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Width { get; init; }
    [JsonPropertyName("height")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Height { get; init; }

    [JsonPropertyName("formatLocale")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public D3FormatLocale? FormatLocale { get; init; }

    [JsonPropertyName("timeFormatLocale")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public D3TimeFormatLocale? TimeFormatLocale { get; init; }

}
