using Markdig.Syntax;
using System.Runtime.CompilerServices;

namespace Markdig.Renderers.RazorComponent;

public class HtmlBlockRenderer : RazorComponentObjectRenderer<HtmlBlock>
{
    protected override void Write(RazorComponentRenderer renderer, HtmlBlock htmlBlock)
    {
        var builder = renderer.Builder;
        var sequence = 0;
        var lineCount = htmlBlock.Lines.Lines?.Length ?? 0;
        DefaultInterpolatedStringHandler htmlBuilder = new(lineCount, lineCount);
        foreach (var line in htmlBlock.Lines.Lines ?? [])
        {
            htmlBuilder.AppendFormatted(line);
            htmlBuilder.AppendLiteral("\n");
        }
        builder.AddMarkupContent(sequence, htmlBuilder.ToStringAndClear());
    }
}
