using Markdig.Extensions.Tables;
using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Diagnostics;

namespace Markdig.Renderers.MudBlazor;

public class TableRenderer : RazorComponentObjectRenderer<Table>
{
    static readonly object BoxedTrue = true;
    public bool UseSimpleTable { get; set; } = false;
    protected override void Write(RazorComponentRenderer renderer, Table table)
    {
        var builder = renderer.Builder;
        builder.OpenRegion(0);
        {
            if (UseSimpleTable)
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
            else
            {
                builder.OpenComponent<MudTable<TableRow>>(3);
                {
                    builder.AddAttributesToMudComponent(4, table.TryGetAttributes());
                    builder.AddComponentParameter(5, nameof(MudTable<TableRow>.CustomHeader), BoxedTrue);
                    builder.AddComponentParameter(6, nameof(MudTable<TableRow>.Items), table.Cast<TableRow>().Where(row => !row.IsHeader));
                    builder.AddComponentParameter(7, nameof(MudTable<TableRow>.HeaderContent), (RenderFragment)(builder =>
                    {
                        using (renderer.UseBuilder(builder))
                        {
                            foreach (var rowObj in table)
                            {
                                var row = (TableRow)rowObj;
                                if (row.IsHeader)
                                {
                                    builder.OpenComponent<MudTHeadRow>(0);
                                    {
                                        builder.AddAttributesToMudComponent(1, row.TryGetAttributes());
                                        builder.AddComponentParameter(2, nameof(MudTHeadRow.ChildContent), (RenderFragment)(builder =>
                                        {
                                            using (renderer.UseBuilder(builder))
                                            {
                                                for (int i = 0; i < row.Count; i++)
                                                {
                                                    RenderCell<MudTh>(renderer, 0, row, i);
                                                }
                                            }
                                        }));
                                    }
                                    builder.CloseComponent();
                                }
                            }
                        }
                    }));
                    builder.AddComponentParameter(8, nameof(MudTable<TableRow>.RowTemplate), (RenderFragment<TableRow>)(row => builder =>
                    {
                        using (renderer.UseBuilder(builder))
                        {
                            for (int i = 0; i < row.Count; i++)
                            {
                                RenderCell<MudTd>(renderer, 0, row, i);
                            }
                            
                        }
                    }));
                }
                builder.CloseComponent();
            }
        }
        builder.CloseRegion();
    }
    static void RenderCell<T>(RazorComponentRenderer renderer, int sequence, TableRow row, int cellIndex)
            where T : MudComponentBase
    {
        var builder = renderer.Builder;
        var table = (Table?)row.Parent ?? throw new UnreachableException();
        builder.OpenRegion(sequence);
        {
            builder.OpenComponent<T>(0);
            {
                var cell = (TableCell)row[cellIndex];
                if (cell.ColumnSpan != 1)
                {
                    builder.AddComponentParameter(0, "colspan", cell.ColumnSpan.ToString());
                }
                if (cell.RowSpan != 1)
                {
                    builder.AddComponentParameter(1, "rowspan", cell.RowSpan.ToString());
                }
                if (table.ColumnDefinitions.Count > 0)
                {
                    var columnIndex = cell.ColumnIndex < 0 || cell.ColumnIndex >= table.ColumnDefinitions.Count
        ? cellIndex
        : cell.ColumnIndex;
                    columnIndex = columnIndex >= table.ColumnDefinitions.Count ? table.ColumnDefinitions.Count - 1 : columnIndex;
                    var alignment = table.ColumnDefinitions[columnIndex].Alignment;
                    if (alignment.HasValue)
                    {
                        var style = alignment switch
                        {
                            TableColumnAlign.Center => "text-align: center;",
                            TableColumnAlign.Right => "text-align: right;",
                            TableColumnAlign.Left => "text-align: left;",
                            _ => null
                        };
                        builder.AddComponentParameter(2, nameof(MudComponentBase.Style), style);
                    }

                }
                builder.AddAttributesToMudComponent(3, cell.TryGetAttributes());
                builder.AddComponentParameter(4, "ChildContent", (RenderFragment)(builder =>
                {
                    using (renderer.UseBuilder(builder))
                    {
                        if (cell.Count == 1)
                        {
                            builder.AddImplicitParagraph(0, true, true, builder =>
                            {
                                using (renderer.UseBuilder(builder))
                                {
                                    renderer.Write(cell);
                                }
                            });
                        }
                        else
                        {
                            builder.OpenRegion(1);
                            {
                                renderer.Write(cell);
                            }
                            builder.CloseRegion();
                        }
                    }
                    
                }));
            }
            builder.CloseComponent();


        }
        builder.CloseRegion();

    }
}