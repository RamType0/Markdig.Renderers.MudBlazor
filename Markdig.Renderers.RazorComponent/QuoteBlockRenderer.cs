using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdig.Renderers.RazorComponent;

public class QuoteBlockRenderer : RazorComponentObjectRenderer<QuoteBlock>
{
    protected override void Write(RazorComponentRenderer renderer, QuoteBlock quoteBlock)
    {
        var builder = renderer.Builder;
        var sequence = 0;
        builder.OpenRegion(sequence);
        {
            builder.OpenElement(0, "blockquote");
            {
                builder.AddAttributes(1, quoteBlock.TryGetAttributes());
                renderer.WriteChildren(2, quoteBlock);
            }
            builder.CloseElement();
        }
        builder.CloseRegion();
    }
}