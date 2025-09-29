using System.Text.Json;
using Domain.Contracts;
using ServiceAbstraction;

namespace Service;

public class CacheService(ICacheRepo cacheRepo):ICacheService
{
    public async Task<string?> GetAsync(string key)
    {
        return await cacheRepo.GetAsync(key);
    }

    public async Task SetAsync(string key, object value, TimeSpan timeToLive)
    {
        var val = JsonSerializer.Serialize(value);
        await cacheRepo.SetAsync(key, val, timeToLive);
    }
}