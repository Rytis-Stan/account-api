namespace AccountApi.Utilities;

public class Text : IText
{
    private readonly string _text;

    public Text(string text)
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