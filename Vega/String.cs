using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Vega.Json.Converters;

namespace Vega;

[JsonConverter(typeof(StringJsonConverter))]
public record String(string Value) : IBackground
{
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator String?(string? value) => value is null ? null : new(value);
}
