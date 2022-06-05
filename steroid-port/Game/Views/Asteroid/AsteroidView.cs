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
        private ScreenService _screenService;
        private Tuple<Rectangle,Texture2D> _textureData;
        private Rectangle _destination;
        private Vector2 _asteroidCenter;
        private Vector2 _currentPosition;
        private Vector2 _randomVelocity;
        
        public int Level { get; set; }

        public AsteroidView(RenderService renderService, ScreenService screenService) : base(renderService)
        {
            _screenService = screenService;
        }
        
        public void Init(SpriteService spriteService, Vector2 position, Vector2 velocity, int level)
        {
            _currentPosition = position;
            _textureData = spriteService.Get($"asteroid-{level}");
            _destination = new Rectangle(_currentPosition.X, _currentPosition.Y, _textureData.Item1.width, _textureData.Item1.height);
            _asteroidCenter = Vector2.Zero;
            _randomVelocity = velocity;
            Level = level;
            Bounds = _destination;
        }
        
        public void UpdateView(int rotation)
        {
            _currentPosition += _randomVelocity;
            
            FixPosition();
            //_currentPosition.X += 2;
            //_currentPosition.Y += 2;
            
            _destination.x = _currentPosition.X;
            _destination.y = _currentPosition.Y;
            _asteroidCenter.X = _destination.width * 0.5f;
            _asteroidCenter.Y = _destination.height * 0.5f;

            Bounds = _destination;
            
            RenderService.Render(_textureData.Item2, _textureData.Item1, Bounds, _asteroidCenter, rotation);
        }
        
        private void FixPosition()
        {
            if (_currentPosition.Y > _screenService.CurrentSize.Y) _currentPosition.Y = 0;
            if (_currentPosition.Y < 0) _currentPosition.Y = _screenService.CurrentSize.Y;
            if (_currentPosition.X < 0) _currentPosition.X = _screenService.CurrentSize.X;
            if (_currentPosition.X > _screenService.CurrentSize.X) _currentPosition.X = 0;
        }
    }
}