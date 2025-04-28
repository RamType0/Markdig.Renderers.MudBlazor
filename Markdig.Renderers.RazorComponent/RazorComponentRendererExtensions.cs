using Markdig.Syntax;
using Microsoft.AspNetCore.Components;

namespace Markdig.Renderers.RazorComponent;

public static class RazorComponentRendererExtensions
{
    public static RenderFragment ToRenderFragment(this MarkdownDocument document, MarkdownPipeline? pipeline = null)
    {
        return builder =>
        {
            var renderer = new RazorComponentRenderer(builder);

            pipeline?.Setup(renderer);
            renderer.Render(document);
        };
    }
}
