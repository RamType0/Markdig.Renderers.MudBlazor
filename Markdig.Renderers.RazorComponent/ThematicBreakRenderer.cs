using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdig.Renderers.RazorComponent;

public class ThematicBreakRenderer : RazorComponentObjectRenderer<ThematicBreakBlock>
{
    protected override void Write(RazorComponentRenderer renderer, ThematicBreakBlock obj)
    {
        var builder = renderer.Builder;
        var sequence = 0;
        builder.OpenRegion(sequence);
        {
            builder.OpenElement(0, "hr");
            {
                builder.AddAttributes(1, obj.TryGetAttributes());
            }
            builder.CloseElement();
        }
        builder.CloseRegion();

    }
}
