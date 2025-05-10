using Bunit;

namespace Markdig.Renderers.RazorComponent.Tests;

public class HeadingRendererTests : RazorComponentRendererTestContext
{
    [Theory]
    [InlineData("# Heading", "h1")]
    [InlineData("## Heading", "h2")]
    [InlineData("### Heading", "h3")]
    [InlineData("#### Heading", "h4")]
    [InlineData("##### Heading", "h5")]
    [InlineData("###### Heading", "h6")]
    public void HeadingRendersCorrectly(string markdown, string tag)
    {
        var document = Markdown.Parse(markdown);
        var rendered = Render(document.ToRenderFragment());

        rendered.MarkupMatches($"<{tag}>Heading</{tag}>");
    }
    [Theory]
    [InlineData("# Heading", "h6")]
    [InlineData("## Heading", "h5")]
    [InlineData("### Heading", "h4")]
    [InlineData("#### Heading", "h3")]
    [InlineData("##### Heading", "h2")]
    [InlineData("###### Heading", "h1")]
    public void HeadingWithOverridedTagRendersCorrectly(string markdown, string tag)
    {
        var document = Markdown.Parse(markdown);

        var pipeline = new MarkdownPipelineBuilder().UseSetup((pipeline, renderer) =>
        {
            var headingRenderer = renderer.ObjectRenderers.FindExact<HeadingRenderer>() ?? throw new InvalidOperationException($"Could not find {nameof(HeadingRenderer)}.");
            headingRenderer.GetHeadingTag = heading => $"h{7 - heading.Level}";
        }).Build();

        var rendered = Render(document.ToRenderFragment(pipeline));
        rendered.MarkupMatches($"<{tag}>Heading</{tag}>");
    }
}
