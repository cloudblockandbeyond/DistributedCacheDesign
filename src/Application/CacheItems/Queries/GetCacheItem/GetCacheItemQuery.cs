namespace DistributedCacheDesign.Application.CacheItems.Queries.GetCacheItem;

public record GetCacheItemQuery (
    string Key
) : IRequest<CacheItemDto>;