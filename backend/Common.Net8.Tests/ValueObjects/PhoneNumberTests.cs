using Common.Net8.ValueObjects;

namespace Common.Net8.Tests.ValueObjects;

public class PhoneNumberTests
{
    [Theory]
    [InlineData("  123-456-7890  ", "123-456-7890")]
    [InlineData(" (123) 456 7890 ", "(123) 456 7890")]
    public void PhoneNumber_Constructor_ShouldNormalizePhone(string input, string expected)
    {
        // Act
        var phone = new PhoneNumber(input);

        // Assert
        Assert.Equal(expected, phone.Phone);
    }

    [Fact]
    public void PhoneNumber_ObjectsWithSamePhone_ShouldBeEqual()
    {
        // Arrange
        var number = "123-456-7890";
        var phone1 = new PhoneNumber(number);
        var phone2 = new PhoneNumber(number);

        // Act & Assert
        Assert.Equal(phone1, phone2);
    }

    [Fact]
    public void PhoneNumber_ToString_ReturnsCorrectPhone()
    {
        // Arrange
        var number = "123-456-7890";
        var phone = new PhoneNumber(number);

        // Act
        var result = phone.ToString();

        // Assert
        Assert.Equal(number, result);
    }
}
