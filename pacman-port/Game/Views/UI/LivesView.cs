using System;
using System.Numerics;
using common.Core.Services.Render;
using common.Core.Views.Base;
using pacman_port.Game.Services.Sprite;
using pacman_port.Game.Systems.UI;
using Raylib_cs;

namespace pacman_port.Game.Views.UI
{
    public class LivesView : View
    {
        private SpriteService _spriteService;
        private Tuple<Rectangle,Texture2D> _textureData;
        private Rectangle _destination;
        private Vector2 _center = Vector2.Zero;
        private readonly UISystem _uiSystem;
        
        public LivesView(RenderService renderService, SpriteService spriteService, UISystem uiSystem) : base(renderService)
        {
            _spriteService = spriteService;
            _uiSystem = uiSystem;
        }

        public void Init(Vector2 position)
        {
            _textureData = _spriteService.Get(102);
            
            _destination = new Rectangle(position.X*24, position.Y*24, _textureData.Item1.width, _textureData.Item1.height);
            
            Bounds = _destination;
        }
        
        public void Update()
        {
            var boundIncr = new Rectangle(0, 0, _textureData.Item2.width, _textureData.Item2.height);

            boundIncr = Bounds;
            
            for (var i = 0; i < _uiSystem.CurrentLives; i++)
            {
                boundIncr.x = 36 * (i+0.5f);
                RenderService.Render(_textureData.Item2, _textureData.Item1, boundIncr, _center, 0);    
            }
            
        }

        public void Reset() { }
    }
}