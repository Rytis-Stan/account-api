using AccountApi.Domain;
using AccountApi.Dto;
using AccountApi.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace AccountApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AccountsController : ControllerBase
{
    private readonly IValidateAccountsFileCommand _validateAccountsFileCommand;

    public AccountsController(IValidateAccountsFileCommand validateAccountsFileCommand)
    {
        _validateAccountsFileCommand = validateAccountsFileCommand;
    }

    [HttpPost]
    public AccountValidationResultDto Validate([FromBody] string accountsFile)
    {
        return _validateAccountsFileCommand.Execute(new TextFromString(accountsFile)).ToDto();
    }
}