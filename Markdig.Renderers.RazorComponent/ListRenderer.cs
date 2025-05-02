using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdig.Renderers.RazorComponent;

public class ListRenderer : RazorComponentObjectRenderer<ListBlock>
{
    protected override void Write(RazorComponentRenderer renderer, ListBlock listBlock)
    {
        var builder = renderer.Builder;
        builder.OpenRegion(0);
        {
            var listElementName = listBlock.IsOrdered ? "ol" : "ul";
            builder.OpenElement(0, listElementName);
            {
                if (listBlock.IsOrdered)
                {
                    if (listBlock.BulletType != '1')
                    {
                        builder.AddAttribute(1, "type", listBlock.BulletType.ToString());
                    }
                    if (listBlock.OrderedStart is not null && listBlock.OrderedStart != "1")
                    {
                        builder.AddAttribute(2, "start", listBlock.OrderedStart);
                    }
                }
                builder.AddAttributes(3, listBlock.TryGetAttributes());
                foreach (var item in listBlock)
                {
                    var listItem = (ListItemBlock)item;
                    builder.AddImplicitParagraph(4, false, !listBlock.IsLoose, builder =>
                    {
                        using (renderer.UseBuilder(builder))
                        {
                            builder.OpenElement(0, "li");
                            {
                                builder.AddAttributes(1, listItem.TryGetAttributes());
                                renderer.WriteChildren(2, listItem);
                            }
                            builder.CloseElement();
                        }
                    });
                }
            }
            builder.CloseElement();
        }
        builder.CloseRegion();
    }
}
