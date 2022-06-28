using System;
using System.Collections.Generic;
using System.Numerics;
using common.Core.Services.Render;
using common.Core.Views.Base;
using pacman_port.Game.Enums;
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
        private int _currentRotation = 0;

        private readonly Dictionary<MovementDirection, Tuple<Rectangle, Texture2D>> _texturesData;
        
        public PlayerView(RenderService renderService, SpriteService spriteService) : base(renderService)
        {
            _spriteService = spriteService;
            
            _texturesData = new Dictionary<MovementDirection, Tuple<Rectangle, Texture2D>>();
            
            _texturesData.Add(MovementDirection.Right,_spriteService.Get(100));
            _texturesData.Add(MovementDirection.Left,_spriteService.Get(102));
            _texturesData.Add(MovementDirection.Up,_spriteService.Get(104));
            _texturesData.Add(MovementDirection.Down,_spriteService.Get(106));
            _texturesData.Add(MovementDirection.None,_spriteService.Get(108));
            
        }
        
        public void Init()
        {
            _textureData = _texturesData[MovementDirection.None];
            
            _destination = new Rectangle(
                _initialTilePosition.X%_textureData.Item2.width, 
                _initialTilePosition.Y%_textureData.Item2.height, 
                _textureData.Item1.width, 
                _textureData.Item1.height
            );
            
            Bounds = _destination;
        }

        public void UpdateView(Vector2 currentPosition, MovementDirection currentMovementDirection)
        {
            _textureData = _texturesData[currentMovementDirection];
            
            _destination.x = currentPosition.X;
            _destination.y = currentPosition.Y;
            
            Bounds = _destination;
            
            RenderService.Render(_textureData.Item2, _textureData.Item1, Bounds, _center, _currentRotation);
        }
    }
}