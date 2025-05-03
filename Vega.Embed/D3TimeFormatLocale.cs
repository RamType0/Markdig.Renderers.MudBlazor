using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;

namespace Vega.Embed;

public record D3TimeFormatLocale
{
    public static D3TimeFormatLocale FromDateTimeFormatInfo(DateTimeFormatInfo dateTimeFormatInfo)
    {
        return new D3TimeFormatLocale
        {
            DateTime = D3DateTimeFormatConverter.ToD3Format(dateTimeFormatInfo.FullDateTimePattern),
            Date = D3DateTimeFormatConverter.ToD3Format(dateTimeFormatInfo.ShortDatePattern),
            Time = D3DateTimeFormatConverter.ToD3Format(dateTimeFormatInfo.LongTimePattern),
            Periods = [dateTimeFormatInfo.AMDesignator, dateTimeFormatInfo.PMDesignator],
            Days = dateTimeFormatInfo.DayNames,
            ShortDays = dateTimeFormatInfo.AbbreviatedDayNames,
            Months = dateTimeFormatInfo.MonthNames,
            ShortMonths = dateTimeFormatInfo.AbbreviatedMonthNames,
        };
    }
    

    [JsonPropertyName("dateTime")]
    public required string DateTime { get; init; }
    [JsonPropertyName("date")]
    public required string Date { get; init; }
    [JsonPropertyName("time")]
    public required string Time { get; init; }
    [JsonPropertyName("periods")]
    public required string[] Periods { get; init; }
    [JsonPropertyName("days")]
    public required string[] Days { get; init; }
    [JsonPropertyName("shortDays")]
    public required string[] ShortDays { get; init; }
    [JsonPropertyName("months")]
    public required string[] Months { get; init; }
    [JsonPropertyName("shortMonths")]
    public required string[] ShortMonths { get; init; }
}