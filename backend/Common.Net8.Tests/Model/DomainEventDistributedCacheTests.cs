using Common.Net8.Model;

namespace Common.Net8.Tests.Model;

public class DomainEventDistributedCacheTests
{
    [Fact]
    public void Constructor_InitializesIdWithUniqueValue()
    {
        // Arrange & Act
        var cache1 = new DomainEventDistributedCache();
        var cache2 = new DomainEventDistributedCache();

        // Assert
        Assert.NotEqual(cache1.Id, cache2.Id);
    }

}
