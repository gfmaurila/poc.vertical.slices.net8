using Common.Net8.Extensions;

namespace Common.Net8.Tests.Extensions;

public class GenericTypeExtensionsTests
{
    [Fact]
    public void GetGenericTypeName_NonGenericType_ReturnsTypeName()
    {
        // Arrange
        var obj = 5; // Int32, a non-generic type

        // Act
        var typeName = obj.GetGenericTypeName();

        // Assert
        Assert.Equal("Int32", typeName);
    }

    [Fact]
    public void GetGenericTypeName_GenericType_ReturnsFormattedTypeName()
    {
        // Arrange
        var list = new List<string>();

        // Act
        var typeName = list.GetGenericTypeName();

        // Assert
        Assert.Equal("List<String>", typeName);
    }

}
