using System;
using Raylib_cs;
using steroid_port.Game.Services;

namespace steroid_port.Game.Systems.Background
{
    public class BackgroundSystem : Base.System
    {
        private readonly SpriteService _spriteService;
        private readonly RenderService _renderService;
        private Tuple<Rectangle, Texture2D> _textureData;
        
        public BackgroundSystem(SpriteService spriteService, RenderService renderService)
        {
            _spriteService = spriteService;
            _renderService = renderService;
        }
        
        public override void Init()
        {
           _textureData = _spriteService.Get("background");
        }

        public override void Update()
        {
            _renderService.Render(_textureData.Item2, new Rectangle(0,0,_textureData.Item2.width, _textureData.Item2.height));
        }
    }
}