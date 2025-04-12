using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdig.Renderers.RazorComponent;

public class ParagraphRenderer : RazorComponentObjectRenderer<ParagraphBlock>
{
    protected override void Write(RazorComponentRenderer renderer, ParagraphBlock obj)
    {
        var builder = renderer.Builder;
        var sequence = 0;
        builder.OpenRegion(sequence);
        {
            builder.OpenElement(0, "p");
            {
                builder.AddAttributes(1, obj.TryGetAttributes());

                renderer.WriteLeafInline(2, obj);
            }
            builder.CloseComponent();
        }
        builder.CloseRegion();
    }
}
