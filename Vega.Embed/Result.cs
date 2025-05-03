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
