using Common.Net8.ValueObjects;

namespace Common.Net8.Tests.ValueObjects;

public class EmailTests
{
    [Theory]
    [InlineData("  EXAMPLE@Email.com  ", "example@email.com")]
    [InlineData("Test@Example.COM", "test@example.com")]
    public void Email_Constructor_ShouldNormalizeAddress(string input, string expected)
    {
        // Act
        var email = new Email(input);

        // Assert
        Assert.Equal(expected, email.Address);
    }

    [Fact]
    public void Email_ObjectsWithSameAddress_ShouldBeEqual()
    {
        // Arrange
        var address = "test@example.com";
        var email1 = new Email(address);
        var email2 = new Email(address);

        // Act & Assert
        Assert.Equal(email1, email2);
    }

    [Fact]
    public void Email_ToString_ReturnsCorrectAddress()
    {
        // Arrange
        var address = "test@example.com";
        var email = new Email(address);

        // Act
        var result = email.ToString();

        // Assert
        Assert.Equal(address, result);
    }
}
