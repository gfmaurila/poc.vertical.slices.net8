using Common.Net8.Constants;

namespace Common.Net8.Tests.Constants;

public class RegexPatternsTests
{
    [Theory]
    [InlineData("email@example.com", true)]
    [InlineData("firstname.lastname@example.com", true)]
    [InlineData("email@subdomain.example.com", true)]
    [InlineData("firstname+lastname@example.com", true)]
    [InlineData("email@123.123.123.123", true)]
    [InlineData("1234567890@example.com", true)]
    [InlineData("email@example-one.com", true)]
    [InlineData("email@example.name", true)]
    [InlineData("email@example.museum", true)]
    [InlineData("email@example.co.jp", true)]
    [InlineData("firstname-lastname@example.com", true)]
    [InlineData("plainaddress", false)]
    [InlineData("@missingusername.com", false)]
    [InlineData("email.example.com", false)]
    [InlineData("email@example@example.com", false)]
    [InlineData(".email@example.com", false)]
    [InlineData("email.@example.com", false)]
    [InlineData("email..email@example.com", false)]
    [InlineData("あいうえお@example.com", false)]
    [InlineData("email@example.com (Joe Smith)", false)]
    [InlineData("email@example", false)]
    [InlineData("email@-example.com", false)]
    [InlineData("email@example..com", false)]
    [InlineData("Abc..123@example.com", false)]
    public void TestEmailRegexPattern(string email, bool expected)
    {
        var result = RegexPatterns.EmailRegexPattern.IsMatch(email);
        Assert.Equal(expected, result);
    }
}
