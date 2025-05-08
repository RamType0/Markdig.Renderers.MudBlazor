using Markdig.Renderers.Html;
using Markdig.Syntax;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Markdig.Renderers.RazorComponent.Components;
public class ParagraphView : ComponentBase
{
    [CascadingParameter]
    RazorComponentRenderer Renderer { get; set; } = null!;
    [CascadingParameter(Name = RazorComponentRendererCascadingParameters.ImplicitParagraph)]
    bool ImplicitParagraph { get; set; }
    [Parameter, EditorRequired]
    public required ParagraphBlock Paragraph { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        using (Renderer.UseBuilder(builder))
        {
            if (ImplicitParagraph)
            {
                Renderer.WriteLeafInline(0, Paragraph);
            }
            else
            {
                builder.OpenElement(1, "p");
                {
                    builder.AddAttributes(2, Paragraph.TryGetAttributes());

                    Renderer.WriteLeafInline(3, Paragraph);
                }
                builder.CloseElement();
            }
        }
    }
}
