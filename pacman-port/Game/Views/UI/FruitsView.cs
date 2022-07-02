using System;
using System.Numerics;
using common.Core.Services.Render;
using common.Core.Views.Base;
using pacman_port.Game.Services.Sprite;
using Raylib_cs;

namespace pacman_port.Game.Views.UI
{
    public class FruitsView : View
    {
        private SpriteService _spriteService;
        private Tuple<Rectangle,Texture2D> _textureData;
        private Rectangle _destination;
        private Vector2 _center = Vector2.Zero;
        
        public FruitsView(RenderService renderService, SpriteService spriteService) : base(renderService)
        {
            _spriteService = spriteService;
        }
    
        public void Init()
        {
            
        }
        
        public void Update()
        {
            //RenderService.Render(_textureData.Item2, _textureData.Item1, Bounds, _center, 0);
        }

        public void Reset()
        {
            
        }
    }
}