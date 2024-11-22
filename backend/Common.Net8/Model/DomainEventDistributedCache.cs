using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Net8.Model;

public class DomainEventDistributedCache
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public BsonDocument Events { get; set; }
}
