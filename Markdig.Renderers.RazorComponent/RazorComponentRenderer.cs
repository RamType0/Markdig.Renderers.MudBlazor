using Markdig.Renderers.RazorComponent.Inlines;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Diagnostics;
using System.Runtime.CompilerServices;

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
    }

    public RenderTreeBuilder Builder { get; set; }
    public override object Render(MarkdownObject markdownObject)
    {
        var builder = Builder;
        builder.OpenRegion(0);
        {
            builder.OpenComponent<CascadingValue<RazorComponentRenderer>>(0);
            {
                builder.AddComponentParameter(1, nameof(CascadingValue<RazorComponentRenderer>.IsFixed), BoxedBool.True);
                builder.AddComponentParameter(2, nameof(CascadingValue<RazorComponentRenderer>.Value), this);
                builder.AddComponentParameter(3, nameof(CascadingValue<RazorComponentRenderer>.ChildContent), (RenderFragment)(builder =>
                {
                    using (UseBuilder(builder))
                    {
                        Write(markdownObject);
                    }
                }));
            }
            builder.CloseComponent();
        }
        builder.CloseRegion();
        return builder;
    }
    [Obsolete($"Use overload with sequence instead", true)]
    public new void WriteChildren(ContainerInline containerInline) => throw new UnreachableException();
    [Obsolete($"Use overload with sequence instead", true)]
    public new void WriteChildren(ContainerBlock containerBlock) => throw new UnreachableException();
    public void WriteChildren(int sequence, ContainerInline containerInline)
    {
        var builder = Builder;
        builder.OpenRegion(sequence);
        base.WriteChildren(containerInline);
        builder.CloseRegion();
    }
    public void WriteChildren(int sequence, ContainerBlock containerBlock)
    {
        var builder = Builder;
        builder.OpenRegion(sequence);
        base.WriteChildren(containerBlock);
        builder.CloseRegion();
    }

    public void WriteLeafInline(int sequence, LeafBlock leafBlock)
    {
        var builder = Builder;
        builder.OpenRegion(sequence);
        {
            Inline? inline = leafBlock.Inline;

            while (inline != null)
            {
                Write(inline);
                inline = inline.NextSibling;
            }

        }
        builder.CloseRegion();
    }
    public static string GetLeafRawLines(LeafBlock leafBlock)
    {
        var lines = leafBlock.Lines.Lines;
        var lineCount = lines?.Length ?? 0;
        DefaultInterpolatedStringHandler sourceCodeBuilder = new(lineCount, lineCount);
        foreach (var line in lines ?? [])
        {
            sourceCodeBuilder.AppendFormatted(line);
            sourceCodeBuilder.AppendLiteral("\n");
        }
        return sourceCodeBuilder.ToStringAndClear();
    }
    public BuilderOverrideScope UseBuilder(RenderTreeBuilder overrideBuilder) => new(this, overrideBuilder);
    public readonly struct BuilderOverrideScope : IDisposable
    {
        readonly RazorComponentRenderer renderer;
        readonly RenderTreeBuilder originalBuilder;
        public BuilderOverrideScope(RazorComponentRenderer renderer, RenderTreeBuilder overrideBuilder)
        {
            this.renderer = renderer;
            originalBuilder = renderer.Builder;
            renderer.Builder = overrideBuilder;
        }
        public void Dispose()
        {
            renderer.Builder = originalBuilder;
        }
    }
}
