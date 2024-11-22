using Common.Net8.Response;

namespace Common.Net8.Tests.Response;

public class ResponseDefaultTests
{
    [Fact]
    public void CanCreateResponseDefaultWithInitialValues()
    {
        // Arrange & Act
        var response = new ResponseDefault
        {
            Message = "Operation successful",
            Success = true,
            Data = new { Detail = "More info here" }
        };

        // Assert
        Assert.Equal("Operation successful", response.Message);
        Assert.True(response.Success);
        Assert.Equal("More info here", response.Data.Detail);
    }

    [Fact]
    public void ResponseDefaultHandlesDifferentDataTypes()
    {
        // Arrange
        var response = new ResponseDefault();
        var testData = new { Name = "Test", Value = 123 };

        // Act
        response.Data = testData;

        // Assert
        Assert.Equal("Test", response.Data.Name);
        Assert.Equal(123, response.Data.Value);
    }

    [Fact]
    public void ResponseDefaultCanHandleNullData()
    {
        // Arrange
        var response = new ResponseDefault();

        // Act
        response.Data = null;

        // Assert
        Assert.Null(response.Data);
    }
}
