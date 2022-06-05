using System;
using System.Numerics;
using Raylib_cs;
using steroid_port.Game.Services.Render;
using steroid_port.Game.Services.Screen;
using steroid_port.Game.Services.Sprite;
using steroid_port.Game.Views.Base;

namespace steroid_port.Game.Views.Asteroid
{
    public class AsteroidView : View
    {
        private Tuple<Rectangle,Texture2D> _textureData;
        private Rectangle _destination;
        private Vector2 _asteroidCenter;
        private Vector2 _currentPosition;
        
        public int Level { get; set; }
        
        public AsteroidView(RenderService renderService) : base(renderService) { }
        
        public void Init(SpriteService spriteService, Vector2 position, int level)
        {
            _currentPosition = position;
            _textureData = spriteService.Get($"asteroid-{level}");
            _destination = new Rectangle(_currentPosition.X, _currentPosition.Y, _textureData.Item1.width, _textureData.Item1.height);
            _asteroidCenter = Vector2.Zero;

            Level = level;
            Bounds = _destination;
        }
        
        public void UpdateView(int rotation)
        {
            //_currentPosition.X += 2;
            //_currentPosition.Y += 2;
            
            _destination.x = _currentPosition.X;
            _destination.y = _currentPosition.Y;
            _asteroidCenter.X = _destination.width * 0.5f;
            _asteroidCenter.Y = _destination.height * 0.5f;

            Bounds = _destination;
            
            RenderService.Render(_textureData.Item2, _textureData.Item1, Bounds, _asteroidCenter, rotation);
        }
    }
}