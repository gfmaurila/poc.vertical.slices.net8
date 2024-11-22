using Common.Net8.Extensions;

namespace Common.Net8.Tests.Extensions;

public class ExceptionsTests
{

    [Fact]
    public void Constructor_InitializesErrorList_WithProvidedErrors()
    {
        // Arrange
        var errors = new List<string> { "Error1", "Error2" };

        // Act
        var ex = new Exceptions("Error occurred", errors);

        // Assert
        Assert.Equal(errors.Count, ex.Errors.Count);
        Assert.Contains("Error1", ex.Errors);
        Assert.Contains("Error2", ex.Errors);
    }


    [Fact]
    public void Constructor_WithMessageAndInnerException_InitializesCorrectly()
    {
        // Arrange
        var innerException = new InvalidOperationException("Inner exception");

        // Act
        var ex = new Exceptions("Main exception", innerException);

        // Assert
        Assert.Equal("Main exception", ex.Message);
        Assert.Equal(innerException, ex.InnerException);
    }
}
