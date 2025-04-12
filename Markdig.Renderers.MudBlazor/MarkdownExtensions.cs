namespace Markdig.Renderers.MudBlazor;

public static class MarkdownExtensions
{
    public static MarkdownPipelineBuilder UseMudBlazor(this MarkdownPipelineBuilder pipeline)
    {
        pipeline.Extensions.AddIfNotAlready<MudBlazorExtension>();
        return pipeline;
    }
}