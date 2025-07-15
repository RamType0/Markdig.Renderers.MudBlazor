using Markdig.Renderers.MudBlazor.Components;
using Markdig.Renderers.RazorComponent;
using Markdig.Syntax.Inlines;
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
            builder.OpenComponent<LinkInlineView>(0);
            {
                builder.AddAttribute(1, nameof(LinkInlineView.Link), link);
                builder.AddAttribute(2, nameof(LinkInlineView.Rel), Rel);
            }
            builder.CloseComponent();
        }
        builder.CloseRegion();
    }
}
