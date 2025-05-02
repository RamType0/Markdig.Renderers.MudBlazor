using Markdig.Helpers;
using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent.ColorCode;
using Markdig.Renderers.RazorComponent.Components;
using Markdig.Renderers.RazorComponent.Vega;
using Markdig.Syntax;

namespace Markdig.Renderers.RazorComponent;

public class CodeBlockRenderer : RazorComponentObjectRenderer<CodeBlock>
{
    public OrderedList<ICodeBlockChildRenderer> ChildRenderers { get; } = [new VegaCodeBlockRenderer(), new ColorCodeCodeBlockRenderer()];
    public bool OutputAttributesOnPre { get; set; }
    protected override void Write(RazorComponentRenderer renderer, CodeBlock codeBlock)
    {
        foreach (var childRenderer in ChildRenderers)
        {
            if (childRenderer.TryWrite(renderer, this, codeBlock))
            {
                return;
            }
        }

        var builder = renderer.Builder;
        var sequence = 0;
        builder.OpenRegion(sequence);
        {
            var sourceCode = RazorComponentRenderer.GetLeafRawLines(codeBlock);

            builder.OpenComponent<FallbackCodeBlock>(0);
            {
                builder.AddAttributes(1, codeBlock.TryGetAttributes());
                builder.AddAttribute(2, nameof(FallbackCodeBlock.SourceCode), sourceCode);
                builder.AddAttribute(5, nameof(FallbackCodeBlock.OutputAttributesOnPre), OutputAttributesOnPre);
            }
            builder.CloseComponent();
        }
        builder.CloseRegion();
    }
}
