using AccountApi.Utilities;

namespace AccountApi.Domain;

public class ValidatedAccountLine
{
    private readonly TextLine _line;
    private readonly List<string> _errors;

    public ValidatedAccountLine(TextLine line, List<string> errors)
    {
        _line = line;
        _errors = errors;
    }

    public bool HasErrors()
    {
        return _errors.Any();
    }

    public string ErrorMessage()
    {
        return $"{string.Join(", ", _errors)} - not valid for {_line.Number} line '{_line.Text}'".FirstCharToUpper();
    }
}
