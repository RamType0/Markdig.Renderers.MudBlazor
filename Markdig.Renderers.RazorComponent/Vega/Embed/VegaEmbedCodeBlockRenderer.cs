using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent.Components;
using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vega.Embed;

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
