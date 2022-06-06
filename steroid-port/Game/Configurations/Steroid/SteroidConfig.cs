using System.Collections.Generic;
using common.Core.Configurations.Base;

namespace steroid_port.Game.Configurations.Steroid
{
    // @TODO: we need to refactor this for the texts :)
    public class SteroidConfig : Config
    {
        public SteroidConfig(string propertiesFile) : base(propertiesFile)
        {
            BuildTexts();
        }

        protected sealed override void BuildTexts()
        {
            Texts = new Dictionary<string, string>();
            
            Texts.Add("GAME_TITLE_1_KEY",Properties[4]);
            Texts.Add("GAME_TITLE_2_KEY", Properties[5]);
            Texts.Add("GAME_PRESS_PLAY_KEY", Properties[6]);
            Texts.Add("HELP_KEY",Properties[7]);
        }
    }
}