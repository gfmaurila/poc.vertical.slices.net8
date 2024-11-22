using MongoDB.Driver;

namespace Common.Net8.DistributedCache.Configuration;

public class MongoDatabaseFactory
{
    private readonly IMongoClient _client;
    private readonly string _databaseName;

    public MongoDatabaseFactory(IMongoClient client, string databaseName)
    {
        _client = client;
        _databaseName = databaseName;
    }

    public IMongoDatabase GetDatabase() => _client.GetDatabase(_databaseName);
}
