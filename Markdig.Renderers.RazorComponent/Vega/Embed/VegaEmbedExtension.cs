namespace Markdig.Renderers.RazorComponent.Vega.Embed;

public class VegaEmbedExtension : IMarkdownExtension
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
        var codeBlockRenderer = renderer.ObjectRenderers.FindExact<CodeBlockRenderer>() ?? throw new InvalidOperationException($"Could not find {nameof(CodeBlockRenderer)}.");
        if(!codeBlockRenderer.ChildRenderers.Contains<VegaEmbedCodeBlockRenderer>())
        {
            codeBlockRenderer.ChildRenderers.Insert(0, new VegaEmbedCodeBlockRenderer());
        }
        
    }
}
