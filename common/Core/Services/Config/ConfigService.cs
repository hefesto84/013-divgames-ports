namespace common.Core.Services.Config
{
    public class ConfigService<T> where T : Configurations.Base.Config
    {
        public T Config { get; }

        public ConfigService(T config)
        {
            Config = config;
        }
    }
}