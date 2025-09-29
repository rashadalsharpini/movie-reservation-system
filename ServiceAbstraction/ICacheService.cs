namespace ServiceAbstraction;

public interface ICacheService
{
    Task<string?> GetAsync(string key);
    Task SetAsync(string key, object value, TimeSpan timeToLive);
}