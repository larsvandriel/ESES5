namespace auth_service.Helpers
{
    public static class ConfigurationLoader
    {
        public static void LoadConfigurationValue(IConfiguration config, string configKey)
        {
            if (configKey == null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            string? configValue = Environment.GetEnvironmentVariable(configKey);
            if (configValue != null)
            {
                config[configKey] = configValue;
            }
        }
    }
}
