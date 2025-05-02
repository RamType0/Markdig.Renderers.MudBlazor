using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.RazorComponent.Inlines;

public class HtmlEntityInlineRenderer : RazorComponentObjectRenderer<HtmlEntityInline>
{
    protected override void Write(RazorComponentRenderer renderer, HtmlEntityInline obj)
    {
        var builder = renderer.Builder;
        builder.AddContent(0, obj.Transcoded);
    }
}
