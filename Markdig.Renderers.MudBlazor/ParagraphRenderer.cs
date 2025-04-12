using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent;
using Markdig.Syntax;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Markdig.Renderers.MudBlazor;

public class ParagraphRenderer : RazorComponentObjectRenderer<ParagraphBlock>
{
    protected override void Write(RazorComponentRenderer renderer, ParagraphBlock obj)
    {
        var builder = renderer.Builder;
        var sequence = 0;
        builder.OpenRegion(sequence);
        {
            builder.OpenComponent<MudText>(0);
            {
                builder.AddAttributesToMudComponent(1, obj.TryGetAttributes());
                builder.AddAttribute(2, nameof(MudText.Typo), Typo.body1);
                builder.AddAttribute(3, nameof(MudText.ChildContent), (RenderFragment)(builder =>
                {
                    var originalBuilder = renderer.Builder;
                    {
                        renderer.Builder = builder;
                        renderer.WriteLeafInline(0, obj);
                    }
                    renderer.Builder = originalBuilder;
                }));
            }
            builder.CloseComponent();
        }
        builder.CloseRegion();
    }
}
