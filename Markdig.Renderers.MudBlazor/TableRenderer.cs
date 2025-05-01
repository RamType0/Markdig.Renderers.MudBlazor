using Markdig.Extensions.Tables;
using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Markdig.Renderers.MudBlazor;

public class TableRenderer : RazorComponentObjectRenderer<Table>
{
    protected override void Write(RazorComponentRenderer renderer, Table table)
    {
        var builder = renderer.Builder;
        builder.OpenRegion(0);
        {
            builder.OpenComponent<MudSimpleTable>(0);
            {
                builder.AddAttributesToMudComponent(1, table.TryGetAttributes());
                builder.AddComponentParameter(2, nameof(MudSimpleTable.ChildContent), (RenderFragment)(builder =>
                {
                    using (renderer.UseBuilder(builder))
                    {
                        RazorComponent.TableRenderer.RenderTableContent(renderer, 0, table);
                    }
                }));
            }
            builder.CloseComponent();
        }

        builder.CloseRegion();
    }

}