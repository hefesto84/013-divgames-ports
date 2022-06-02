using steroid_port.Game.Configurations;

namespace steroid_port.Game.Services
{
    public class ConfigService
    {
        public SteroidConfig Config { get; private set; }
        
        public void Init(SteroidConfig steroidConfig)
        {
            Config = steroidConfig;
        }
    }
}