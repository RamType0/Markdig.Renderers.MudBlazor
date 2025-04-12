using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.RazorComponent.Inlines;

public class HtmlEntityInlineRenderer : RazorComponentObjectRenderer<HtmlEntityInline>
{
    protected override void Write(RazorComponentRenderer renderer, HtmlEntityInline obj)
    {
        var builder = renderer.Builder;
        var sequence = 0;
        builder.AddContent(sequence, obj.Transcoded);
    }
}
