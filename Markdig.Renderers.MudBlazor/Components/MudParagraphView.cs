using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent;
using Markdig.Syntax;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;

namespace Markdig.Renderers.MudBlazor.Components;
public class MudParagraphView : ComponentBase
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
                builder.OpenComponent<MudText>(1);
                {
                    builder.AddAttributesToMudComponent(1, Paragraph.TryGetAttributes());
                    builder.AddComponentParameter(2, nameof(MudText.Typo), Typo.body1);
                    builder.AddComponentParameter(3, nameof(MudText.ChildContent), (RenderFragment)(builder =>
                    {
                        using (Renderer.UseBuilder(builder))
                        {
                            Renderer.WriteLeafInline(0, Paragraph);
                        }
                    }));
                }
                builder.CloseComponent();
            }
        }
    }
}
