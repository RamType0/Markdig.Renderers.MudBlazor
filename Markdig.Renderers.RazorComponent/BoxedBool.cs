namespace Markdig.Renderers.RazorComponent;

internal static class BoxedBool
{

    public static readonly object True = true, False = false;
    public static object Value(bool value) => value ? True : False;
}