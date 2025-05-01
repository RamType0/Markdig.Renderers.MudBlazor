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
            var sequence = 0;
            builder.OpenRegion(sequence);
            {
                var sourceCode = RazorComponentRenderer.GetLeafRawLines(codeBlock);

                builder.OpenComponent<ColorCodeBlock>(0);
                {
                    builder.AddAttributes(1, codeBlock.TryGetAttributes());
                    builder.AddAttribute(2, nameof(ColorCodeBlock.SourceCode), sourceCode);
                    builder.AddAttribute(3, nameof(ColorCodeBlock.Language), language);
                    builder.AddAttribute(4, nameof(ColorCodeBlock.CodeStyle), CodeStyle);
                    builder.AddAttribute(5, nameof(ColorCodeBlock.OutputAttributesOnPre), codeBlockRenderer.OutputAttributesOnPre);
                }
                builder.CloseComponent();
            }
            builder.CloseRegion();

            return true;
        }

            
    }
}
