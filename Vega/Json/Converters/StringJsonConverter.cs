using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vega.Json.Converters;

public sealed class StringJsonConverter : JsonConverter<String>
{
    public override String? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetString();
    }

    public override void Write(Utf8JsonWriter writer, String value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}