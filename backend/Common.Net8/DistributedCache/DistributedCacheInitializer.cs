using Common.Net8.AppSettings;
using Common.Net8.DistributedCache.Configuration;
using Common.Net8.DistributedCache.Redis;
using Common.Net8.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Common.Net8.DistributedCache;

public class DistributedCacheInitializer
{
    public static void Initialize(IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton<IConfiguration>(provider => configuration);
        services.AddSingleton<RedisConnection>();
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configuration.GetConnectionString("CacheConnection")));
        services.AddScoped(typeof(IRedisCacheService<>), typeof(RedisCacheService<>));
        services.Configure<CacheOptions>(configuration.GetSection("CacheOptions"));
    }

}
