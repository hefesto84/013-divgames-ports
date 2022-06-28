using System;
using Newtonsoft.Json;

namespace pacman_port.Game.Services.Sprite
{
    [Serializable]
    public class SpriteDataEntry
    {
        [JsonProperty("id")]
        public int Id;
        [JsonProperty("name")] 
        public string Name;
        [JsonProperty("iX")]
        public int X;
        [JsonProperty("iY")]
        public int Y;
        [JsonProperty("iW")]
        public int Width;
        [JsonProperty("iH")]
        public int Height;
    }
}