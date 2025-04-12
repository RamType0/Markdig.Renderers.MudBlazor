using Markdig.Extensions.Mathematics;
using Markdig.Renderers.RazorComponent.Components;

namespace Markdig.Renderers.RazorComponent.Inlines;

public class MathInlineRenderer : RazorComponentObjectRenderer<MathInline>
{
    protected override void Write(RazorComponentRenderer renderer, MathInline obj)
    {
        var builder = renderer.Builder;
        var sequence = 0;

        builder.OpenRegion(sequence);
        {
            builder.OpenComponent<KatexMathInline>(0);
            {
                builder.AddAttribute(1, nameof(KatexMathInline.MathInline), obj);
            }
            builder.CloseComponent();
        }
        builder.CloseRegion();
    }
}
