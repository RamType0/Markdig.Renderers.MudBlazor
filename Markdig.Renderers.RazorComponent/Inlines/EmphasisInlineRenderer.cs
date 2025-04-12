using Markdig.Renderers.Html;
using Markdig.Syntax.Inlines;
using static Markdig.Renderers.Html.Inlines.EmphasisInlineRenderer;

namespace Markdig.Renderers.RazorComponent.Inlines;

public class EmphasisInlineRenderer : RazorComponentObjectRenderer<EmphasisInline>
{
    /// <summary>
    /// Gets or sets the GetTag delegate.
    /// </summary>
    public GetTagDelegate GetTag { get; set; } = GetDefaultTag;
    protected override void Write(RazorComponentRenderer renderer, EmphasisInline obj)
    {
        var builder = renderer.Builder;
        var sequence = 0;
        builder.OpenRegion(sequence);
        {
            var elementName = GetTag(obj);
            if (elementName is not null)
            {
                builder.OpenElement(0, elementName);
                {
                    builder.AddAttributes(1, obj.TryGetAttributes());
                    renderer.WriteChildren(2, obj);
                }
                builder.CloseElement();
            }
            else
            {
                renderer.WriteChildren(2, obj);
            }
        }
        builder.CloseRegion();
    }
}
