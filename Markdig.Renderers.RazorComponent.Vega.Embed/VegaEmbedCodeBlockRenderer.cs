using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent.Vega.Embed.Components;
using Markdig.Syntax;

namespace Markdig.Renderers.RazorComponent.Vega.Embed;
internal class VegaEmbedCodeBlockRenderer : ICodeBlockChildRenderer
{
    public bool TryWrite(RazorComponentRenderer renderer, CodeBlockRenderer codeBlockRenderer, CodeBlock codeBlock)
    {
        var languageId = (codeBlock as FencedCodeBlock)?.Info;
        if (languageId is "vega" or "vegalite" or "vega-lite")
        {
            var builder = renderer.Builder;
            builder.OpenRegion(0);
            {
                builder.OpenComponent<VegaEmbedCodeBlock>(0);
                {
                    builder.AddAttributes(1, codeBlock.TryGetAttributes());
                    builder.AddComponentParameter(2, nameof(VegaEmbedCodeBlock.CodeBlock), codeBlock);
                }
                builder.CloseComponent();
            }
            builder.CloseRegion();
            return true;
        }
        else
        {
            return false;
        }
    }
}
