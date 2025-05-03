using System.Text.Json.Serialization;

namespace Vega;

[JsonDerivedType(typeof(String),nameof(String))]
[JsonDerivedType(typeof(SignalRef), nameof(SignalRef))]
public interface IBackground;