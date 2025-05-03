using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Vega;

public record SignalRef : IBackground, IWidth, IHeight, IPadding
{
    [JsonPropertyName("signal")]
    public required string Signal { get; init; }
}


public record AutoSize
{
   
}

public enum AutoSizeType
{
    Pad,
    Fit,
    FitX,
    FitY,
    None,
}

public sealed class Union<T1,T2>
    where T1: notnull
    where T2: notnull
{
    public Union(T1 value) => this.value = value;
    public Union(T2 value) => this.value = value;

    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator Union<T1, T2>?(T1? value) => value is null ? null : new(value);
    [return: NotNullIfNotNull(nameof(value))]
    public static implicit operator Union<T1, T2>?(T2? value) => value is null ? null : new(value);
    private object? value;

    public object? Value
    {
        get => value;
        set
        {
            if(value is T1 or T2)
            {
                this.value = value;
            }
            else
            {
                if(value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                else
                {
                    throw new ArgumentException($"The specified value is not assignable type to this union. Type:{value.GetType()}", nameof(value));
                }
                    
            }
        }
    }

    public T GetValue<T>()
    {
        if(value is T typedValue)
        {
            return typedValue;
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

}