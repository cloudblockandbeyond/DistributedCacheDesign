namespace DistributedCacheDesign.Application.CacheItems.Queries.GetCacheItem;

public class GetCacheItemQueryHandler : IRequestHandler<GetCacheItemQuery, CacheItemDto>
{
    private readonly IDistributedCache _distributedCache;
    private readonly IMapper _mapper;

    public GetCacheItemQueryHandler(IDistributedCache DistributedCache, IMapper mapper)
    {
        _distributedCache = DistributedCache ?? throw new ArgumentNullException(nameof(DistributedCache));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CacheItemDto> Handle(GetCacheItemQuery request, CancellationToken cancellationToken)
    {
        var entity = await _distributedCache.GetAsync(request.Key);
        return _mapper.Map<CacheItemDto>(entity);
    }
}