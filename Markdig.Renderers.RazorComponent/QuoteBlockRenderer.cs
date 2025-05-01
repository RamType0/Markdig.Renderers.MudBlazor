using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdig.Renderers.RazorComponent;

public class QuoteBlockRenderer : RazorComponentObjectRenderer<QuoteBlock>
{
    protected override void Write(RazorComponentRenderer renderer, QuoteBlock quoteBlock)
    {
        var builder = renderer.Builder;
        builder.OpenRegion(0);
        {
            builder.OpenElement(0, "blockquote");
            {
                builder.AddAttributes(1, quoteBlock.TryGetAttributes());

                builder.AddImplicitParagraph(2, true, false, builder =>
                {
                    using (renderer.UseBuilder(builder))
                    {
                        renderer.WriteChildren(0, quoteBlock);
                    }
                });
            }
            builder.CloseElement();
        }
        builder.CloseRegion();
    }
}