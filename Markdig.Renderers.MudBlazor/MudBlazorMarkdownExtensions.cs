using Markdig.Renderers.MudBlazor;

namespace Markdig;

public static class MudBlazorMarkdownExtensions

{
    public static MarkdownPipelineBuilder UseMudBlazor(this MarkdownPipelineBuilder pipeline)
    {
        pipeline.Extensions.AddIfNotAlready<MudBlazorExtension>();
        return pipeline;
    }
}