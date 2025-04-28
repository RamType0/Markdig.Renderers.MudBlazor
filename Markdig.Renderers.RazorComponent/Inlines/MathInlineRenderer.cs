using Markdig.Extensions.Mathematics;
using Markdig.Renderers.RazorComponent.Components;
using Markdig.Renderers.RazorComponent.Katex;

namespace Markdig.Renderers.RazorComponent.Inlines;

public class MathInlineRenderer : RazorComponentObjectRenderer<MathInline>
{
    public static KatexOptions DefaultKatexOptions { get; } = new()
    {
        ThrowOnError = false,
    };
    public KatexOptions KatexOptions { get; set; } = DefaultKatexOptions;
    protected override void Write(RazorComponentRenderer renderer, MathInline obj)
    {
        var builder = renderer.Builder;
        var sequence = 0;

        builder.OpenRegion(sequence);
        {
            builder.OpenComponent<KatexView>(0);
            {
                builder.AddAttribute(1, nameof(KatexView.TexExpression), obj.Content.ToString());
                builder.AddAttribute(2, nameof(KatexView.Options), KatexOptions);
            }
            builder.CloseComponent();
        }
        builder.CloseRegion();
    }
}
