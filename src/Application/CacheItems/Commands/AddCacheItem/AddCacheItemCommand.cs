namespace DistributedCacheDesign.Application.CacheItems.Commands.AddCacheItem;

public record AddCacheItemCommand (
    string Key,
    string Value,
    int ExpirationInSeconds
) : IRequest<bool>;