using Markdig.Helpers;
using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent.ColorCode;
using Markdig.Renderers.RazorComponent.Components;
using Markdig.Syntax;

namespace Markdig.Renderers.RazorComponent;

public class CodeBlockRenderer : RazorComponentObjectRenderer<CodeBlock>
{
    public OrderedList<ICodeBlockChildRenderer> ChildRenderers { get; } = [new ColorCodeCodeBlockRenderer()];
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
        builder.OpenRegion(0);
        {
            var sourceCode = RazorComponentRenderer.GetLeafRawLines(codeBlock);

            builder.OpenComponent<FallbackCodeBlock>(0);
            {
                builder.AddAttributes(1, codeBlock.TryGetAttributes());
                builder.AddComponentParameter(2, nameof(FallbackCodeBlock.SourceCode), sourceCode);
                builder.AddComponentParameter(5, nameof(FallbackCodeBlock.OutputAttributesOnPre), OutputAttributesOnPre);
            }
            builder.CloseComponent();
        }
        builder.CloseRegion();
    }
}
