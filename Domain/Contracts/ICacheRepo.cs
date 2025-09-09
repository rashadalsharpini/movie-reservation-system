namespace Domain.Contracts;

public interface ICacheRepo
{
    Task<string?> GetAsync(string cacheKey);
    Task SetAsync(string cacheKey, string cacheValue, TimeSpan expiry);
}