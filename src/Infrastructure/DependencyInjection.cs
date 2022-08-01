namespace DistributedCacheDesign.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraStructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<MongoSettings>(configuration.GetSection(MongoSettings.SectionName));

        var options = RedisSettings.GetConfigurationOptions(configuration);

        services.AddSingleton<IConnectionMultiplexer>(
            ConnectionMultiplexer.Connect(options)
        );

        services.AddScoped<IDistributedCache, DistributedCache>();

        return services;
    }
}