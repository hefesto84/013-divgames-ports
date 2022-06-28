using System;
using System.Numerics;
using common.Core.Services.Render;
using common.Core.Views.Base;
using pacman_port.Game.Services;
using pacman_port.Game.Services.Sprite;
using Raylib_cs;

namespace pacman_port.Game.Views.Fruit
{
    public class FruitView : View
    {
        private SpriteService _spriteService;
        private Tuple<Rectangle,Texture2D> _textureData;
        private Rectangle _destination;
        private Vector2 _center = Vector2.Zero;
        private int _lastIndex = 0;

        public FruitView(RenderService renderService) : base(renderService)
        {
            
        }
        
        public void Init(SpriteService spriteService, int index)
        {
            _lastIndex = index;
            _spriteService = spriteService;
            SetFruitId(index);
        }

        public void UpdateView(Vector2 position, int rotation)
        {
            _destination.x = position.X;
            _destination.y = position.Y;
            //_center.X = _destination.width * 0.5f;
            //_center.Y = _destination.height * 0.5f;
            
            Bounds = _destination;
            
            RenderService.Render(_textureData.Item2, _textureData.Item1, Bounds, _center, rotation);
        }
        
        public void SetFruitId(int fruitId)
        {
            _lastIndex = fruitId;
            //_textureData = _spriteService.Get($"f3");
            _destination = new Rectangle(0, 0, _textureData.Item1.width, _textureData.Item1.height);
            _center = Vector2.Zero;
            
            Bounds = _destination;
        }
    }
}