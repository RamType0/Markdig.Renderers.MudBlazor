using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent;
using Markdig.Syntax.Inlines;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Markdig.Renderers.MudBlazor.Inlines;

public class LinkInlineRenderer : RazorComponentObjectRenderer<LinkInline>
{
    /// <summary>
    /// Gets or sets the literal string in property rel for links
    /// </summary>
    public string? Rel { get; set; }
    protected override void Write(RazorComponentRenderer renderer, LinkInline link)
    {
        var builder = renderer.Builder;
        builder.OpenRegion(0);
        {
            var url = link.GetDynamicUrl?.Invoke() ?? link.Url;
            if (link.IsImage)
            {
                builder.OpenRegion(0);
                {
                    builder.OpenComponent<MudImage>(0);
                    {
                        builder.AddComponentParameter(1, nameof(MudImage.Src), url);
                        builder.AddAttributesToMudComponent(2, link.TryGetAttributes());

                        builder.AddComponentParameter(3, nameof(MudImage.Alt), RazorComponent.Inlines.LinkInlineRenderer.GetAlt(link));
                        builder.AddComponentParameter(4, "title", link.Title);
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
                        builder.AddAttributesToMudComponent(2, link.TryGetAttributes());

                        builder.AddComponentParameter(3, "title", link.Title);
                        if (!string.IsNullOrWhiteSpace(Rel))
                        {
                            builder.AddComponentParameter(4, "rel", Rel);
                        }
                        builder.AddComponentParameter(5, nameof(MudLink.ChildContent), (RenderFragment)(builder =>
                        {
                            using (renderer.UseBuilder(builder))
                            {
                                renderer.WriteChildren(0, link);
                            }
                        }));
                    }
                    builder.CloseComponent();
                }
                builder.CloseRegion();
            }
        }
        builder.CloseRegion();
    }
}
