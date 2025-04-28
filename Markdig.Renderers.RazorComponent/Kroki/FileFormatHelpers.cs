using System.Net.Mime;

namespace Kroki;

public static class FileFormatHelpers
{
    public static string ToMimeType(this FileFormat format) => format switch
    {
        FileFormat.Png => MediaTypeNames.Image.Png,
        FileFormat.Svg => MediaTypeNames.Image.Svg,
        FileFormat.Jpeg => MediaTypeNames.Image.Jpeg,
        FileFormat.Pdf => MediaTypeNames.Application.Pdf,
        FileFormat.Base64 => $"{MediaTypeNames.Text.Plain}; charset=x-user-defined",
        FileFormat.Txt => MediaTypeNames.Text.Plain,
        FileFormat.UTxt => $"{MediaTypeNames.Text.Plain}; charset=utf-8",
        _ => throw new ArgumentOutOfRangeException(nameof(format), $"Unexpected {nameof(FileFormat)}: {format}"),
    };
    public static string ToEndpointPath(this FileFormat format) => format switch
    {
        FileFormat.Png => "png",
        FileFormat.Svg => "svg",
        FileFormat.Jpeg => "jpeg",
        FileFormat.Pdf => "pdf",
        FileFormat.Base64 => "base64",
        FileFormat.Txt => "txt",
        FileFormat.UTxt => "utxt",
        _ => throw new ArgumentOutOfRangeException(nameof(format), $"Unexpected {nameof(FileFormat)}: {format}"),
    };
}