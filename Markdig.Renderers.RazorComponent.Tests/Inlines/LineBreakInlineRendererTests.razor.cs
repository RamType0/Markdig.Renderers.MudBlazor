using Bunit;
using Markdig.Renderers.RazorComponent.Inlines;

namespace Markdig.Renderers.RazorComponent.Tests.Inlines;

partial class LineBreakInlineRendererTests
{
    [InlineData("""
        foo  
        baz
        """)]
    [InlineData("""
        foo\
        baz
        """)]
    [InlineData("""
        foo       
        baz
        """)]
    public partial void HardLineBreakRendersCorrectly(string content);
}
