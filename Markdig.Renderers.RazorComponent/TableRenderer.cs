using Markdig.Extensions.Tables;
using Markdig.Renderers.Html;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace Markdig.Renderers.RazorComponent;

public class TableRenderer : RazorComponentObjectRenderer<Table>
{
    protected override void Write(RazorComponentRenderer renderer, Table table)
    {
        var builder = renderer.Builder;
        builder.OpenRegion(0);
        {
            builder.OpenElement(0, "table");
            {
                builder.AddAttributes(1, table.TryGetAttributes());
                RenderTableContent(renderer, 2, table);
            }
            builder.CloseElement();
        }

        builder.CloseRegion();
    }

    public static void RenderTableContent(RazorComponentRenderer renderer, int sequence, Table table)
    {
        var builder = renderer.Builder;
        builder.OpenRegion(sequence);
        {
            builder.OpenRegion(0);
            {
                foreach (var rowObj in table)
                {
                    var row = (TableRow)rowObj;
                    if (row.IsHeader)
                    {
                        builder.OpenElement(0, "thead");
                        {
                            builder.OpenElement(1, "tr");
                            {
                                builder.AddAttributes(2, row.TryGetAttributes());
                                for (int i = 0; i < row.Count; i++)
                                {
                                    RenderCell(renderer, 3, "th", row, i);
                                }
                            }
                            builder.CloseElement();

                        }
                        builder.CloseElement();
                        break;
                    }
                }
            }
            builder.CloseRegion();
            builder.OpenRegion(1);
            {
                builder.OpenElement(0, "tbody");
                {
                    foreach (var rowObj in table)
                    {
                        var row = (TableRow)rowObj;
                        if (!row.IsHeader)
                        {
                            builder.OpenElement(1, "tr");
                            {
                                builder.AddAttributes(2, row.TryGetAttributes());
                                for (int i = 0; i < row.Count; i++)
                                {
                                    RenderCell(renderer, 3, "td", row, i);

                                }
                            }
                            builder.CloseElement();
                        }
                    }
                }
                builder.CloseElement();
            }
            builder.CloseRegion();
        }
        builder.CloseRegion();

    }

    static void RenderCell(RazorComponentRenderer renderer, int sequence, string elementName, TableRow row, int cellIndex)
    {
        var builder = renderer.Builder;
        var table = (Table?)row.Parent ?? throw new UnreachableException();
        builder.OpenRegion(sequence);
        {
            builder.OpenElement(0, elementName);
            {
                var cell = (TableCell)row[cellIndex];
                if (cell.ColumnSpan != 1)
                {
                    builder.AddAttribute(0, "colspan", cell.ColumnSpan.ToString());
                }
                if (cell.RowSpan != 1)
                {
                    builder.AddAttribute(1, "rowspan", cell.RowSpan.ToString());
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
                        builder.AddAttribute(2, "style", style);
                    }

                }
                builder.AddAttributes(3, cell.TryGetAttributes());
                if(cell.Count == 1)
                {
                    builder.AddImplicitParagraph(4, true, true, builder =>
                    {
                        using (renderer.UseBuilder(builder))
                        {
                            renderer.Write(cell);
                        }
                    });
                }
                else
                {
                    builder.OpenRegion(5);
                    {
                        renderer.Write(cell);
                    }
                    builder.CloseRegion();
                }
            }
            builder.CloseElement();


        }
        builder.CloseRegion();

    }
}