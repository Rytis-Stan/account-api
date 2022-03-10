using AccountApi.Domain;
using AccountApi.Utilities;
using NUnit.Framework;

namespace AccountApi.Tests.Domain;

public class AccountLineValidationTests
{
    [TestCase("", new[] { "line format" })]
    [TestCase("a", new[] { "line format" })]
    [TestCase("a ", new[] { "line format" })]
    [TestCase(" a", new[] { "line format" })]
    [TestCase(" a ", new[] { "line format" })]
    [TestCase("##@$%@%", new[] { "line format" })]
    [TestCase("Abc 3456789 b", new[] { "line format" })]
    [TestCase("Abc 3456789", new string[0])]
    [TestCase("abc 3456789", new[] { "account name" })]
    [TestCase("abc 3456789t", new[] { "account name", "account number" })]
    [TestCase("a9c 3456789t", new[] { "account name", "account number" })]
    [TestCase("A9c 3456789t", new[] { "account name", "account number" })]
    [TestCase("Abc 3456789t", new[] { "account number" })]
    [TestCase("Def 3456789t", new[] { "account number" })]
    [TestCase("Def 3456789p", new string[0])]
    [TestCase("Def 4456789p", new string[0])]
    [TestCase("Def 5456789p", new[] { "account number" })]
    [TestCase(" Def 5456789p", new[] { "line format" })]
    [TestCase("Def  5456789p", new[] { "line format" })]
    [TestCase("Def 5456789p ", new[] { "line format" })]
    [TestCase("  Def  5456789p  ", new[] { "line format" })]
    [TestCase(" Def  5456789p Abc", new[] { "line format" })]
    [TestCase("Def 5456789p Abc", new[] { "line format" })]
    public void ReportsExpectedErrors(string text, string[] expectedErrors)
    {
        const int ignored = -1;
        Assert.That(
            new AccountLineValidation().ErrorsIn(new TextLine(text, ignored)),
            Is.EqualTo(expectedErrors)
        );
    }
}
