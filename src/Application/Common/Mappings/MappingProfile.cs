namespace DistributedCacheDesign.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddCacheItemCommand, CacheItem>();
        CreateMap<CacheItem, CacheItemDto>();
    }
}