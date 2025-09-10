using Domain.Contracts;
using StackExchange.Redis;

namespace Persistence.Repos;

    
public class CacheRepo(IConnectionMultiplexer connection) : ICacheRepo
{
    private readonly IDatabase _database = connection.GetDatabase();
    public async Task<string?> GetAsync(string cacheKey)
    {
        var cacheValue = await _database.StringGetAsync(cacheKey);
        return cacheValue.IsNullOrEmpty?null:cacheValue.ToString();
    }

    public async Task SetAsync(string cacheKey, string cacheValue, TimeSpan expiry)
       => await _database.StringSetAsync(cacheKey, cacheValue, expiry);
}