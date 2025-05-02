using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;

namespace Vega.Embed;

public record D3TimeFormatLocale
{
    public static D3TimeFormatLocale FromDateTimeFormatInfo(DateTimeFormatInfo dateTimeFormatInfo, string dateTime, string date, string time)
    {
        return new D3TimeFormatLocale
        {
            DateTime = dateTime,
            Date = date,
            Time = time,
            Periods = [dateTimeFormatInfo.AMDesignator, dateTimeFormatInfo.PMDesignator],
            Days = dateTimeFormatInfo.DayNames,
            ShortDays = dateTimeFormatInfo.AbbreviatedDayNames,
            Months = dateTimeFormatInfo.MonthNames,
            ShortMonths = dateTimeFormatInfo.AbbreviatedMonthNames,
        };
    }

    static string ToD3Time(string longTimePattern)
    {
        ReadOnlySpan<char> original = longTimePattern;
        StringBuilder d3Time = new();
        while (!original.IsEmpty)
        {
            if (original.StartsWith("d"))
            {
                if (original.StartsWith("dddd"))
                {
                    d3Time.Append("%A");
                    original = original[4..];
                    continue;
                }
                if (original.StartsWith("ddd"))
                {
                    d3Time.Append("%a");
                    original = original[3..];
                    continue;
                }
                if (original.StartsWith("dd"))
                {
                    d3Time.Append("%d");
                    original = original[2..];
                    continue;
                }
                {
                    d3Time.Append("%e");
                    original = original[1..];
                    continue;
                }
            }
            if (original.StartsWith("f"))
            {
                if(original.StartsWith("fffffff"))
                {
                    throw new FormatException("D3 does not support 100 nano-seconds");
                }
                if (original.StartsWith("ffffff"))
                {
                    d3Time.Append("%f");
                    original = original[6..];
                    continue;
                }
                if(original.StartsWith("fffff"))
                {
                    throw new FormatException("D3 does not support 10 micro-seconds");
                }
                if (original.StartsWith("ffff"))
                {
                    throw new FormatException("D3 does not support 100 micro-seconds");
                }
                if (original.StartsWith("fff"))
                {
                    d3Time.Append("%L");
                    original = original[3..];
                    continue;
                }
                if (original.StartsWith("ff"))
                {
                    throw new FormatException("D3 does not support centi-seconds");
                }
                throw new FormatException("D3 does not support deci-seconds");
            }

            if (original.StartsWith("F"))
            {
                if (original.StartsWith("FFFFFFF"))
                {
                    throw new FormatException("D3 does not support 100 nano-seconds");
                }
                if (original.StartsWith("FFFFFF"))
                {
                    d3Time.Append("%-F");
                    original = original[6..];
                    continue;
                }
                if (original.StartsWith("FFFFF"))
                {
                    throw new FormatException("D3 does not support 10 micro-seconds");
                }
                if (original.StartsWith("FFFF"))
                {
                    throw new FormatException("D3 does not support 100 micro-seconds");
                }
                if (original.StartsWith("FFF"))
                {
                    d3Time.Append("%-L");
                    original = original[3..];
                    continue;
                }
                if (original.StartsWith("FF"))
                {
                    throw new FormatException("D3 does not support centi-seconds");
                }
                throw new FormatException("D3 does not support deci-seconds");
            }

            if (original.StartsWith("g"))
            {
                throw new Exception("D3 does not support the 'g' or 'gg' format specifier");
            }

            if (original.StartsWith("h"))
            {
                if(original.StartsWith("hh"))
                {
                    d3Time.Append("%I");
                    original = original[2..];
                    continue;
                }
                {
                    d3Time.Append("%-I");
                    original = original[1..];
                    continue;
                }
                
            }
            if (original.StartsWith("H"))
            {
                if(original.StartsWith("HH"))
                {
                    d3Time.Append("%H");
                    original = original[2..];
                    continue;
                }
                {
                    d3Time.Append("%-H");
                    original = original[1..];
                    continue;
                }
            }
            if (original.StartsWith("K"))
            {
                d3Time.Append("%Z");
                original = original[1..];
                continue;
            }
            if (original.StartsWith("m"))
            {
                if(original.StartsWith("mm"))
                {
                    d3Time.Append("%M");
                    original = original[2..];
                    continue;
                }
                {
                    d3Time.Append("%-M");
                    original = original[1..];
                    continue;
                }
            }
            if (original.StartsWith("M"))
            {
                if (original.StartsWith("MMMM"))
                {
                    d3Time.Append("%B");
                    original = original[4..];
                    continue;
                }
                if (original.StartsWith("MMM"))
                {
                    d3Time.Append("%b");
                    original = original[3..];
                    continue;
                }
                if (original.StartsWith("MM"))
                {
                    d3Time.Append("%m");
                    original = original[2..];
                    continue;
                }
                {
                    d3Time.Append("%-m");
                    original = original[1..];
                    continue;
                }
            }
            if (original.StartsWith("s"))
            {
                if (original.StartsWith("ss"))
                {
                    d3Time.Append("%S");
                    original = original[2..];
                    continue;
                }
                {
                    d3Time.Append("%-S");
                    original = original[1..];
                    continue;
                }
            }
            if (original.StartsWith("t"))
            {
                if (original.StartsWith("tt"))
                {
                    d3Time.Append("%p");
                    original = original[2..];
                    continue;
                }
                {
                    throw new FormatException("D3 does not support the 't' format specifier");
                }
            }

            
            if (original.StartsWith("y"))
            {
                if (original.StartsWith("yyyyy"))
                {
                    throw new FormatException("D3 does not support the 'yyyyy' format specifier");
                }
                if (original.StartsWith("yyyy"))
                {
                    d3Time.Append("%Y");
                    original = original[4..];
                    continue;
                }
                if (original.StartsWith("yyy"))
                {
                    throw new FormatException("D3 does not support the 'yyy' format specifier");
                }
                if(original.StartsWith("yy"))
                {
                    d3Time.Append("%y");
                    original = original[2..];
                    continue;
                }
                {
                    throw new FormatException("D3 does not support the 'y' format specifier");
                }
            }
            if (original.StartsWith("z"))
            {
                if (original.StartsWith("zzz"))
                {
                    d3Time.Append("%Z");
                    original = original[3..];
                    continue;
                }
                if (original.StartsWith("zz"))
                {
                    d3Time.Append("%Z");
                    original = original[2..];
                    continue;
                }
                {
                    d3Time.Append("%Z");
                    original = original[1..];
                    continue;
                }
            }
        }
        throw new NotImplementedException();
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