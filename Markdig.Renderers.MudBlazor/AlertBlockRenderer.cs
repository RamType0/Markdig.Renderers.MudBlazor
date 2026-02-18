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
            "IMPORTANT" => (Severity.Info, Icons.Material.Outlined.NotificationImportant),
            "WARNING" => (Severity.Warning, null),
            "CAUTION" => (Severity.Error, null),
            _ => (Severity.Normal, null),
        };
    }
}
