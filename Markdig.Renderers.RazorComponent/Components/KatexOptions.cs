﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Markdig.Renderers.RazorComponent.Components;
/// <summary>
/// The options for <c>katex.render</c> and <c>katex.renderToString</c>. 
/// </summary>
/// <remarks><see href="https://katex.org/docs/options.html">Document</see></remarks>
public record KatexOptions
{
    /// <summary>
    /// If <see langword="true"/> the math will be rendered in display mode. If <see langword="false"/> the math will be rendered in inline mode.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? DisplayMode { get; set; }
    /// <summary>
    /// Determines the markup language of the output.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Output { get; set; }
    /// <summary>
    /// If <see langword="true"/>, display math has <c>\tags</c> rendered on the left instead of the right, like <c>\usepackage[leqno]{amsmath}</c> in LaTeX.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Leqno { get; set; }
    /// <summary>
    /// If <see langword="true"/>, display math renders flush left with a <c>2em</c> left margin, like <c>\documentclass[fleqn]</c> in LaTeX with the <c>amsmath</c> package.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Fleqn { get; set; }
    /// <summary>
    /// If <see langword="true"/> (the default), KaTeX will throw a <c>ParseError</c> when it encounters an unsupported command or invalid LaTeX. If <see langword="false"/>, KaTeX will render unsupported commands as text, and render invalid LaTeX as its source code with hover text giving the error, in the color given by <see cref="ErrorColor"/>.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ThrowOnError { get; set; }
    /// <summary>
    /// A color string given in the format <c>"#XXX"</c> or <c>"#XXXXXX"</c>. This option determines the color that unsupported commands and invalid LaTeX are rendered in when <see cref="ThrowOnError"/> is set to <see langword="false"/>. (default: <c>#cc0000</c>)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ErrorColor { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyDictionary<string, string>? Macros { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? MinRuleThickness { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ColorIsTextColor { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? MaxSize { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? MaxExpand { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Strict { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Trust { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? GlobalGroup { get; set; }
}
