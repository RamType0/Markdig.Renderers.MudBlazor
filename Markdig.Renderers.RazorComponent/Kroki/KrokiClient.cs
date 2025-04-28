using System.Collections.Specialized;
using System.IO.Compression;
using System.Net.Http.Json;
using System.Web;

namespace Kroki;

public class KrokiClient
{
    public static Uri DefaultEndpoint { get; } = new("https://kroki.io");
    public KrokiClient() : this(DefaultEndpoint) { }
    /// <param name="endpoint">The Kroki endpoint to use. This should not include diagram type or output format. For example: <c>https://kroki.io</c>.</param>
    public KrokiClient(Uri endpoint)
    {
        Endpoint = endpoint;
    }

    Uri Endpoint { get; }
    public HttpRequestMessage CreatePostRequest(KrokiRequest request)
    {
        return new()
        {
            RequestUri = Endpoint,
            Content = JsonContent.Create(request),
        };
    }
    public Uri CreateGetUri(KrokiRequest request, CompressionLevel diagramCompressionLevel) 
    {
        NameValueCollection? query;
        if(request.DiagramOptions is { } options)
        {
            query = HttpUtility.ParseQueryString("");
            foreach (var keyValue in options)
            {
                query.Add(keyValue.Key, keyValue.Value);
            }
        }
        else
        {
            query = null;
        }
        var encodedDiagram = DiagramEncoder.EncodeToString(request.DiagramSource, diagramCompressionLevel);
        Uri requestUri = new(Endpoint, $"{request.DiagramType}/{request.OutputFormat.ToEndpointPath()}/{encodedDiagram}?{query}");
        return requestUri;
    }
}

public class KrokiClientOptions
{
    public Uri Endpoint { get; set; } = KrokiClient.DefaultEndpoint;
}