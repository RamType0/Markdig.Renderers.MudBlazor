using System.Text.Json.Serialization;
using Vega.Json.Converters;

namespace Vega;

[JsonConverter(typeof(NumberJsonConverter))]
public record Number(double Value) : IWidth, IHeight, IPadding
{
    public static implicit operator Number(double value) => new(value);
}

[JsonDerivedType(typeof(Number))]
[JsonDerivedType(typeof(SignalRef))]
public interface IWidth;
[JsonDerivedType(typeof(Number))]
[JsonDerivedType(typeof(SignalRef))]
public interface IHeight;