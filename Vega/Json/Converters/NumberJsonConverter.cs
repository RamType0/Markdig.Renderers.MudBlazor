using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vega.Json.Converters;

public sealed class NumberJsonConverter : JsonConverter<Number>
{
    public override Number? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetDouble();
    }

    public override void Write(Utf8JsonWriter writer, Number value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.Value);
    }
}