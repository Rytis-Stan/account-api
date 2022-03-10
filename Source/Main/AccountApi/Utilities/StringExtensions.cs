namespace AccountApi.Utilities;

public static class StringExtensions
{
    public static string FirstCharToUpper(this string str)
    {
        return str.Length > 1
            ? str[..1].ToUpper() + str[1..]
            : str.ToUpper();
    }
}
