namespace Markdig.Renderers.RazorComponent.Tests.Helpers;

public sealed class InlineRazorComponentRendererSetupExtension(Action<MarkdownPipeline, RazorComponentRenderer> setup) : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
        
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
        if(renderer is RazorComponentRenderer razorComponentRenderer)
        {
            setup(pipeline, razorComponentRenderer);
        }
    }
}
