using steroid_port.Game.Configurations.Steroid;

namespace steroid_port.Game.Services.Config
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