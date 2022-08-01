namespace DistributedCacheDesign.Infrastructure.Persistence;

/// <summary>
/// Represents a Redis distributed cache.
/// </summary>
public static class RedisCache
{
    /// <summary>
    /// Adds a cache item to the cache.
    /// </summary>
    /// <param name="IDatabase">The Redis database.</param>
    /// <param name="cacheItem">The cache item to be added.</param>
    /// <returns>True or false indicating whether the cache item is added or not.</returns>
    public static async Task<bool> AddAsync(IDatabase db, CacheItem cacheItem)
    {
        var redisKey = $"{ cacheItem.PartitionKey }:{ cacheItem.Key }";

        if (db.IsConnected(redisKey))
        {
            var serializedValue = JsonConvert.SerializeObject(cacheItem);
            var expireAt = TimeSpan.FromSeconds(Convert.ToDouble(cacheItem.ExpirationInSeconds));
            return await db.StringSetAsync(redisKey, serializedValue, expireAt);
        }

        return false;
    }

    /// <summary>
    /// Gets a cache item from the cache.
    /// </summary>
    /// <param name="IDatabase">The Redis database.</param>
    /// <param name="key">The key of the cache item.</param>
    /// <param name="partitionKey">The partition key of the cache item.</param>
    /// <returns>The cache item.</returns>
    public static async Task<CacheItem?> GetAsync(IDatabase db, string key, string partitionKey)
    {
        var redisKey = $"{ partitionKey }:{ key }";

        if (db.IsConnected(redisKey))
        {
            var serializedValue = await db.StringGetAsync(redisKey);

            return serializedValue.IsNullOrEmpty
                ? null
                : JsonConvert.DeserializeObject<CacheItem>(serializedValue.ToString());
        }

        return null;
    }
}