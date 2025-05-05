using Markdig;
using Markdig.Renderers.MudBlazor;
using Markdig.Renderers.RazorComponent.Katex;
using Markdig.Renderers.RazorComponent.Kroki;
using Markdig.Renderers.RazorComponent.Vega.Embed;

namespace SampleApp.Web.Client.Pages;

partial class Home
{
    static MarkdownPipeline MarkdownPipeline { get; } = new MarkdownPipelineBuilder().UseAdvancedExtensions().UseMudBlazor().UseVegaEmbed().UseKatex().Build();
    static readonly string markdownText = """
### Markdig.Renderers.RazorComponent Sample App

**Welcome to sample app!**

You can experience 
  - Live Markdown Preview
  - Ollama chat with markdown formatted response

with this sample.

This page demonstrates SSR with this library.

SSR is also supported for

$KaTeX math inlines$.


""";
}
