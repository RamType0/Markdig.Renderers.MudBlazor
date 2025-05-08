using Markdig.Renderers.MudBlazor.Components;
using Markdig.Renderers.RazorComponent;
using Markdig.Syntax;

namespace Markdig.Renderers.MudBlazor;

public class ParagraphRenderer : RazorComponentObjectRenderer<ParagraphBlock>
{
    protected override void Write(RazorComponentRenderer renderer, ParagraphBlock obj)
    {
        var builder = renderer.Builder;
        builder.OpenRegion(0);
        {
            builder.OpenComponent<MudParagraphView>(0);
            {
                builder.AddComponentParameter(1, nameof(MudParagraphView.Paragraph), obj);
            }
            builder.CloseComponent();
        }
        builder.CloseRegion();
    }
}
