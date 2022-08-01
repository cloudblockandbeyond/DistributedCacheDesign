namespace DistributedCacheDesign.Application.CacheItems.Commands.AddCacheItem;

public class AddCacheItemCommandHandler : IRequestHandler<AddCacheItemCommand, bool>
{
    private readonly IDistributedCache _distributedCache;
    private readonly IMapper _mapper;

    public AddCacheItemCommandHandler(IDistributedCache distributedCache, IMapper mapper)
    {
        _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<bool> Handle(AddCacheItemCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CacheItem>(request);
        return await _distributedCache.AddAsync(entity);
    }
}