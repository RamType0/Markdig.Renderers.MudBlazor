using System.Runtime.CompilerServices;
using System.Text;

namespace Vega.Embed;
/// <remarks>https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/Globalization/DateTimeFormat.cs</remarks>
internal static class D3DateTimeFormatConverter
{
    public static string ToD3Format(string dateTimeFormat)
    {
        DefaultInterpolatedStringHandler result = new(0, dateTimeFormat.Length);
        FormatCustomized(dateTimeFormat, ref result);
        return result.ToStringAndClear();
    }
    private static void FormatCustomized(scoped ReadOnlySpan<char> format, ref DefaultInterpolatedStringHandler result)
    {
        int i = 0;
        int tokenLen;

        while (i < format.Length)
        {
            char ch = format[i];
            int nextChar;
            switch (ch)
            {
                case 'g':
                    throw new FormatException("D3 does not support formatting era.");

                case 'h':
                    tokenLen = ParseRepeatPattern(format, i, ch);
                    result.AppendFormatted(tokenLen switch
                    {
                        1 => "%-I",
                        _ => "%I",
                    });
                    break;

                case 'H':
                    tokenLen = ParseRepeatPattern(format, i, ch);
                    result.AppendFormatted(tokenLen switch
                    {
                        1 => "%-H",
                        _ => "%H",
                    });
                    break;

                case 'm':
                    tokenLen = ParseRepeatPattern(format, i, ch);
                    result.AppendFormatted(tokenLen switch
                    {
                        1 => "%-M",
                        _ => "%M",
                    });
                    break;

                case 's':
                    tokenLen = ParseRepeatPattern(format, i, ch);

                    result.AppendFormatted(tokenLen switch
                    {
                        1 => "%-S",
                        _ => "%S",
                    });
                    break;

                case 'f':
                case 'F':
                    tokenLen = ParseRepeatPattern(format, i, ch);
                    if (ch == 'f')
                    {
                        result.AppendFormatted(tokenLen switch
                        {
                            3 => "%L",
                            6 => "%f",
                            _ => throw new FormatException($"D3 does not support formatting fraction part of seconds with digits:{tokenLen}"),
                        });
                    }
                    else
                    {
                        result.AppendFormatted(tokenLen switch
                        {
                            3 => "%-L",
                            6 => "%-f",
                            _ => throw new FormatException($"D3 does not support formatting fraction part of seconds with digits:{tokenLen}"),
                        });
                    }
                    break;

                case 't':
                    tokenLen = ParseRepeatPattern(format, i, ch);
                    if (tokenLen == 1)
                    {
                        throw new FormatException("D3 does not support formatting first char of AM or PM.");
                    }
                    else
                    {
                        result.AppendFormatted("%p");
                    }
                    break;

                case 'd':
                    //
                    // tokenLen == 1 : Day of month as digits with no leading zero.
                    // tokenLen == 2 : Day of month as digits with leading zero for single-digit months.
                    // tokenLen == 3 : Day of week as a three-letter abbreviation.
                    // tokenLen >= 4 : Day of week as its full name.
                    //
                    tokenLen = ParseRepeatPattern(format, i, ch);

                    result.AppendFormatted(tokenLen switch
                    {
                        1 => "%-d",
                        2 => "%d",
                        3 => "%a",
                        _ => "%A",
                    });
                    break;

                case 'M':
                    // tokenLen == 1 : Month as digits with no leading zero.
                    // tokenLen == 2 : Month as digits with leading zero for single-digit months.
                    // tokenLen == 3 : Month as a three-letter abbreviation.
                    // tokenLen >= 4 : Month as its full name.
                    tokenLen = ParseRepeatPattern(format, i, ch);

                    result.AppendFormatted(tokenLen switch
                    {
                        1 => "%-m",
                        2 => "%m",
                        3 => "%b",
                        _ => "%B",
                    });
                    break;

                case 'y':
                    // Notes about OS behavior:
                    // y: Always print (year % 100). No leading zero.
                    // yy: Always print (year % 100) with leading zero.
                    // yyy/yyyy/yyyyy/... : Print year value.  With leading zeros.

                    tokenLen = ParseRepeatPattern(format, i, ch);

                    result.AppendFormatted(tokenLen switch
                    {
                        1 => "%-y",
                        2 => "%y",
                        _ => "%Y",
                    });
                    break;

                case 'z':
                    tokenLen = ParseRepeatPattern(format, i, ch);
                    result.AppendFormatted("%Z");
                    break;

                case 'K':
                    tokenLen = 1;
                    result.AppendFormatted("%Z");
                    break;

                case ':':
                    result.AppendFormatted(":");
                    tokenLen = 1;
                    break;

                case '/':
                    result.AppendFormatted("/");
                    tokenLen = 1;
                    break;

                case '\'':
                case '\"':
                    tokenLen = ParseQuoteString(format, i, ref result);
                    break;

                case '%':
                    // Optional format character.
                    // For example, format string "%d" will print day of month
                    // without leading zero.  Most of the cases, "%" can be ignored.
                    nextChar = ParseNextChar(format, i);
                    // nextChar will be -1 if we have already reached the end of the format string.
                    // Besides, we will not allow "%%" to appear in the pattern.
                    if (nextChar >= 0 && nextChar != '%')
                    {
                        char nextCharChar = (char)nextChar;
                        FormatCustomized(new ReadOnlySpan<char>(in nextCharChar), ref result);
                        tokenLen = 2;
                    }
                    else
                    {
                        //
                        // This means that '%' is at the end of the format string or
                        // "%%" appears in the format string.
                        //
                        throw new FormatException("'%' is at the end of the format string or appears in the format string.");
                    }
                    break;

                case '\\':
                    // Escaped character.  Can be used to insert a character into the format string.
                    // For example, "\d" will insert the character 'd' into the string.
                    //
                    // NOTENOTE : we can remove this format character if we enforce the enforced quote
                    // character rule.
                    // That is, we ask everyone to use single quote or double quote to insert characters,
                    // then we can remove this character.
                    //
                    nextChar = ParseNextChar(format, i);
                    if (nextChar >= 0)
                    {
                        if(nextChar is '%')
                        {
                            result.AppendFormatted("%%");
                        }
                        else
                        {

                            result.AppendFormatted((char)nextChar);
                        }
                        tokenLen = 2;
                    }
                    else
                    {
                        //
                        // This means that '\' is at the end of the formatting string.
                        //
                        throw new FormatException("'\' is at the end of the formatting string.");
                    }
                    break;

                default:
                    // NOTENOTE : we can remove this rule if we enforce the enforced quote character rule.
                    // That is, if we ask everyone to use single quote or double quote to insert characters,
                    // then we can remove this default block.
                    result.AppendFormatted(ch);
                    tokenLen = 1;
                    break;
            }
            i += tokenLen;
        }
    }
    internal static int ParseRepeatPattern(ReadOnlySpan<char> format, int pos, char patternChar)
    {
        int index = pos + 1;
        while ((uint)index < (uint)format.Length && format[index] == patternChar)
        {
            index++;
        }
        return index - pos;
    }
    //
    // The pos should point to a quote character. This method will
    // append to the result StringBuilder the string enclosed by the quote character.
    //
    internal static int ParseQuoteString(scoped ReadOnlySpan<char> format, int pos, ref DefaultInterpolatedStringHandler result)
    {
        //
        // NOTE : pos will be the index of the quote character in the 'format' string.
        //
        int formatLen = format.Length;
        int beginPos = pos;
        char quoteChar = format[pos++]; // Get the character used to quote the following string.

        bool foundQuote = false;
        while (pos < formatLen)
        {
            char ch = format[pos++];
            if (ch == quoteChar)
            {
                foundQuote = true;
                break;
            }
            else if (ch == '\\')
            {
                // The following are used to support escaped character.
                // Escaped character is also supported in the quoted string.
                // Therefore, someone can use a format like "'minute:' mm\"" to display:
                //  minute: 45"
                // because the second double quote is escaped.
                if (pos < formatLen)
                {
                    var escapedChar = format[pos++];
                    if(escapedChar is '%')
                    {
                        result.AppendFormatted("%%");
                    }
                    else
                    {
                        result.AppendFormatted(escapedChar);
                    }
                }
                else
                {
                    //
                    // This means that '\' is at the end of the formatting string.
                    //
                    throw new FormatException("'\' is at the end of the formatting string.");
                }
            }
            else
            {
                if (ch is '%')
                {
                    result.AppendFormatted("%%");
                }
                else
                {
                    result.AppendFormatted(ch);
                }
            }
        }

        if (!foundQuote)
        {
            // Here we can't find the matching quote.
            throw new FormatException("Quote string is unclosed.");
        }

        //
        // Return the character count including the begin/end quote characters and enclosed string.
        //
        return pos - beginPos;
    }

    //
    // Get the next character at the index of 'pos' in the 'format' string.
    // Return value of -1 means 'pos' is already at the end of the 'format' string.
    // Otherwise, return value is the int value of the next character.
    //
    internal static int ParseNextChar(ReadOnlySpan<char> format, int pos)
    {
        if ((uint)(pos + 1) >= (uint)format.Length)
        {
            return -1;
        }
        return format[pos + 1];
    }
}