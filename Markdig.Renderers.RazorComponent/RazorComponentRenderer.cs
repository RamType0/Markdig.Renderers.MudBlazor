using Markdig.Renderers.RazorComponent.Inlines;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Microsoft.AspNetCore.Components.Rendering;
using System.Diagnostics;

namespace Markdig.Renderers.RazorComponent;
public class RazorComponentRenderer : RendererBase
{
    public RazorComponentRenderer(RenderTreeBuilder renderTreeBuilder)
    {
        Builder = renderTreeBuilder;

        // Default block renderers
        ObjectRenderers.Add(new CodeBlockRenderer());
        ObjectRenderers.Add(new ListRenderer());
        ObjectRenderers.Add(new HeadingRenderer());
        ObjectRenderers.Add(new HtmlBlockRenderer());
        ObjectRenderers.Add(new ParagraphRenderer());
        ObjectRenderers.Add(new QuoteBlockRenderer());
        ObjectRenderers.Add(new ThematicBreakRenderer());

        // Default inline renderers
        ObjectRenderers.Add(new AutolinkInlineRenderer());
        ObjectRenderers.Add(new CodeInlineRenderer());
        ObjectRenderers.Add(new DelimiterInlineRenderer());
        ObjectRenderers.Add(new EmphasisInlineRenderer());
        ObjectRenderers.Add(new LineBreakInlineRenderer());
        ObjectRenderers.Add(new HtmlInlineRenderer());
        ObjectRenderers.Add(new HtmlEntityInlineRenderer());
        ObjectRenderers.Add(new LinkInlineRenderer());
        ObjectRenderers.Add(new LiteralInlineRenderer());

        ObjectRenderers.Add(new TableRenderer());
        ObjectRenderers.Add(new MathInlineRenderer());
    }

    public RenderTreeBuilder Builder { get; set; }
    public override object Render(MarkdownObject markdownObject)
    {
        Write(markdownObject);
        return Builder;
    }
    [Obsolete($"Use overload with sequence instead", true)]
    public new void WriteChildren(ContainerInline containerInline) => throw new UnreachableException();
    [Obsolete($"Use overload with sequence instead", true)]
    public new void WriteChildren(ContainerBlock containerBlock) => throw new UnreachableException();
    public void WriteChildren(int sequence, ContainerInline containerInline)
    {
        Builder.OpenRegion(sequence);
        base.WriteChildren(containerInline);
        Builder.CloseRegion();
    }
    public void WriteChildren(int sequence, ContainerBlock containerBlock)
    {
        Builder.OpenRegion(sequence);
        base.WriteChildren(containerBlock);
        Builder.CloseRegion();
    }

    public void WriteLeafInline(int sequence, LeafBlock leafBlock)
    {
        Builder.OpenRegion(sequence);
        {
            Inline? inline = leafBlock.Inline;

            while (inline != null)
            {
                Write(inline);
                inline = inline.NextSibling;
            }

        }
        Builder.CloseRegion();
    }
}
