using Common.Net8.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Common.Net8.Tests.Model;

public class DomainEventDistributedCacheIntegrationTests
{
    private readonly IMongoCollection<DomainEventDistributedCache> _collection;

    public DomainEventDistributedCacheIntegrationTests()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("TestDb");
        _collection = database.GetCollection<DomainEventDistributedCache>("EventsCache");
    }

    [Fact]
    public async Task SaveAndLoad_DomainEventDistributedCache_WorksCorrectly()
    {
        // Arrange
        var cache = new DomainEventDistributedCache
        {
            Events = new BsonDocument("event1", "data1")
        };

        // Act
        await _collection.InsertOneAsync(cache);
        var loadedCache = await _collection.Find(Builders<DomainEventDistributedCache>.Filter.Eq("_id", cache.Id)).FirstOrDefaultAsync();

        // Assert
        Assert.NotNull(loadedCache);
        Assert.Equal(cache.Id, loadedCache.Id);
        Assert.Equal("data1", loadedCache.Events["event1"].AsString);
    }
}
