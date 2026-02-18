using Markdig.Extensions.Alerts;
using Markdig.Helpers;
using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Markdig.Renderers.MudBlazor;

public class AlertBlockRenderer : RazorComponentObjectRenderer<AlertBlock>
{
    protected override void Write(RazorComponentRenderer renderer, AlertBlock obj)
    {
        var builder = renderer.Builder;
        builder.OpenRegion(0);
        {
            builder.OpenComponent<MudAlert>(0);
            {
                builder.AddAttributesToMudComponent(1, obj.TryGetAttributes());
                var (severity, icon) = ToSeverityAndIcon(obj.Kind);
                builder.AddComponentParameter(2, nameof(MudAlert.Severity), severity);
                builder.AddComponentParameter(3, nameof(MudAlert.Icon), icon);
                builder.AddComponentParameter(4, nameof(MudAlert.ChildContent), (RenderFragment)(builder =>
                {
                    using (renderer.UseBuilder(builder))
                    {
                        builder.AddImplicitParagraph(0, true, false, builder =>
                        {
                            using (renderer.UseBuilder(builder))
                            {
                                renderer.WriteChildren(0, obj);
                            }

                        });
                        
                    }
                    
                }));
            }
            builder.CloseComponent();
        }
        builder.CloseRegion();
    }
    static (Severity Severity, string? Icon) ToSeverityAndIcon(StringSlice kind)
    {
        Span<char> span = stackalloc char[kind.Length];
        kind.AsSpan().ToUpperInvariant(span);
        return span switch
        {
            "NOTE" => (Severity.Info, null),
            "TIP" => (Severity.Success, Icons.Material.Outlined.Lightbulb),
            "IMPORTANT" => (Severity.Info, "<svg viewBox=\"0 0 16 16\" version=\"1.1\" width=\"16\" height=\"16\" aria-hidden=\"true\"><path d=\"M0 1.75C0 .784.784 0 1.75 0h12.5C15.216 0 16 .784 16 1.75v9.5A1.75 1.75 0 0 1 14.25 13H8.06l-2.573 2.573A1.458 1.458 0 0 1 3 14.543V13H1.75A1.75 1.75 0 0 1 0 11.25Zm1.75-.25a.25.25 0 0 0-.25.25v9.5c0 .138.112.25.25.25h2a.75.75 0 0 1 .75.75v2.19l2.72-2.72a.749.749 0 0 1 .53-.22h6.5a.25.25 0 0 0 .25-.25v-9.5a.25.25 0 0 0-.25-.25Zm7 2.25v2.5a.75.75 0 0 1-1.5 0v-2.5a.75.75 0 0 1 1.5 0ZM9 9a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z\"></path></svg>"),
            "WARNING" => (Severity.Warning, null),
            "CAUTION" => (Severity.Error, null),
            _ => (Severity.Normal, null),
        };
    }
}
