using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vega.Embed;

namespace Markdig.Renderers.RazorComponent.Vega;
internal class VegaCodeBlockRenderer : ICodeBlockChildRenderer
{
    public bool TryWrite(RazorComponentRenderer renderer, CodeBlockRenderer codeBlockRenderer, CodeBlock codeBlock)
    {
        var languageId = (codeBlock as FencedCodeBlock)?.Info;
        if (languageId is "vega" or "vegalite")
        {
            var builder = renderer.Builder;
            var sequence = 0;
            builder.OpenRegion(sequence);
            {
                builder.OpenComponent<VegaEmbedView>(0);
                {
                    builder.AddAttribute(1, nameof(VegaEmbedView.VegaSpec), RazorComponentRenderer.GetLeafRawLines(codeBlock));
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
