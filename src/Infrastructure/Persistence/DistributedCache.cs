namespace DistributedCacheDesign.Infrastructure.Persistence;

/// <summary>
/// Represents a distributed cache using Cache Aside pattern.
/// </summary>
public class DistributedCache : IDistributedCache
{
    private readonly IConfiguration _configuration;
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly IMongoCollection<BsonDocument> _collection;

    /// <summary>
    /// Initializes a new instance of the <see cref="DistributedCache"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="connectionMultiplexer">Redis connection multiplexer.</param>
    /// <param name="options">MongoDB options.</param>
    /// <exception cref="ArgumentNullException">
    public DistributedCache(IConfiguration configuration, IConnectionMultiplexer connectionMultiplexer, IOptions<MongoSettings> options)
    {
        // Initialize redis connection multiplexer
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _connectionMultiplexer = connectionMultiplexer ?? throw new ArgumentNullException(nameof(connectionMultiplexer));

        // Initialize mongo db collection
        var mongoSettings = options ?? throw new ArgumentNullException(nameof(options));
        var client = new MongoClient(mongoSettings.Value.ConnectionString);
        var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
        _collection = database.GetCollection<BsonDocument>(mongoSettings.Value.CollectionName);
    }

    public async Task<bool> AddAsync(CacheItem cacheItem)
    {
        var itemAdded = false;

        cacheItem.PartitionKey =  _configuration.GetSection("Redis:PartitionKey").Value;

        // Indicates whether any Redis servers are connected.
        if (_connectionMultiplexer.IsConnected)
            itemAdded = await RedisCache.AddAsync(_connectionMultiplexer.GetDatabase(), cacheItem);

        // If Redis is not connected, then add the cache item to MongoDB.
        if (!itemAdded)
            itemAdded = await MongoCache.AddAsync(_collection, cacheItem);

        return itemAdded;
    }

    public async Task<CacheItem?> GetAsync(string key)
    {
        var cacheItem = new CacheItem();

        var partitionKey =  _configuration.GetSection("Redis:PartitionKey").Value;

        // Indicates whether any Redis servers are connected.
        if (_connectionMultiplexer.IsConnected)
            cacheItem = await RedisCache.GetAsync(_connectionMultiplexer.GetDatabase(), key, partitionKey);

        // If Redis is not connected, then get the cache item from MongoDB.
        if (cacheItem == null || string.IsNullOrWhiteSpace(cacheItem.Value))
            cacheItem = await MongoCache.GetAsync(_collection, key);

        return cacheItem;
    }
}