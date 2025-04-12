using ColorCode;
using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent.Components;
using Markdig.Syntax;
using System.Runtime.CompilerServices;

namespace Markdig.Renderers.RazorComponent;

public class CodeBlockRenderer : RazorComponentObjectRenderer<CodeBlock>
{
    public bool OutputAttributesOnPre { get; set; }
    public ColorCode.Styling.StyleDictionary? CodeStyle { get; set; }
    protected override void Write(RazorComponentRenderer renderer, CodeBlock codeBlock)
    {
        var builder = renderer.Builder;
        var sequence = 0;
        builder.OpenRegion(sequence);
        {
            var sourceCode = GetSourceCode(codeBlock);
            var languageId = (codeBlock as FencedCodeBlock)?.Info;
            var language = string.IsNullOrEmpty(languageId) ? null : Languages.FindById(languageId);
            builder.OpenComponent<ColorCodeBlock>(0);
            {
                builder.AddAttributes(1, codeBlock.TryGetAttributes());
                builder.AddAttribute(2, nameof(ColorCodeBlock.SourceCode), sourceCode);
                builder.AddAttribute(3, nameof(ColorCodeBlock.Language), language);
                builder.AddAttribute(4, nameof(ColorCodeBlock.CodeStyle), CodeStyle);
                builder.AddAttribute(5, nameof(ColorCodeBlock.OutputAttributesOnPre), OutputAttributesOnPre);
            }
            builder.CloseComponent();
        }
        builder.CloseRegion();
    }
    static string GetSourceCode(CodeBlock codeBlock)
    {
        var lines = codeBlock.Lines.Lines;
        var lineCount = lines?.Length ?? 0;
        DefaultInterpolatedStringHandler sourceCodeBuilder = new(lineCount, lineCount);
        foreach (var line in lines ?? [])
        {
            sourceCodeBuilder.AppendFormatted(line);
            sourceCodeBuilder.AppendLiteral("\n");
        }
        return sourceCodeBuilder.ToStringAndClear();
    }
}
