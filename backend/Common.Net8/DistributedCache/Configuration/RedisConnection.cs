using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Common.Net8.DistributedCache.Configuration;

public class RedisConnection
{
    private static ConnectionMultiplexer _redisConnection;
    private static readonly object padlock = new object();

    public RedisConnection(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CacheConnection");
        lock (padlock)
        {
            if (_redisConnection == null || !_redisConnection.IsConnected)
            {
                _redisConnection = ConnectionMultiplexer.Connect(connectionString);
            }
        }
    }

    // Modifique este método para aceitar um índice de banco de dados opcional
    public IDatabase GetDatabase(int dbIndex = -1) // -1 indica que será usado o banco de dados padrão
    {
        return _redisConnection.GetDatabase(dbIndex);
    }

    // Novo método para obter a instância ConnectionMultiplexer
    public ConnectionMultiplexer GetConnectionMultiplexer()
    {
        return _redisConnection;
    }
}