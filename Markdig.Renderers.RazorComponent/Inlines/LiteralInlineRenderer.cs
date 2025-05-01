using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.RazorComponent.Inlines;

public class LiteralInlineRenderer : RazorComponentObjectRenderer<LiteralInline>
{
    protected override void Write(RazorComponentRenderer renderer, LiteralInline obj)
    {
        var builder = renderer.Builder;
        builder.AddContent(0, obj.Content.ToString());
    }
}
