using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent;
using Markdig.Renderers.RazorComponent.Inlines;
using Markdig.Syntax.Inlines;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;

namespace Markdig.Renderers.MudBlazor.Components;
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
                    builder.OpenComponent<MudImage>(0);
                    {
                        builder.AddComponentParameter(1, nameof(MudImage.Src), url);
                        builder.AddAttributesToMudComponent(2, Link.TryGetAttributes());

                        builder.AddComponentParameter(3, nameof(MudImage.Alt), LinkInlineRenderer.GetAlt(Link));
                        builder.AddComponentParameter(4, "title", Link.Title);
                    }
                    builder.CloseComponent();
                }
                builder.CloseRegion();
            }
            else
            {
                builder.OpenRegion(1);
                {
                    builder.OpenComponent<MudLink>(0);
                    {
                        builder.AddComponentParameter(1, nameof(MudLink.Href), url);
                        builder.AddAttributesToMudComponent(2, Link.TryGetAttributes());

                        builder.AddComponentParameter(3, "title", Link.Title);
                        if (!string.IsNullOrWhiteSpace(Rel))
                        {
                            builder.AddComponentParameter(4, "rel", Rel);
                        }
                        builder.AddComponentParameter(5, nameof(MudLink.ChildContent), (RenderFragment)(builder =>
                        {
                            using (Renderer.UseBuilder(builder))
                            {
                                Renderer.WriteChildren(0, Link);
                            }
                        }));
                    }
                    builder.CloseComponent();
                }
                builder.CloseRegion();
            }
        }
        
    }
}
