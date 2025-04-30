using ColorCode;
using Kroki;
using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent.Components;
using Markdig.Syntax;
using System.Runtime.CompilerServices;

namespace Markdig.Renderers.RazorComponent;

public class CodeBlockRenderer : RazorComponentObjectRenderer<CodeBlock>
{
    public bool UseKroki { get; set; }
    public bool OutputAttributesOnPre { get; set; }
    public ColorCode.Styling.StyleDictionary? CodeStyle { get; set; }
    protected override void Write(RazorComponentRenderer renderer, CodeBlock codeBlock)
    {
        var builder = renderer.Builder;
        var sequence = 0;
        builder.OpenRegion(sequence);
        {
            var sourceCode = RazorComponentRenderer.GetLeafRawLines(codeBlock);
            var languageId = (codeBlock as FencedCodeBlock)?.Info;
            if(UseKroki && languageId is not null && DiagramTypes.All.Contains(languageId))
            {
                builder.OpenRegion(0);
                {
                    builder.OpenComponent<KrokiView>(0);
                    {
                        builder.AddAttribute(1, nameof(KrokiView.DiagramType), languageId);
                        builder.AddAttribute(2, nameof(KrokiView.DiagramSource), sourceCode);
                    }
                    builder.CloseComponent();
                }
                builder.CloseRegion();
            }
            else
            {
                builder.OpenRegion(1);
                {
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
            
        }
        builder.CloseRegion();
    }
}
