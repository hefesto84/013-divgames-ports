using System;
using System.Numerics;
using common.Core.Services.Render;
using common.Core.Views.Base;
using pacman_port.Game.Services;
using Raylib_cs;

namespace pacman_port.Game.Views.Player
{
    public class PlayerView : View
    {
        private readonly SpriteService _spriteService;
        private Tuple<Rectangle,Texture2D> _textureData;
        private Rectangle _destination;
        private readonly Vector2 _center = Vector2.Zero;
        private readonly Vector2 _initialTilePosition = new(1, 1);

        public PlayerView(RenderService renderService, SpriteService spriteService) : base(renderService)
        {
            _spriteService = spriteService;
        }
        
        public void Init()
        {
            _textureData = _spriteService.Get("pacman");
            
            _destination = new Rectangle(
                _initialTilePosition.X%_textureData.Item2.width, 
                _initialTilePosition.Y%_textureData.Item2.height, 
                _textureData.Item1.width, 
                _textureData.Item1.height
            );
            
            Bounds = _destination;
        }

        public void UpdateView(Vector2 currentPosition)
        {
            _destination.x = currentPosition.X;
            _destination.y = currentPosition.Y;
            
            Bounds = _destination;
            
            RenderService.Render(_textureData.Item2, _textureData.Item1, Bounds, _center, 0);
        }
    }
}