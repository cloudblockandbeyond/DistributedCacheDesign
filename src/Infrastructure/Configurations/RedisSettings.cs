namespace DistributedCacheDesign.Infrastructure.Configurations;

public static class RedisSettings
{
    public const string SectionName = "Redis";

    public static ConfigurationOptions GetConfigurationOptions(ConfigurationManager configuration)
    {
        var options = new ConfigurationOptions();

        var _primarySection = configuration.GetSection($"{ SectionName }:Primary");
        if (_primarySection != null)
            options.EndPoints.Add(_primarySection.GetValue<string>("Endpoint"), _primarySection.GetValue<int>("Port"));

        var _secondarySection = configuration.GetSection($"{ SectionName }:Secondary");
        if (_secondarySection != null)
            options.EndPoints.Add(_secondarySection.GetValue<string>("Endpoint"), _secondarySection.GetValue<int>("Port"));

        options.AbortOnConnectFail = false;

        return options;
    }
}