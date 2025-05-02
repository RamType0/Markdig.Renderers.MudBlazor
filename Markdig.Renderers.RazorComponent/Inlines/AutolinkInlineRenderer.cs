using Markdig.Renderers.Html;
using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.RazorComponent.Inlines;

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
            builder.OpenElement(0, "a");
            {
                builder.AddAttribute(1, "href", url);
                builder.AddAttributes(2, obj.TryGetAttributes());
                if (!obj.IsEmail && !string.IsNullOrWhiteSpace(Rel))
                {
                    builder.AddAttribute(3, "rel", Rel);
                }
                builder.AddContent(4, obj.Url);
            }
            builder.CloseElement();
        }
        builder.CloseRegion();
    }
}