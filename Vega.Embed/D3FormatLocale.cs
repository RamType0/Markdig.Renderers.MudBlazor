using System;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Vega.Embed;

public record D3FormatLocale
{
    public static D3FormatLocale FromNumberFormatInfo(NumberFormatInfo numberFormatInfo)
    {
        return new D3FormatLocale
        {
            Decimal = numberFormatInfo.NumberDecimalSeparator,
            Thousands = numberFormatInfo.NumberGroupSeparator,
            Grouping = numberFormatInfo.NumberGroupSizes,
            Currency = numberFormatInfo.CurrencyPositivePattern switch
            {
                0 => [numberFormatInfo.CurrencySymbol, ""],
                1 => ["", numberFormatInfo.CurrencySymbol],
                2 => [$"{numberFormatInfo.CurrencySymbol} ", ""],
                3 => ["", $" {numberFormatInfo.CurrencySymbol}"],
                _ => throw new ArgumentException($"Invalid CurrencyPositivePattern: {numberFormatInfo.CurrencyPositivePattern}"),
            },
            Numerals = numberFormatInfo.NativeDigits,
            Percent = numberFormatInfo.PercentSymbol,
            Minus = numberFormatInfo.NegativeSign,
            NaN = numberFormatInfo.NaNSymbol,
        };
    }
    [JsonPropertyName("decimal")]
    public required string Decimal { get; init; }
    [JsonPropertyName("thousands")]
    public required string Thousands { get; init; }
    [JsonPropertyName("grouping")]
    public required int[] Grouping { get; init; }
    [JsonPropertyName("currency")]
    public required string[] Currency { get; init; }

    // https://github.com/d3/d3-format/blob/main/src/locale.js
    // They check whether the value is `undefined` or not,
    // `null` may lead to an error in the JS code.

    [JsonPropertyName("numerals")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Numerals { get; init; }
    [JsonPropertyName("percent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Percent { get; init; }
    [JsonPropertyName("minus")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Minus { get; init; }
    [JsonPropertyName("nan")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? NaN { get; init; }

}
