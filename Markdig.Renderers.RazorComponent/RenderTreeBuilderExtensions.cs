using Markdig.Renderers.Html;
using Microsoft.AspNetCore.Components.Rendering;

namespace Markdig.Renderers.RazorComponent;

public static class RenderTreeBuilderExtensions
{
    public static void AddAttributes(this RenderTreeBuilder bulder, int sequence, HtmlAttributes? attributes, Func<string, string>? classFilter = null)
    {
        {
            if (attributes is null)
            {
                return;
            }
            if (attributes.Id is { } id)
            {
                bulder.AddAttribute(sequence, "id", id);
            }
            if (attributes.Classes is { Count: > 0 } cssClasses)
            {
                var cssClassTextSource = classFilter is null ? cssClasses : cssClasses.Select(classFilter);
                var cssClassText = string.Join(' ', cssClassTextSource);
                bulder.AddAttribute(sequence, "class", cssClassText);
            }
            if (attributes.Properties is { Count: > 0 } properties)
            {
                foreach (var property in properties)
                {
                    bulder.AddAttribute(sequence, property.Key, property.Value);
                }
            }
        }
    }
}
