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

    TaskCompletionSource firstRenderTcs = new();

    SemaphoreSlim semaphore = new(1);
    protected override void OnInitialized()
    {
        if (!RendererInfo.IsInteractive)
        {
            throw new InvalidOperationException($"{nameof(InteractiveKatexView)} must be used in interactive platform.");
        }
        moduleTask = new(() => JsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/RamType0.Markdig.Renderers.RazorComponent.Katex/lib/katex.js").AsTask());
    }

    protected override async Task OnParametersSetAsync()
    {
        await firstRenderTcs.Task;
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
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            firstRenderTcs.SetResult();
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
