using System.Text.Json;
using LanguageExt.Common;
using Microsoft.Extensions.Caching.Distributed;

namespace PlantCare.API.DataAccess.Cache;

internal static class CacheExtensionMethods
{
    public static async Task SetRecordAsync<T>(this IDistributedCache cache, string recordId, T data)
    {
        var options = new DistributedCacheEntryOptions();

        options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);

        var jsonData = JsonSerializer.Serialize(data);
        await cache.SetStringAsync(recordId, jsonData, options);
    }

    public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
    {
        var jsonData = await cache.GetStringAsync(recordId);

        if (jsonData is null)
        {
            return default(T);
        }

        return JsonSerializer.Deserialize<T>(jsonData);
    }

    public static async Task<Result<T>> ProcessCacheResult<T>(this Result<T> result, IDistributedCache cache,  string recordId)
    {
        
        return await result.Match<Task<T>>(
            async succ =>
        {
            await cache.SetRecordAsync<T>(recordId, succ);
            return succ;
        }, err =>
            {
                throw new InvalidOperationException(err.Message);
            });
    }
}