using System.Buffers;
using System.Buffers.Text;
using System.Diagnostics;
using System.IO.Compression;
using System.Text;

namespace Kroki;

public static class DiagramEncoder
{
    public static string EncodeToString(ReadOnlySpan<char> diagramSource, CompressionLevel compressionLevel)
    {
        var maxUtf8Size = Encoding.UTF8.GetMaxByteCount(diagramSource.Length);
        var buffer = ArrayPool<byte>.Shared.Rent(maxUtf8Size);
        try
        {
            var utf8Size = Encoding.UTF8.GetBytes(diagramSource, buffer);
            var utf8DiagramSource = buffer.AsSpan()[..utf8Size];
            return EncodeToString(utf8DiagramSource, compressionLevel);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
        
    }
    public static string EncodeToString(ReadOnlySpan<byte> utf8DiagramSource, CompressionLevel compressionLevel)
    {
        using MemoryStream memoryStream = new();

        using (ZLibStream zlibStream = new(memoryStream, compressionLevel, leaveOpen: true))
        {
            zlibStream.Write(utf8DiagramSource);
        }
        if(!memoryStream.TryGetBuffer(out var buffer))
        {
            throw new UnreachableException("Could not get inner buffer from memory stream.");
        }
        return Base64Url.EncodeToString(buffer);
    }
}
