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
### Sample App

**Welcome to sample app!**

You can experience 
  - Live Markdown Preview
  - Ollama chat with markdown formatted response

with this sample.

This page demonstrates SSR with this library.

SSR is also supported for

$KaTeX math inlines$, and Vega, Vega-Lite charts.

```vegalite

{
  "$schema": "https://vega.github.io/schema/vega-lite/v6.json",
  "description": "A simple bar chart with embedded data.",
  "data": {
    "values": [
      {"a": "A", "b": 28}, {"a": "B", "b": 55}, {"a": "C", "b": 43},
      {"a": "D", "b": 91}, {"a": "E", "b": 81}, {"a": "F", "b": 53},
      {"a": "G", "b": 19}, {"a": "H", "b": 87}, {"a": "I", "b": 52}
    ]
  },
  "mark": "bar",
  "encoding": {
    "x": {"field": "a", "type": "nominal", "axis": {"labelAngle": 0}},
    "y": {"field": "b", "type": "quantitative"}
  }
}
```

But please care, we could **NOT** handle JS error on SSR!

""";
}
