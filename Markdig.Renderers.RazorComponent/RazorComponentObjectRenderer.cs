using Markdig.Syntax;

namespace Markdig.Renderers.RazorComponent;

public abstract class RazorComponentObjectRenderer<TObject> : MarkdownObjectRenderer<RazorComponentRenderer, TObject> where TObject : MarkdownObject;