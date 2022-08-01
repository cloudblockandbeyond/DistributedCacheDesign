namespace DistributedCacheDesign.Infrastructure.Configurations;

public class MongoSettings
{
    public const string SectionName = "Mongo";
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CollectionName { get; set; } = null!;
}