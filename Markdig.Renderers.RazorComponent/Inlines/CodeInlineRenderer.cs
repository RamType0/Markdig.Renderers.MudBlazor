using Markdig.Renderers.Html;
using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.RazorComponent.Inlines;

public class CodeInlineRenderer : RazorComponentObjectRenderer<CodeInline>
{
    protected override void Write(RazorComponentRenderer renderer, CodeInline obj)
    {
        var builder = renderer.Builder;
        builder.OpenRegion(0);
        {
            builder.OpenElement(0, "code");
            {
                builder.AddAttributes(1, obj.TryGetAttributes());
                builder.AddContent(2, obj.Content);
            }
            builder.CloseElement();
        }
        builder.CloseRegion();
    }
}
