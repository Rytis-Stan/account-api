namespace AccountApi.Utilities;

public class TextLine
{
    public TextLine(string text, int number)
    {
        Text = text;
        Number = number;
    }

    public string Text { get; }
    public int Number { get; }
}
