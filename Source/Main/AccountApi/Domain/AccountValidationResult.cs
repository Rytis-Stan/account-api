namespace AccountApi.Domain;

public class AccountValidationResult
{
    public AccountValidationResult(string[] invalidLines)
    {
        InvalidLines = invalidLines;
    }

    public bool FileValid => InvalidLines.Length == 0;
    public string[] InvalidLines { get; }
}
