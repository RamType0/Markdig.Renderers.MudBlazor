using Markdig.Renderers.RazorComponent;
using Markdig.Renderers.RazorComponent.Tests.Helpers;

namespace Markdig;

public static class InlineRazorComponentRendererSetupExtensions
{
    public static MarkdownPipelineBuilder UseSetup(this MarkdownPipelineBuilder pipeline, Action<MarkdownPipeline, RazorComponentRenderer> setup)
    {
        pipeline.Extensions.Add(new InlineRazorComponentRendererSetupExtension(setup));
        return pipeline;
    }
}