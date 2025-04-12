using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.RazorComponent.Inlines;

public class HtmlInlineRenderer : RazorComponentObjectRenderer<HtmlInline>
{
    protected override void Write(RazorComponentRenderer renderer, HtmlInline obj)
    {
        var builder = renderer.Builder;
        var sequence = 0;
        builder.AddMarkupContent(sequence, obj.Tag);
    }
}
