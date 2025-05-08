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

    protected override void OnInitialized()
    {
        moduleTask = new(() => JsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/RamType0.Markdig.Renderers.RazorComponent/lib/katex.js").AsTask());
    }
    protected override async Task OnParametersSetAsync()
    {
        renderedExpression = null;
        var module = await moduleTask.Value;
        var html = await module.InvokeAsync<string>("katex.renderToString", TexExpression, Options);
        renderedExpression = new(html);
    }

    public async ValueTask DisposeAsync()
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

                throw;
            }

        }
    }
}
