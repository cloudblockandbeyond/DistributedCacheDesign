namespace DistributedCacheDesign.Domain.Entities;

/// <summary>
/// Represents a cache item.
/// </summary>
public class CacheItem
{
    /// <summary>
    /// Gets or sets the key of the cache item.
    /// </summary>
    public string Key { get; set; } = null!;

    /// <summary>
    /// Gets or sets the partition key of the cache item.
    /// </summary>
    public string PartitionKey { get; set; } = null!;

    /// <summary>
    /// Gets or sets the value of the cache item.
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// Gets or sets the expiration in seconds of the cache item.
    /// </summary>
    public int ExpirationInSeconds { get; set; }
}