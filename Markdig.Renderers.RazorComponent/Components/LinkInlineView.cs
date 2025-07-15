using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent.Inlines;
using Markdig.Syntax.Inlines;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Markdig.Renderers.RazorComponent.Components;
public class LinkInlineView : ComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [CascadingParameter]
    RazorComponentRenderer Renderer { get; set; } = null!;
    [Parameter, EditorRequired]
    public required LinkInline Link { get; set; }
    [Parameter, EditorRequired]
    public string? Rel { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        using (Renderer.UseBuilder(builder))
        {
            var url = Link.GetDynamicUrl?.Invoke() ?? Link.Url;
            if (url is not null && url.StartsWith('#'))
            {
                url = NavigationManager.Uri + url;
            }
            if (Link.IsImage)
            {
                builder.OpenRegion(0);
                {
                    builder.OpenElement(0, "img");
                    {
                        builder.AddAttribute(1, "src", url);
                        builder.AddAttributes(2, Link.TryGetAttributes());
                        if (LinkInlineRenderer.GetAlt(Link) is { } alt)
                        {
                            builder.AddAttribute(3, "alt", alt);
                        }
                        builder.AddAttribute(4, "title", Link.Title);
                    }
                    builder.CloseElement();
                }
                builder.CloseRegion();
            }
            else
            {
                builder.OpenRegion(1);
                {
                    builder.OpenElement(0, "a");
                    {
                        builder.AddAttribute(1, "href", url);
                        builder.AddAttributes(2, Link.TryGetAttributes());
                        builder.AddAttribute(3, "title", Link.Title);

                        if (!string.IsNullOrWhiteSpace(Rel))
                        {
                            builder.AddAttribute(4, "rel", Rel);
                        }
                        Renderer.WriteChildren(5, Link);
                    }
                    builder.CloseElement();
                }
                builder.CloseRegion();
            }
        }
        
    }
}
