namespace Markdig.Renderers.RazorComponent.Kroki;

public class KrokiExtension : IMarkdownExtension
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
        codeBlockRenderer.UseKroki = true;
    }
}
