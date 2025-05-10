using Bunit;
using Markdig.Renderers.RazorComponent.Inlines;

namespace Markdig.Renderers.RazorComponent.Tests.Inlines;

public class EmphasisInlineRendererTests : RazorComponentRendererTestContext
{
    [Theory]
    [InlineData("*Emphasis*", "em")]
    [InlineData("**Emphasis**", "strong")]
    public void EmphasisInlineRendersCorrectly(string markdown, string tag)
    {
        var document = Markdown.Parse(markdown);
        var rendered = Render(document.ToRenderFragment());
        rendered.MarkupMatches($"""
            <p>
                <{tag}>Emphasis</{tag}>
            </p>
            """);
    }
    [Theory]
    [InlineData("*Emphasis*", "em1")]
    [InlineData("**Emphasis**", "em2")]
    public void EmphasisInlineWithOverridedTagRendersCorrectly(string markdown, string tag)
    {
        var document = Markdown.Parse(markdown);

        var pipeline = new MarkdownPipelineBuilder().UseSetup((pipeline, renderer) =>
        {
            var headingRenderer = renderer.ObjectRenderers.FindExact<EmphasisInlineRenderer>() ?? throw new InvalidOperationException($"Could not find {nameof(EmphasisInlineRenderer)}.");
            headingRenderer.GetTag = emphasis => $"em{emphasis.DelimiterCount}";
        }).Build();

        var rendered = Render(document.ToRenderFragment(pipeline));

        rendered.MarkupMatches($"""
            <p>
                <{tag}>Emphasis</{tag}>
            </p>
            """);
    }
}
