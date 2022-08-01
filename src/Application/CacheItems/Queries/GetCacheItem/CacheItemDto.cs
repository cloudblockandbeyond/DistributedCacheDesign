namespace DistributedCacheDesign.Application.CacheItems.Queries.GetCacheItem;

public class CacheItemDto
{
    public string Key { get; set; } = null!;
    public string? Value { get; set; }
}