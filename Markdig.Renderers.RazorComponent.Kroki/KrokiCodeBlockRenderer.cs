using Kroki;
using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent.Kroki.Components;
using Markdig.Syntax;

namespace Markdig.Renderers.RazorComponent.Kroki;

public class KrokiCodeBlockRenderer : ICodeBlockChildRenderer
{
    public bool TryWrite(RazorComponentRenderer renderer, CodeBlockRenderer codeBlockRenderer, CodeBlock codeBlock)
    {
        var languageId = (codeBlock as FencedCodeBlock)?.Info;

        if (languageId is not null && DiagramTypes.All.Contains(languageId))
        {
            var builder = renderer.Builder;
            builder.OpenRegion(0);
            {
                builder.OpenComponent<KrokiCodeBlock>(0);
                {
                    builder.AddAttributes(1, codeBlock.TryGetAttributes());
                    builder.AddComponentParameter(2, nameof(KrokiCodeBlock.CodeBlock), codeBlock);
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