namespace Markdig.Renderers.RazorComponent.Katex;

public class KatexExtension : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
        
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
        if (renderer is not RazorComponentRenderer razorComponentRenderer)
        {
            return;
        }

        if (!razorComponentRenderer.ObjectRenderers.Contains<KatexMathInlineRenderer>())
        {
            razorComponentRenderer.ObjectRenderers.Insert(0, new KatexMathInlineRenderer());
        }
        if(!razorComponentRenderer.ObjectRenderers.Contains<KatexMathBlockRenderer>())
        {
            razorComponentRenderer.ObjectRenderers.Insert(0, new KatexMathBlockRenderer());
        }
    }
}
