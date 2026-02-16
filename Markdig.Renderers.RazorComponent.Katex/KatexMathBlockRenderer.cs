using Katex;
using Katex.Components;
using Markdig.Extensions.Mathematics;
using Markdig.Renderers.Html;

namespace Markdig.Renderers.RazorComponent.Katex;

public class KatexMathBlockRenderer : RazorComponentObjectRenderer<MathBlock>
{
    public static KatexOptions DefaultKatexOptions { get; } = new()
    {
        DisplayMode = true,
        ThrowOnError = false,
    };
    public KatexOptions KatexOptions { get; set; } = DefaultKatexOptions;
    protected override void Write(RazorComponentRenderer renderer, MathBlock obj)
    {
        var builder = renderer.Builder;

        builder.OpenRegion(0);
        {
            builder.OpenElement(0, "div");
            {
                builder.AddAttributes(1, obj.TryGetAttributes());
                builder.OpenComponent<KatexView>(2);
                {
                    builder.AddComponentParameter(3, nameof(KatexView.TexExpression), RazorComponentRenderer.GetLeafRawLines(obj));
                    builder.AddComponentParameter(4, nameof(KatexView.Options), KatexOptions);
                }
                builder.CloseComponent();
            }
            builder.CloseElement();

        }
        builder.CloseRegion();
    }
}
