using Markdig.Syntax;

namespace Markdig.Renderers.RazorComponent;

public class HtmlBlockRenderer : RazorComponentObjectRenderer<HtmlBlock>
{
    protected override void Write(RazorComponentRenderer renderer, HtmlBlock htmlBlock)
    {
        var builder = renderer.Builder;
        builder.AddMarkupContent(0, RazorComponentRenderer.GetLeafRawLines(htmlBlock));
    }
}
