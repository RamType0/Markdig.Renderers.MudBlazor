using Markdig.Renderers.RazorComponent.Kroki;

namespace Markdig;

public static class KrokiMarkdownExtensions
{
    public static MarkdownPipelineBuilder UseKroki(this MarkdownPipelineBuilder pipeline)
    {
        pipeline.Extensions.AddIfNotAlready<KrokiExtension>();

        return pipeline;
    }
}