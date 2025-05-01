using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.RazorComponent.Inlines;

public class LineBreakInlineRenderer : RazorComponentObjectRenderer<LineBreakInline>
{
    protected override void Write(RazorComponentRenderer renderer, LineBreakInline obj)
    {
        var builder = renderer.Builder;
        builder.OpenElement(0, "br");
        {

        }
        builder.CloseElement();
    }
}
