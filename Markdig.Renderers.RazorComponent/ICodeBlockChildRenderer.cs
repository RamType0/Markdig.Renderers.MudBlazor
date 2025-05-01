using Markdig.Syntax;

namespace Markdig.Renderers.RazorComponent;

public interface ICodeBlockChildRenderer
{
    public abstract bool TryWrite(RazorComponentRenderer renderer, CodeBlockRenderer codeBlockRenderer, CodeBlock codeBlock);
}
