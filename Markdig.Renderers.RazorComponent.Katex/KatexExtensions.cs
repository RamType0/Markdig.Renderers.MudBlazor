namespace Markdig.Renderers.RazorComponent.Katex;

public static class KatexExtensions
{
    public static MarkdownPipelineBuilder UseKatex(this MarkdownPipelineBuilder pipeline)
    {
        pipeline.Extensions.AddIfNotAlready<KatexExtension>();

        return pipeline;
    }
}