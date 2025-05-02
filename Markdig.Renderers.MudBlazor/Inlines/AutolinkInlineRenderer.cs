using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent;
using Markdig.Syntax.Inlines;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Markdig.Renderers.MudBlazor.Inlines;

public class AutolinkInlineRenderer : RazorComponentObjectRenderer<AutolinkInline>
{
    /// <summary>
    /// Gets or sets the literal string in property rel for links
    /// </summary>
    public string? Rel { get; set; }
    protected override void Write(RazorComponentRenderer renderer, AutolinkInline obj)
    {
        var builder = renderer.Builder;
        builder.OpenRegion(0);
        {
            var url = obj.Url;
            if (obj.IsEmail)
            {
                url = $"mailto:{url}";
            }
            builder.OpenComponent<MudLink>(0);
            {
                builder.AddComponentParameter(1, nameof(MudLink.Href), url);
                builder.AddAttributesToMudComponent(2, obj.TryGetAttributes());
                if (!obj.IsEmail && !string.IsNullOrWhiteSpace(Rel))
                {
                    builder.AddComponentParameter(3, "rel", Rel);
                }
                builder.AddComponentParameter(4, nameof(MudLink.ChildContent), (RenderFragment)(builder =>
                {
                    builder.AddContent(0, obj.Url);
                }));
            }
            builder.CloseComponent();
        }
        builder.CloseRegion();
    }
}