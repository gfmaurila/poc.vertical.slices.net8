using Common.Net8.Response;

namespace Common.Net8.Tests.Response;

public class ResponseAuthTests
{
    [Fact]
    public void CanAssignStringToData()
    {
        // Arrange
        var response = new ResponseAuth();
        var testData = "Test String";

        // Act
        response.Data = testData;

        // Assert
        Assert.Equal(testData, response.Data);
    }

    [Fact]
    public void CanAssignObjectToData()
    {
        // Arrange
        var response = new ResponseAuth();
        var testData = new { Name = "Test", Value = 123 };

        // Act
        response.Data = testData;

        // Assert
        Assert.Equal("Test", response.Data.Name);
        Assert.Equal(123, response.Data.Value);
    }

    [Fact]
    public void CanAssignNullToData()
    {
        // Arrange
        var response = new ResponseAuth();

        // Act
        response.Data = null;

        // Assert
        Assert.Null(response.Data);
    }
}
