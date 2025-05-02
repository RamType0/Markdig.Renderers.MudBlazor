using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdig.Renderers.RazorComponent;

public class ThematicBreakRenderer : RazorComponentObjectRenderer<ThematicBreakBlock>
{
    protected override void Write(RazorComponentRenderer renderer, ThematicBreakBlock obj)
    {
        var builder = renderer.Builder;
        builder.OpenRegion(0);
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
