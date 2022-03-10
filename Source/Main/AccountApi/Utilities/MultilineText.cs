namespace AccountApi.Utilities;

public class MultilineText : IText
{
    private readonly string[] _lines;

    public MultilineText(params string[] lines)
    {
        _lines = lines;
    }

    public IEnumerable<TextLine> Lines()
    {
        return _lines.Select((text, index) => new TextLine(text, index + 1));
    }
}