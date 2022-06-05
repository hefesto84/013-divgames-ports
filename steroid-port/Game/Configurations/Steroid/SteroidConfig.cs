using System;
using System.Collections.Generic;
using System.IO;

namespace steroid_port.Game.Configurations.Steroid
{
    public class SteroidConfig
    {
        public string Name { get; }
        public int Width { get; }
        public int Height { get; }
        public int Fps { get; }

        public Dictionary<string, string> Texts { get; }

        public SteroidConfig()
        {
            var properties = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "properties.ini"));
            Name = properties[0];
            Width = Int32.Parse(properties[1]);
            Height = Int32.Parse(properties[2]);
            Fps = Int32.Parse(properties[3]);

            Texts = new Dictionary<string, string>();
            
            Texts.Add("GAME_TITLE_1_KEY",properties[4]);
            Texts.Add("GAME_TITLE_2_KEY", properties[5]);
            Texts.Add("GAME_PRESS_PLAY_KEY", properties[6]);
            Texts.Add("HELP_KEY",properties[7]);
        }
    }
}