using Markdig.Syntax;
using System.Runtime.CompilerServices;

namespace Markdig.Renderers.RazorComponent;

public class HtmlBlockRenderer : RazorComponentObjectRenderer<HtmlBlock>
{
    protected override void Write(RazorComponentRenderer renderer, HtmlBlock htmlBlock)
    {
        var builder = renderer.Builder;
        var sequence = 0;

        builder.AddMarkupContent(sequence, RazorComponentRenderer.GetLeafRawLines(htmlBlock));
    }
}
