using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Markdig.Renderers.RazorComponent.Katex.Components;
partial class InteractiveKatexView : IAsyncDisposable
{
    [Inject]
    IJSRuntime JsRuntime { get; set; } = null!;
    [Parameter, EditorRequired]
    public required string TexExpression { get; set; }
    [Parameter]
    public KatexOptions? Options { get; set; }
    MarkupString? renderedExpression;

    Lazy<Task<IJSObjectReference>> moduleTask = null!;

    SemaphoreSlim semaphore = new(1);
    protected override void OnInitialized()
    {
        moduleTask = new(() => JsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/RamType0.Markdig.Renderers.RazorComponent.Katex/lib/katex.js").AsTask());
    }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await semaphore.WaitAsync();
        try
        {
            renderedExpression = null;
            var module = await moduleTask.Value;
            var html = await module.InvokeAsync<string>("katex.renderToString", TexExpression, Options);
            renderedExpression = new(html);
        }
        finally
        {
            semaphore.Release();
        }
        
    } 

    public async ValueTask DisposeAsync()
    {
        await semaphore.WaitAsync();
        try
        {
            if (moduleTask.IsValueCreated)
            {
                try
                {
                    var module = await moduleTask.Value;
                    await module.DisposeAsync();
                }
                catch (JSDisconnectedException)
                {

                }

            }
        }
        finally
        {
            semaphore.Release();
            semaphore.Dispose();
        }
    }
}
