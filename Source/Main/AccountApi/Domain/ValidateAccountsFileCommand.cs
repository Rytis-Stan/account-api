using AccountApi.Utilities;

namespace AccountApi.Domain;

public class ValidateAccountsFileCommand : IValidateAccountsFileCommand
{
    private readonly IAccountLineValidation _lineValidation;

    public ValidateAccountsFileCommand(IAccountLineValidation lineValidation)
    {
        _lineValidation = lineValidation;
    }

    public AccountValidationResult Execute(IText accounts)
    {
        string[] invalidLines = accounts.Lines()
            .Select(line => new ValidatedAccountLine(line, _lineValidation.ErrorsIn(line)))
            .Where(line => line.HasErrors())
            .Select(line => line.ErrorMessage())
            .ToArray();
        return new AccountValidationResult(invalidLines);
    }
}
