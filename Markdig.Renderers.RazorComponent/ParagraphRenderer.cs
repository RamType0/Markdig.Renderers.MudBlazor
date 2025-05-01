using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent.Components;
using Markdig.Syntax;

namespace Markdig.Renderers.RazorComponent;

public class ParagraphRenderer : RazorComponentObjectRenderer<ParagraphBlock>
{
    protected override void Write(RazorComponentRenderer renderer, ParagraphBlock obj)
    {
        var builder = renderer.Builder;
        builder.OpenRegion(0);
        {
            builder.OpenComponent<ParagraphView>(0);
            {
                builder.AddComponentParameter(1, nameof(ParagraphView.Paragraph), obj);
            }
            builder.CloseComponent();
        }
        builder.CloseRegion();
    }
}
