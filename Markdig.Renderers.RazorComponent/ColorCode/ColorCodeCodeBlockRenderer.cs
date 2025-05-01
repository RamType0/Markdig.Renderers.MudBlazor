using ColorCode;
using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent.Components;
using Markdig.Syntax;

namespace Markdig.Renderers.RazorComponent.ColorCode;

public class ColorCodeCodeBlockRenderer : ICodeBlockChildRenderer
{
    public global::ColorCode.Styling.StyleDictionary? CodeStyle { get; set; }
    public bool TryWrite(RazorComponentRenderer renderer, CodeBlockRenderer codeBlockRenderer, CodeBlock codeBlock)
    {

        var languageId = (codeBlock as FencedCodeBlock)?.Info;
        var language = string.IsNullOrEmpty(languageId) ? null : Languages.FindById(languageId);

        if(language is null)
        {
            return false;
        }
        else
        {
            var builder = renderer.Builder;
            builder.OpenRegion(0);
            {
                var sourceCode = RazorComponentRenderer.GetLeafRawLines(codeBlock);

                builder.OpenComponent<ColorCodeBlock>(0);
                {
                    builder.AddAttributes(1, codeBlock.TryGetAttributes());
                    builder.AddComponentParameter(2, nameof(ColorCodeBlock.SourceCode), sourceCode);
                    builder.AddComponentParameter(3, nameof(ColorCodeBlock.Language), language);
                    builder.AddComponentParameter(4, nameof(ColorCodeBlock.CodeStyle), CodeStyle);
                    builder.AddComponentParameter(5, nameof(ColorCodeBlock.OutputAttributesOnPre), codeBlockRenderer.OutputAttributesOnPre);
                }
                builder.CloseComponent();
            }
            builder.CloseRegion();

            return true;
        }

            
    }
}
