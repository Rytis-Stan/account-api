using AccountApi.Utilities;
using NUnit.Framework;

namespace AccountApi.Tests.Utilities;

public class StringExtensionsTests
{
    [TestCase("", "")]
    [TestCase("a", "A")]
    [TestCase("b", "B")]
    [TestCase("ba", "Ba")]
    [TestCase("def", "Def")]
    [TestCase("Def", "Def")]
    public void ConvertsFirstCharacterOfATextIntoUppercase(string text, string expected)
    {
        Assert.That(text.FirstCharToUpper(), Is.EqualTo(expected));
    }
}
