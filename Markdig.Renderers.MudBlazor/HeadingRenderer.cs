﻿using Markdig.Renderers.Html;
using Markdig.Renderers.RazorComponent;
using Markdig.Syntax;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Markdig.Renderers.MudBlazor;

public class HeadingRenderer : RazorComponentObjectRenderer<HeadingBlock>
{
    protected override void Write(RazorComponentRenderer renderer, HeadingBlock obj)
    {
        var builder = renderer.Builder;
        var sequence = 0;
        builder.OpenRegion(sequence);
        {
            builder.OpenComponent<MudText>(0);
            {
                builder.AddAttributesToMudComponent(1, obj.TryGetAttributes());
                builder.AddAttribute(2, nameof(MudLink.ChildContent), (RenderFragment)(builder =>
                {
                    var originalBuilder = renderer.Builder;
                    {
                        renderer.Builder = builder;
                        renderer.WriteLeafInline(0, obj);
                    }
                    renderer.Builder = originalBuilder;
                }));
                builder.AddAttribute(3, nameof(MudText.Typo), GetHeadingTypo(obj));
            }

            builder.CloseComponent();
        }
        builder.CloseRegion();
    }
    public Func<HeadingBlock, Typo> GetHeadingTypo { get; set; } = GetHeadingDefaultTypo;
    public static Typo GetHeadingDefaultTypo(HeadingBlock headingBlock) => headingBlock.Level switch
    {
        1 => Typo.h1,
        2 => Typo.h2,
        3 => Typo.h3,
        4 => Typo.h4,
        5 => Typo.h5,
        6 => Typo.h6,
        _ => Typo.h6,
    };
}