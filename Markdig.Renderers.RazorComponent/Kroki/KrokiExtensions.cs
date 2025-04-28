namespace Markdig.Renderers.RazorComponent.Kroki;

public static class KrokiExtensions
{
    public static MarkdownPipelineBuilder UseKroki(this MarkdownPipelineBuilder pipeline)
    {
        pipeline.Extensions.AddIfNotAlready<KrokiExtension>();

        return pipeline;
    }
}