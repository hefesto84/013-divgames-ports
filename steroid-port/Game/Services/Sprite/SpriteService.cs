using System;
using System.Collections.Generic;
using System.IO;
using common.Core.Services.Screen;
using Raylib_cs;

namespace steroid_port.Game.Services.Sprite
{
    public class SpriteService
    {
        private Texture2D _mainTexture;
        private Texture2D _backgroundTexture;
        private Dictionary<string, Rectangle> _spriteData;
        private Dictionary<string, Texture2D> _textureData;
        private readonly ScreenService _screenService;

        public SpriteService(ScreenService screenService)
        {
            _screenService = screenService;
        }
        
        public Tuple<Rectangle,Texture2D> Get(string id)
        {
            var isSpriteRegistered = _spriteData.TryGetValue(id, out var rect);
            if (!isSpriteRegistered) throw new Exception($"Sprite with id: {id} not registered.");
            return new Tuple<Rectangle, Texture2D>(rect,_textureData[id]);
        }
        
        public void Init()
        {
            _mainTexture = Raylib.LoadTexture(Directory.GetCurrentDirectory()+"/Resources/steroid.png");
            
            _spriteData = new Dictionary<string, Rectangle>();
            _spriteData.Add("ship",new Rectangle(0,0,33,21));
            _spriteData.Add("asteroid-3", new Rectangle(53,0,50,50));
            _spriteData.Add("asteroid-2", new Rectangle(104,0,40,40));
            _spriteData.Add("asteroid-1", new Rectangle(146,0,24,24));
            _spriteData.Add("shot",new Rectangle(35,0,15,7));

            _textureData = new Dictionary<string, Texture2D>();
            _textureData.Add("ship", _mainTexture);
            _textureData.Add("asteroid-3",_mainTexture);
            _textureData.Add("asteroid-2",_mainTexture);
            _textureData.Add("asteroid-1",_mainTexture);
            _textureData.Add("shot",_mainTexture);
            
            CreateBackgroundTexture();
        }

        private void CreateBackgroundTexture()
        {
            var width = (int)_screenService.CurrentSize.X;
            var height = (int)_screenService.CurrentSize.Y;
            
            var background = Raylib.GenImageColor(width, height, Color.BLACK);

            var r = new Random();
            
            for (var i = 0; i < 499; i++)
            {
                Raylib.ImageDrawPixel(ref background, r.Next(0,width-1), r.Next(0,height-1), Color.DARKGRAY);    
            }
            
            _backgroundTexture = Raylib.LoadTextureFromImage(background);
            
            _spriteData.Add("background", new Rectangle(0,0,width, height));
            _textureData.Add("background", _backgroundTexture);
        }
    }
}