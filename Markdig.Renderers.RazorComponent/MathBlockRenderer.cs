using Markdig.Extensions.Mathematics;
using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent.Components;
using Markdig.Renderers.RazorComponent.Katex;

namespace Markdig.Renderers.RazorComponent;

public class MathBlockRenderer : RazorComponentObjectRenderer<MathBlock>
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
