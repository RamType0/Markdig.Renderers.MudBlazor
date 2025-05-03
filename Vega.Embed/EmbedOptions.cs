using System.Text.Json.Serialization;

namespace Vega.Embed;

public record EmbedOptions
{
    [JsonPropertyName("mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VegaEmbedMode? Mode { get; set; }

    [JsonPropertyName("theme")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Theme { get; set; }
    [JsonPropertyName("renderer")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Renderer? Renderer { get; set; }
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Width { get; set; }
    [JsonPropertyName("height")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Height { get; set; }

    [JsonPropertyName("editorUrl")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Uri? EditorUrl { get; set; }
    [JsonPropertyName("sourceHeader")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SourceHeader { get; set; }
    [JsonPropertyName("sourceFooter")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SourceFooter { get; set; }
    [JsonPropertyName("downloadFileName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DownloadFileName { get; set; }

    [JsonPropertyName("formatLocale")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public D3FormatLocale? FormatLocale { get; set; }

    [JsonPropertyName("timeFormatLocale")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public D3TimeFormatLocale? TimeFormatLocale { get; set; }

    [JsonPropertyName("ast")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Ast { get; set; }

}
