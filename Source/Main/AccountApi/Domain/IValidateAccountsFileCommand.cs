using AccountApi.Utilities;

namespace AccountApi.Domain;

public interface IValidateAccountsFileCommand
{
    AccountValidationResult Execute(IText accounts);
}
