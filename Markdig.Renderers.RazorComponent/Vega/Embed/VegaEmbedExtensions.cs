namespace Markdig.Renderers.RazorComponent.Vega.Embed;

public static class VegaEmbedExtensions
{
    public static MarkdownPipelineBuilder UseVegaEmbed(this MarkdownPipelineBuilder pipeline)
    {
        pipeline.Extensions.AddIfNotAlready<VegaEmbedExtension>();

        return pipeline;
    }
}