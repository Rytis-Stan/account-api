using System.Net;
using System.Net.Http.Json;
using AccountApi.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace AccountApi.Tests;

public class ApiTests
{
    private WebApplicationFactory<Program> _app;
    private HttpClient _client;

    [SetUp]
    public void BeforeEach()
    {
        _app = new WebApplicationFactory<Program>();
        _client = _app.CreateClient();
    }

    [TearDown]
    public void AfterEach()
    {
        _app.Dispose();
        _client.Dispose();
    }

    [Test]
    public async Task ReportsNoErrorsInGoodAccountsFile()
    {
        HttpResponseMessage response = await PostAccountsFileAsync("Abc 3210987p");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        AccountValidationResultDto validationResult = await response.ContentAsJsonAsync<AccountValidationResultDto>();
        Assert.That(validationResult.FileValid, Is.True);
        Assert.That(validationResult.InvalidLines, Is.Null);
    }

    [Test]
    public async Task ReportedJsonDoesNotContainInvalidLinesPropertyWhenPassingInAGoodAccountsFile()
    {
        HttpResponseMessage response = await PostAccountsFileAsync("Abc 3210987p");
        string validationResultJson = await response.Content.ReadAsStringAsync();
        Assert.That(validationResultJson, Is.EqualTo("{\"fileValid\":true}"));
    }

    [Test]
    public async Task ReportsErrorsInBadAccountsFile()
    {
        string accountsFileContents = string.Join(
            Environment.NewLine,
            "Thomas 32999921",
            "Richard 3293982",
            "XAEA-12 8293982",
            "Rose 329a982",
            "Bob 329398.",
            "michael 3113902",
            "Rob 3113902p"
        );
        string[] expectedInvalidLines =
        {
            "Account number - not valid for 1 line 'Thomas 32999921'",
            "Account name, account number - not valid for 3 line 'XAEA-12 8293982'",
            "Account number - not valid for 4 line 'Rose 329a982'",
            "Account number - not valid for 5 line 'Bob 329398.'",
            "Account name - not valid for 6 line 'michael 3113902'"
        };

        HttpResponseMessage response = await PostAccountsFileAsync(accountsFileContents);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        AccountValidationResultDto validationResult = await response.ContentAsJsonAsync<AccountValidationResultDto>();
        Assert.That(validationResult.FileValid, Is.False);
        Assert.That(validationResult.InvalidLines, Is.EqualTo(expectedInvalidLines));
    }

    private async Task<HttpResponseMessage> PostAccountsFileAsync(string accountsFile)
    {
        return await _client.PostAsJsonAsync("accounts/validate", accountsFile);
    }
}