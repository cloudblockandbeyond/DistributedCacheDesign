namespace DistributedCacheDesign.Application.Common.Interfaces;

/// <summary>
/// Represents a distributed cache.
/// </summary>
public interface IDistributedCache
{
    /// <summary>
    /// Adds a cache item to the cache.
    /// </summary>
    /// <param name="cacheItem">The cache item to be added.</param>
    /// <returns>True or false indicating whether the cache item is added or not.</returns>
    Task<bool> AddAsync(CacheItem cacheItem);

    /// <summary>
    /// Gets a cache item from the cache.
    /// </summary>
    /// <param name="key">The key of the cache item.</param>
    /// <returns>The cache item.</returns>
    Task<CacheItem?> GetAsync(string key);
}