using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent.Components;
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
        builder.OpenRegion(0);
        {
            builder.OpenComponent<LinkInlineView>(0);
            {
                builder.AddAttribute(1, nameof(LinkInlineView.Link), link);
                builder.AddAttribute(2, nameof(LinkInlineView.Rel), Rel);
            }
            builder.CloseComponent();
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
