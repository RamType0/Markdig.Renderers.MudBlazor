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
        var sequence = 0;
        builder.OpenRegion(sequence);
        {
            builder.OpenComponent<MudSimpleTable>(0);
            {
                builder.AddAttributesToMudComponent(1, table.TryGetAttributes());
                builder.AddAttribute(2, nameof(MudSimpleTable.ChildContent), (RenderFragment)(builder =>
                {
                    var originalBuilder = renderer.Builder;
                    {
                        renderer.Builder = builder;
                        RazorComponent.TableRenderer.RenderTableContent(renderer, 0, table);
                    }
                    renderer.Builder = originalBuilder;


                }));
            }
            builder.CloseComponent();
        }

        builder.CloseRegion();
    }

}