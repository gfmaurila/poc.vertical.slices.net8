using API.Admin.Feature.Users.GetUser;
using Common.Net8.Interface;

namespace API.Admin.Tests.Integration.Utilities.Redis;

public class UserTestRedisService
{
    private readonly IRedisCacheService<List<UserQueryModel>> _cacheService;
    public UserTestRedisService(IRedisCacheService<List<UserQueryModel>> cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task Delete(string key)
    {
        await _cacheService.DeleteAsync(key);
    }

    public async Task ClearDatabaseAsync()
    {
        await _cacheService.ClearDatabaseAsync();
    }
}
