using Markdig.Extensions.Mathematics;
using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent.Components;

namespace Markdig.Renderers.RazorComponent;

public class MathBlockRenderer : RazorComponentObjectRenderer<MathBlock>
{
    public static KatexOptions DefaultKatexOptions { get; } = new()
    {
        DisplayMode = true,
        ThrowOnError = false,
    };
    public KatexOptions KatexOptions { get; } = DefaultKatexOptions;
    protected override void Write(RazorComponentRenderer renderer, MathBlock obj)
    {
        var builder = renderer.Builder;
        var sequence = 0;

        builder.OpenRegion(sequence);
        {
            builder.OpenElement(0, "div");
            {
                builder.AddAttributes(1, obj.TryGetAttributes());
                builder.OpenComponent<KatexView>(2);
                {
                    builder.AddAttribute(3, nameof(KatexView.TexExpression), RazorComponentRenderer.GetLeafRawLines(obj));
                    builder.AddAttribute(4, nameof(KatexView.Options), KatexOptions);
                }
                builder.CloseComponent();
            }
            builder.CloseElement();
            
        }
        builder.CloseRegion();
    }
}
