using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;
using HtmlAttributes = Markdig.Renderers.Html.HtmlAttributes;

namespace Markdig.Renderers.MudBlazor;

public static class RenderTreeBuilderExtensions
{
    public static void AddAttributesToMudComponent(this RenderTreeBuilder builder, int sequence, HtmlAttributes? attributes, Func<string, string>? classFilter = null)
    {
        {
            if (attributes is null)
            {
                return;
            }
            if (attributes.Id is { } id)
            {
                builder.AddComponentParameter(sequence, "id", id);
            }
            if (attributes.Classes is { Count: > 0 } cssClasses)
            {
                var cssClassTextSource = classFilter is null ? cssClasses : cssClasses.Select(classFilter);
                var cssClassText = string.Join(' ', cssClassTextSource);
                builder.AddComponentParameter(sequence, nameof(MudComponentBase.Class), cssClassText);
            }
            if (attributes.Properties is { Count: > 0 } properties)
            {
                foreach (var property in properties)
                {
                    builder.AddComponentParameter(sequence, property.Key, property.Value);
                }
            }
        }
    }
}