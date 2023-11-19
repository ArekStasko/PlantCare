using System.Text.Json;
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
}