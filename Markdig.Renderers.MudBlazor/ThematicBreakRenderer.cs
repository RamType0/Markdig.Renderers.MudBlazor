using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent;
using Markdig.Syntax;
using MudBlazor;

namespace Markdig.Renderers.MudBlazor;

public class ThematicBreakRenderer : RazorComponentObjectRenderer<ThematicBreakBlock>
{
    protected override void Write(RazorComponentRenderer renderer, ThematicBreakBlock obj)
    {
        var builder = renderer.Builder;
        var sequence = 0;
        builder.OpenRegion(sequence);
        {
            builder.OpenComponent<MudDivider>(0);
            {
                builder.AddAttributes(1, obj.TryGetAttributes());
            }
            builder.CloseComponent();
        }
        builder.CloseRegion();
    }
}
