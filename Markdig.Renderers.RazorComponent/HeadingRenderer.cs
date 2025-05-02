using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdig.Renderers.RazorComponent;

public class HeadingRenderer : RazorComponentObjectRenderer<HeadingBlock>
{
    protected override void Write(RazorComponentRenderer renderer, HeadingBlock obj)
    {
        var builder = renderer.Builder;
        builder.OpenRegion(0);
        {
            builder.OpenElement(0, GetHeadingTag(obj));
            {
                builder.AddAttributes(1, obj.TryGetAttributes());

                renderer.WriteLeafInline(2, obj);
            }
            builder.CloseElement();
        }
        builder.CloseRegion();
    }
    public Func<HeadingBlock, string> GetHeadingTag { get; set; } = GetHeadingDefaultTag;
    public static string GetHeadingDefaultTag(HeadingBlock headingBlock) => headingBlock.Level switch
    {
        1 => "h1",
        2 => "h2",
        3 => "h3",
        4 => "h4",
        5 => "h5",
        6 => "h6",
        _ => $"h{headingBlock.Level}",
    };
}