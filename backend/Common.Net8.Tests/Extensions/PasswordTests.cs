using Common.Net8.Extensions;

namespace Common.Net8.Tests.Extensions;

public class PasswordTests
{
    [Fact]
    public void ComputeSha256Hash_ReturnsConsistentHash_ForSameInput()
    {
        // Arrange
        var password = "testPassword123";

        // Act
        var hash1 = Password.ComputeSha256Hash(password);
        var hash2 = Password.ComputeSha256Hash(password);

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void ComputeSha256Hash_ReturnsDifferentHashes_ForDifferentInputs()
    {
        // Arrange
        var password1 = "password123";
        var password2 = "password1234";

        // Act
        var hash1 = Password.ComputeSha256Hash(password1);
        var hash2 = Password.ComputeSha256Hash(password2);

        // Assert
        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void ComputeSha256Hash_HandlesEmptyString_Correctly()
    {
        // Arrange
        var emptyPassword = "";

        // Act
        var hash = Password.ComputeSha256Hash(emptyPassword);

        // Assert
        Assert.NotNull(hash);
        Assert.NotEmpty(hash);
    }
}
