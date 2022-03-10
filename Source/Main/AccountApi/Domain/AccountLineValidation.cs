using System.Text.RegularExpressions;
using AccountApi.Utilities;

namespace AccountApi.Domain;

public class AccountLineValidation : IAccountLineValidation
{
    private static readonly Regex AccountOwnerNameRegex = new("^([A-Z])([A-Z]|[a-z])*$");
    private static readonly Regex AccountNumberRegex = new("^(3|4)([0-9]){6}(p?)$");

    public List<string> ErrorsIn(TextLine line)
    {
        List<string> errors = new List<string>();

        string[] parts = line.Text.Split(' ');
        if (parts.Length != 2 || parts.Any(part => part == ""))
        {
            // NOTE: The "line format" is an extra error that was not mentioned in the task specification,
            // but was added to handle any other malformed input text lines.
            errors.Add("line format");
        }
        else
        {
            string accountOwnerName = parts[0];
            string accountNumber = parts[1];

            if (!AccountOwnerNameRegex.Match(accountOwnerName).Success)
            {
                errors.Add("account name");
            }
            if (!AccountNumberRegex.Match(accountNumber).Success)
            {
                errors.Add("account number");
            }
        }

        return errors;
    }
}
