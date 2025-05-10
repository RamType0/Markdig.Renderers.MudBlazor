using Markdig.Renderers.RazorComponent.Vega.Embed;

namespace Markdig;

public static class VegaEmbedMarkdownExtensions
{
    public static MarkdownPipelineBuilder UseVegaEmbed(this MarkdownPipelineBuilder pipeline)
    {
        pipeline.Extensions.AddIfNotAlready<VegaEmbedExtension>();

        return pipeline;
    }
}