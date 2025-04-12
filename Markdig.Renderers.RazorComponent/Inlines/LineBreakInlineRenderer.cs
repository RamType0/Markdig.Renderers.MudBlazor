using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.RazorComponent.Inlines;

public class LineBreakInlineRenderer : RazorComponentObjectRenderer<LineBreakInline>
{
    protected override void Write(RazorComponentRenderer renderer, LineBreakInline obj)
    {
        var builder = renderer.Builder;
        var sequence = 0;
        builder.OpenElement(sequence, "br");
        {

        }
        builder.CloseElement();
    }
}
