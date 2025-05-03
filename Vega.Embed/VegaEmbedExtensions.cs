using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace Vega.Embed;

public static class VegaEmbedExtensions
{
    public static async ValueTask<IAsyncDisposable> VegaEmbedAsync(this IJSRuntime jsRuntime, ElementReference el, JsonElement spec, EmbedOptions? opts = null)
    {
        object?[] args = opts is null ? [el, spec] : [el, spec, opts];
        var jsResult = await jsRuntime.InvokeAsync<IJSObjectReference>("vegaEmbed", args);
        return new Result(jsResult);
    }
    public static async ValueTask<IAsyncDisposable> VegaEmbedAsync(this IJSRuntime jsRuntime, ElementReference el, JsonObject spec, EmbedOptions? opts = null)
    {
        object?[] args = opts is null ? [el, spec] : [el, spec, opts];
        var jsResult = await jsRuntime.InvokeAsync<IJSObjectReference>("vegaEmbed", args);
        return new Result(jsResult);
    }
    public static async ValueTask<IAsyncDisposable> VegaEmbedAsync(this IJSRuntime jsRuntime, ElementReference el, Uri spec, EmbedOptions? opts = null)
    {
        object?[] args = opts is null ? [el, spec] : [el, spec, opts];
        var jsResult = await jsRuntime.InvokeAsync<IJSObjectReference>("vegaEmbed", args);
        return new Result(jsResult);
    }
    public static async ValueTask<IAsyncDisposable> VegaEmbedAsync(this IJSRuntime jsRuntime, string el, JsonElement spec, EmbedOptions? opts = null)
    {
        object?[] args = opts is null ? [el, spec] : [el, spec, opts];
        var jsResult = await jsRuntime.InvokeAsync<IJSObjectReference>("vegaEmbed", args);
        return new Result(jsResult);
    }
    public static async ValueTask<IAsyncDisposable> VegaEmbedAsync(this IJSRuntime jsRuntime, string el, JsonObject spec, EmbedOptions? opts = null)
    {
        object?[] args = opts is null ? [el, spec] : [el, spec, opts];
        var jsResult = await jsRuntime.InvokeAsync<IJSObjectReference>("vegaEmbed", args);
        return new Result(jsResult);
    }
    public static async ValueTask<IAsyncDisposable> VegaEmbedAsync(this IJSRuntime jsRuntime, string el, Uri spec, EmbedOptions? opts = null)
    {
        object?[] args = opts is null ? [el, spec] : [el, spec, opts];
        var jsResult = await jsRuntime.InvokeAsync<IJSObjectReference>("vegaEmbed", args);
        return new Result(jsResult);
    }
}
