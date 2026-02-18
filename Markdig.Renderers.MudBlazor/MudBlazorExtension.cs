using Markdig.Renderers.MudBlazor.Inlines;
using Markdig.Renderers.RazorComponent;

namespace Markdig.Renderers.MudBlazor;
public class MudBlazorExtension : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline)
    {

    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
        if (renderer is not RazorComponentRenderer razorComponentRenderer)
        {
            return;
        }

        razorComponentRenderer.ObjectRenderers.TryRemove<RazorComponent.HeadingRenderer>();
        razorComponentRenderer.ObjectRenderers.AddIfNotAlready<HeadingRenderer>();

        razorComponentRenderer.ObjectRenderers.TryRemove<RazorComponent.ParagraphRenderer>();
        razorComponentRenderer.ObjectRenderers.AddIfNotAlready<ParagraphRenderer>();

        razorComponentRenderer.ObjectRenderers.TryRemove<RazorComponent.ThematicBreakRenderer>();
        razorComponentRenderer.ObjectRenderers.AddIfNotAlready<ThematicBreakRenderer>();

        razorComponentRenderer.ObjectRenderers.TryRemove<RazorComponent.Inlines.AutolinkInlineRenderer>();
        razorComponentRenderer.ObjectRenderers.AddIfNotAlready<AutolinkInlineRenderer>();

        razorComponentRenderer.ObjectRenderers.TryRemove<RazorComponent.Inlines.LinkInlineRenderer>();
        razorComponentRenderer.ObjectRenderers.AddIfNotAlready<LinkInlineRenderer>();

        razorComponentRenderer.ObjectRenderers.TryRemove<RazorComponent.AlertBlockRenderer>();
        razorComponentRenderer.ObjectRenderers.AddIfNotAlready<AlertBlockRenderer>();

        razorComponentRenderer.ObjectRenderers.TryRemove<RazorComponent.TableRenderer>();
        razorComponentRenderer.ObjectRenderers.AddIfNotAlready<TableRenderer>();
    }
}
