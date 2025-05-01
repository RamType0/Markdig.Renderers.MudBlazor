using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.RazorComponent.Inlines;

public class DelimiterInlineRenderer : RazorComponentObjectRenderer<DelimiterInline>
{
    protected override void Write(RazorComponentRenderer renderer, DelimiterInline obj)
    {
        var builder = renderer.Builder;
        builder.OpenRegion(0);
        {
            builder.AddContent(0, obj.ToLiteral());
            renderer.WriteChildren(1, obj);
        }
        builder.CloseRegion();
    }
}