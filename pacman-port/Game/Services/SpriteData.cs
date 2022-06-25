using System;
using Newtonsoft.Json;

namespace pacman_port.Game.Services
{
    [Serializable]
    public class SpriteData
    {
        [JsonProperty("tileWidth")] 
        public int TileWidth;
        [JsonProperty("tileHeight")] 
        public int TileHeight;
        [JsonProperty("sprites")]
        public SpriteDataEntry[] Sprites;
    }
}