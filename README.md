[![Version](https://img.shields.io/nuget/v/RamType0.Markdig.Renderers.MudBlazor?logo=nuget&style=flat-square)](https://www.nuget.org/packages/RamType0.Markdig.Renderers.MudBlazor/)
[![Nuget downloads](https://img.shields.io/nuget/dt/RamType0.Markdig.Renderers.MudBlazor?label=nuget%20downloads&logo=nuget&style=flat-square)](https://www.nuget.org/packages/RamType0.Markdig.Renderers.MudBlazor/)  
# Markdown renderers for Blazor and [MudBlazor](https://github.com/MudBlazor/MudBlazor)

## Getting started

### [KaTeX](https://github.com/KaTeX/KaTeX) integration
If you want to render `MathInline`, you need to do following steps.


#### [Install KaTeX](https://katex.org/docs/browser) in your app

Add this to `<head>` element. 
```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/katex@0.16.22/dist/katex.min.css" integrity="sha384-5TcZemv2l/9On385z///+d7MSYlvIEw9FuZTIdZ14vJLqWphw7e7ZPuOiCHJcFCP" crossorigin="anonymous">
```

### Add `KatexExtension` to `MarkdownPipelineBuilder`
```C#
using Markdig.Renderers.RazorComponent.Katex;

pipelineBuilder.UseKatex();
```

### [Vega-Embed](https://github.com/vega/vega-embed) integration

If you want to render Vega and Vega-Lite charts, you need to do following steps.


#### Add `VegaEmbedExtension` to `MarkdownPipelineBuilder`
```C#
using Markdig.Renderers.RazorComponent.Vega.Embed;

pipelineBuilder.UseVegaEmbed();
```

### [Kroki](https://github.com/yuzutech/kroki) integration
If you want to render diagrams supported by Kroki, you need to do following steps. 

#### Add `KrokiHttpRequestFactory` to services

```C#

builder.Services.AddKrokiHttpRequestFactory();

```

#### Add `KrokiExtension` to `MarkdownPipelineBuilder`

```C#

using Markdig.Renderers.RazorComponent.Kroki;

pipelineBuilder.UseKroki();

```

### MudBlazor

This library is using [CSS isolation](https://learn.microsoft.com/aspnet/core/blazor/components/css-isolation).

So don't forget to [bundle it](https://learn.microsoft.com/aspnet/core/blazor/components/css-isolation#css-isolation-bundling).

## How to use

### MudBlazor

Use `MudMarkdown` component.

```razor
@using Markdig.Renderers.MudBlazor.Components

<MudMarkdown Value="$ e^{i\theta} = \cos\theta + i\sin\theta $" />
```

### Common Blazor

Use `MarkdownDocument.ToRenderFragment` extension method.


```razor

@using Markdig
@using Markdig.Renderers.RazorComponent
@using Microsoft.AspNetCore.Components.Rendering

@GetMarkdownContent()

@code{

    string markdownText = @"
# Heading 1
## Heading 2
### Heading 3
#### Heading 4
##### Heading 5
###### Heading 6

Paragraph

*Emphasis*
**Emphasis**

> BlockQuote

This is a $math block$

 a     | b
-------|-------
 0     | 1
 2     | 3

";
    RenderFragment GetMarkdownContent()
    {
        var pipelineBuilder = new MarkdownPipelineBuilder().UseAdvancedExtensions();
        var pipeline = pipelineBuilder.Build();
        var markdownDocument = Markdown.Parse(markdownText, pipeline);
        return markdownDocument.ToRenderFragment(pipeline);
    }
}

```

## Customization

Just write extension for `RazorComponentRenderer`.

Actually, MudBlazor integration is implemented in this way.
```C#
public class MudBlazorExtension : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline)
    {

    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
        if (renderer is not RazorComponentRenderer razorComponentRenderer)
        {
            return;
        }

        razorComponentRenderer.ObjectRenderers.TryRemove<RazorComponent.HeadingRenderer>();
        razorComponentRenderer.ObjectRenderers.AddIfNotAlready<HeadingRenderer>();

        razorComponentRenderer.ObjectRenderers.TryRemove<RazorComponent.ParagraphRenderer>();
        razorComponentRenderer.ObjectRenderers.AddIfNotAlready<ParagraphRenderer>();

        razorComponentRenderer.ObjectRenderers.TryRemove<RazorComponent.ThematicBreakRenderer>();
        razorComponentRenderer.ObjectRenderers.AddIfNotAlready<ThematicBreakRenderer>();

        razorComponentRenderer.ObjectRenderers.TryRemove<RazorComponent.Inlines.AutolinkInlineRenderer>();
        razorComponentRenderer.ObjectRenderers.AddIfNotAlready<AutolinkInlineRenderer>();

        razorComponentRenderer.ObjectRenderers.TryRemove<RazorComponent.Inlines.LinkInlineRenderer>();
        razorComponentRenderer.ObjectRenderers.AddIfNotAlready<LinkInlineRenderer>();

        razorComponentRenderer.ObjectRenderers.TryRemove<RazorComponent.TableRenderer>();
        razorComponentRenderer.ObjectRenderers.AddIfNotAlready<TableRenderer>();
    }
}
```

## Robust rendering

There is already existing well-known Markdown integration for MudBlazor, [MudBlazor.Markdown](https://github.com/MyNihongo/MudBlazor.Markdown).

### `MathInline`

[MudBlazor.Markdown has wont-fix issue around rendering `MathInline`](https://github.com/MyNihongo/MudBlazor.Markdown/issues/291).

Unlike MudBlazor.Markdown, this library is using KaTeX instead of MathJax and never depends on direct DOM manipulation.

### `CodeBlock`

[MudBlazor.Markdown is using highlight.js to manipulate DOM directly](https://github.com/MyNihongo/MudBlazor.Markdown/blob/e9727f76245973915664c6dd75686d2e7358925d/src/MudBlazor.Markdown/Components/MudCodeHighlight.razor.cs#L132).

Unlike MudBlazor.Markdown, this library is using ColorCode instead of highlight.js and never depends on direct DOM manipulation.

### Mermaid diagrams

mermaid.js is very difficult to integrate to Blazor because it does terrible DOM manipulation.

This library is using [Kroki](https://github.com/yuzutech/kroki), and does not depend on mermaid.js directly.

## Compatible for LLM apps

Use `CascadingCodeBlockOptions.OpenCodeBlockContent` to override code block content for Vega-Embed, Kroki open code blocks.

This would be helpful for LLM apps. Open code blocks indicates that the code is imcomplete and rendering them resulted in error.

```razor
<CascadingCodeBlockOptions>
    <OpenCodeBlockContent>
        <MudProgressCircular Indeterminate />
    </OpenCodeBlockContent>
    <ChildContent>
        <MudMarkdown Value="@markdownText" Pipeline="@GetPipeline()" />
    </ChildContent>
</CascadingCodeBlockOptions>
```
