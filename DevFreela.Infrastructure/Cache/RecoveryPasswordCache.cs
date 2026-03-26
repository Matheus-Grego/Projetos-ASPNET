
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Cache;

public class RecoveryPasswordCache : IRecoveryPasswordCache
{
    private readonly IMemoryCache _cache;

    public RecoveryPasswordCache(IMemoryCache memoryCache)
    {
        _cache = memoryCache;
    }
    
    public void SetCode(string cacheKey, int code)
    {
        _cache.Set(cacheKey, code, TimeSpan.FromMinutes(5));
    }

    public string? GetCode(string cacheKey)
    {
        return _cache.TryGetValue(cacheKey, out string? code) ? code : null;
    }

    public void RemoveCode(string cacheKey)
    {
         _cache.Remove(cacheKey);
    }
}