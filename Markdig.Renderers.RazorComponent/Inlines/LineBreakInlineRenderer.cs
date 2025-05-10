using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.RazorComponent.Inlines;

public class LineBreakInlineRenderer : RazorComponentObjectRenderer<LineBreakInline>
{
    /// <summary>
    /// Gets or sets a value indicating whether to render this softline break as a HTML hardline break tag (&lt;br /&gt;)
    /// </summary>
    public bool RenderAsHardlineBreak { get; set; }
    protected override void Write(RazorComponentRenderer renderer, LineBreakInline obj)
    {
        var builder = renderer.Builder;
        if (obj.IsHard || RenderAsHardlineBreak)
        {
            builder.OpenElement(0, "br");
            {

            }
            builder.CloseElement();
        }
    }
}
