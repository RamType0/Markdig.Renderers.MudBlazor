using System.Text.Json.Serialization;

namespace Vega;

[JsonDerivedType(typeof(Number))]
[JsonDerivedType(typeof(Padding))]
[JsonDerivedType(typeof(SignalRef))]
public interface IPadding;
