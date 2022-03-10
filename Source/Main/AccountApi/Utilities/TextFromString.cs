namespace AccountApi.Utilities;

public class TextFromString : IText
{
    private readonly string _text;

    public TextFromString(string text)
    {
        _text = text;
    }

    public IEnumerable<TextLine> Lines()
    {
        return _text
            .Split(Environment.NewLine)
            .Select((line, index) => new TextLine(line, index + 1));
    }
}