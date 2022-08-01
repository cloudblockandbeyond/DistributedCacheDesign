namespace DistributedCacheDesign.Infrastructure.Persistence;

/// <summary>
/// Represents a Mongo distributed cache.
/// </summary>
public static class MongoCache
{
    /// <summary>
    /// Adds a cache item to the cache.
    /// </summary>
    /// <param name="collection">The MongoDB collection.</param>
    /// <param name="cacheItem">The cache item to be added.</param>
    /// <returns>True or false indicating whether the cache item is added or not.</returns>
    public static async Task<bool> AddAsync(IMongoCollection<BsonDocument> collection, CacheItem cacheItem)
    {
        /* TTL index should be created beforehand.
            create ttl index on a collection: db.FPContext.createIndex( { "expireAt": 1 }, { expireAfterSeconds: 0 } );
            get all indexes on a collection: db.collection.getIndexes()
        */

        var expireAt = new BsonDateTime(DateTime.UtcNow.AddSeconds(Convert.ToDouble(cacheItem.ExpirationInSeconds)));
        var serializedValue = JsonConvert.SerializeObject(cacheItem);

        var document = new BsonDocument
        {
            { "key", cacheItem.Key },
            { "value", serializedValue},
            { "expireAt", expireAt}
        };

        await collection.InsertOneAsync(document);
        return true;
    }

    /// <summary>
    /// Gets a cache item from the cache.
    /// </summary>
    /// <param name="collection">The MongoDB collection.</param>
    /// <param name="key">The key of the cache item.</param>
    /// <returns>The value stored in the cache item.</returns>
    public static async Task<CacheItem?> GetAsync(IMongoCollection<BsonDocument> collection, string key)
    {
        var filter = new BsonDocument("key", key);
        var document = await collection.Find(filter).FirstOrDefaultAsync();

        if (document == null)
            return null;

        var serializedValue = document.GetValue("value")?.AsString;

        return (serializedValue == null)
            ? null
            : JsonConvert.DeserializeObject<CacheItem>(serializedValue);
    }
}