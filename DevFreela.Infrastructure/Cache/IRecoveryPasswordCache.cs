namespace DevFreela.Infrastructure.Cache;

public interface IRecoveryPasswordCache
{
    void SetCode(string cacheKey, int code);
    string? GetCode(string cacheKey);
    void RemoveCode(string cacheKey);
}