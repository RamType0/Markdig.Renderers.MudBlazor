using Markdig.Renderers.Html;
using Markdig.Syntax.Inlines;
using System.Runtime.CompilerServices;

namespace Markdig.Renderers.RazorComponent.Inlines;

public class LinkInlineRenderer : RazorComponentObjectRenderer<LinkInline>
{

    /// <summary>
    /// Gets or sets the literal string in property rel for links
    /// </summary>
    public string? Rel { get; set; }
    protected override void Write(RazorComponentRenderer renderer, LinkInline link)
    {
        var builder = renderer.Builder;
        var sequence = 0;
        builder.OpenRegion(sequence);
        {
            var url = link.GetDynamicUrl?.Invoke() ?? link.Url;
            if (link.IsImage)
            {
                builder.OpenRegion(0);
                {
                    builder.OpenElement(0, "img");
                    {
                        builder.AddAttribute(1, "src", url);
                        builder.AddAttributes(2, link.TryGetAttributes());
                        if (GetAlt(link) is { } alt)
                        {
                            builder.AddAttribute(3, "alt", alt);
                        }
                        builder.AddAttribute(4, "title", link.Title);
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
                        builder.AddAttributes(2, link.TryGetAttributes());
                        builder.AddAttribute(3, "title", link.Title);

                        if (!string.IsNullOrWhiteSpace(Rel))
                        {
                            builder.AddAttribute(4, "rel", Rel);
                        }
                        renderer.WriteChildren(5, link);
                    }
                    builder.CloseElement();
                }
                builder.CloseRegion();
            }
        }
        builder.CloseRegion();
    }

    public static string? GetAlt(LinkInline link)
    {
        var hasAlt = false;
        DefaultInterpolatedStringHandler altBuilder = new(0, 0);
        foreach (var child in link)
        {
            if (child is LiteralInline literal)
            {
                hasAlt = true;
                altBuilder.AppendFormatted(literal.Content);
            }
        }
        var alt = altBuilder.ToStringAndClear();
        return hasAlt ? alt : null;
    }
}
