using AccountApi.Domain;
using AccountApi.Utilities;
using NUnit.Framework;

namespace AccountApi.Tests.Domain;

public class ValidateAccountsFileCommandTests
{
    [TestCase((object) new string[0])]
    [TestCase((object) new[] { "" })]
    [TestCase((object) new[] { "any" })]
    [TestCase((object) new[] { "any", "ignored" })]
    [TestCase((object) new[] { "any", "ignored", "line" })]
    public void ReportsNoErrorsWhenAllValidationsPass(string[] lines)
    {
        IValidateAccountsFileCommand command = new ValidateAccountsFileCommand(new AlwaysPassingAccountLineValidation());

        AccountValidationResult result = command.Execute(new MultilineText(lines));
        
        Assert.That(result.FileValid, Is.True);
        Assert.That(result.InvalidLines, Is.Empty);
    }

    [Test]
    public void ReportsErrorsForLinesThatHaveFailingValidations()
    {
        Test(
            new[] { "abc" },
            new[] { LineWithErrors(1, "SomeError") },
            new[] { "SomeError - not valid for 1 line 'abc'" }
        );
        Test(
            new[] { "def" },
            new[] { LineWithErrors(1, "SomeError") },
            new[] { "SomeError - not valid for 1 line 'def'" }
        );
        Test(
            new[] { "def" },
            new[] { LineWithErrors(1, "someError") },
            new[] { "SomeError - not valid for 1 line 'def'" }
        );
        Test(
            new[] { "def" },
            new[] { LineWithErrors(1, "random stuff") },
            new[] { "Random stuff - not valid for 1 line 'def'" }
        );
        Test(
            new[] { "def" },
            new[] { LineWithErrors(1, "random stuff", "other error") },
            new[] { "Random stuff, other error - not valid for 1 line 'def'" }
        );
        Test(
            new[] { "def" },
            new[] { LineWithErrors(1, "random stuff", "error2") },
            new[] { "Random stuff, error2 - not valid for 1 line 'def'" }
        );
        Test(
            new[] { "def", "abc" },
            new[] { LineWithErrors(1, "random stuff", "error2") },
            new[] { "Random stuff, error2 - not valid for 1 line 'def'" }
        );
        Test(
            new[] { "def", "abc" },
            new[] { LineWithErrors(2, "random stuff", "error2") },
            new[] { "Random stuff, error2 - not valid for 2 line 'abc'" }
        );
        Test(
            new[] { "def", "abc" },
            new[]
            {
                LineWithErrors(1, "error1"),
                LineWithErrors(2, "random stuff", "error2")
            },
            new[]
            {
                "Error1 - not valid for 1 line 'def'",
                "Random stuff, error2 - not valid for 2 line 'abc'"
            }
        );
        Test(
            new[] { "def", "good line", "abc" },
            new[]
            {
                LineWithErrors(1, "error1"),
                LineWithErrors(3, "random stuff", "error2")
            },
            new[]
            {
                "Error1 - not valid for 1 line 'def'",
                "Random stuff, error2 - not valid for 3 line 'abc'"
            }
        );
        void Test(string[] lines, KeyValuePair<int, string[]>[] linesWithErrors, string[] expectedInvalidLines)
        {
            IAccountLineValidation validation = new FailingAccountLineValidation(new Dictionary<int, string[]>(linesWithErrors));
            IValidateAccountsFileCommand command = new ValidateAccountsFileCommand(validation);

            AccountValidationResult result = command.Execute(new MultilineText(lines));

            Assert.That(result.FileValid, Is.False);
            Assert.That(result.InvalidLines, Is.EqualTo(expectedInvalidLines));
        }

        KeyValuePair<int, string[]> LineWithErrors(int lineNumber, params string[] errors)
        {
            return new KeyValuePair<int, string[]>(lineNumber, errors);
        }
    }

    private class AlwaysPassingAccountLineValidation : IAccountLineValidation
    {
        public List<string> ErrorsIn(TextLine line)
        {
            return new List<string>();
        }
    }

    private class FailingAccountLineValidation : IAccountLineValidation
    {
        private readonly Dictionary<int, string[]> _linesWithErrors;

        public FailingAccountLineValidation(Dictionary<int, string[]> linesWithErrors)
        {
            _linesWithErrors = linesWithErrors;
        }

        public List<string> ErrorsIn(TextLine line)
        {
            return _linesWithErrors.ContainsKey(line.Number)
                ? _linesWithErrors[line.Number].ToList()
                : new List<string>();
        }
    }
}
