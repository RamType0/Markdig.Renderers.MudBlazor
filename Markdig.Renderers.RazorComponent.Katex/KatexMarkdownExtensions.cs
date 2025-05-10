using Markdig.Renderers.RazorComponent.Katex;

namespace Markdig;

public static class KatexMarkdownExtensions
{
    public static MarkdownPipelineBuilder UseKatex(this MarkdownPipelineBuilder pipeline)
    {
        pipeline.Extensions.AddIfNotAlready<KatexExtension>();

        return pipeline;
    }
}