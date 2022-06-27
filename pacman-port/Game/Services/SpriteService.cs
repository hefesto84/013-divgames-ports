using System;
using System.Collections.Generic;
using System.IO;
using common.Core.Services.Screen;
using Newtonsoft.Json;
using Raylib_cs;

namespace pacman_port.Game.Services
{
    public class SpriteService
    {
        // 3/8
        private Texture2D _mainTexture;
        private Texture2D _testTileTexture;
        private Dictionary<int, Rectangle> _spriteData;
        private Dictionary<int, Texture2D> _textureData;
        private readonly ScreenService _screenService;
        private Texture2D _testTileBlockedTexture;

        public SpriteService(ScreenService screenService)
        {
            _screenService = screenService;
        }
        
        public Tuple<Rectangle,Texture2D> Get(int id)
        {
            var isSpriteRegistered = _spriteData.TryGetValue(id, out var rect);
            if (!isSpriteRegistered) throw new Exception($"Sprite with id: {id} not registered.");
            return new Tuple<Rectangle, Texture2D>(rect,_textureData[id]);
        }

        public void Init()
        {
            _mainTexture = Raylib.LoadTexture(Directory.GetCurrentDirectory()+"/Resources/spritemap-384.png");
            _testTileTexture = Raylib.LoadTexture(Directory.GetCurrentDirectory() + "/Resources/test-tile.png");
            _testTileBlockedTexture = Raylib.LoadTexture(Directory.GetCurrentDirectory() + "/Resources/test-tile-blocked.png");
            LoadSpriteData();
        }

        private void LoadSpriteData()
        {
            var data= File.ReadAllText(Directory.GetCurrentDirectory() + "/Resources/spritemap-384.json");
            var contents = JsonConvert.DeserializeObject<SpriteData>(data);
            
            _spriteData = new Dictionary<int, Rectangle>();
            _textureData = new Dictionary<int, Texture2D>();

            foreach (var entry in contents.Sprites)
            {
                _spriteData.Add(entry.Id, new Rectangle(entry.X, entry.Y, entry.Width, entry.Height));
                _textureData.Add(entry.Id, _mainTexture);
            }
            
            
            //_spriteData.Add("test-tile",new Rectangle(0,0,24,24));
            //_textureData.Add("test-tile",_testTileTexture);
            
            _spriteData.Add(1,new Rectangle(0,0,24,24));
            _textureData.Add(1,_testTileBlockedTexture);
            
            Console.WriteLine($"Assets loaded: {_spriteData.Count}");
        }
    }
}