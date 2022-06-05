using System;
using System.Numerics;
using Raylib_cs;
using steroid_port.Game.Services;
using steroid_port.Game.Views.Base;

namespace steroid_port.Game.Views
{
    public class AsteroidView : View
    {
        private Tuple<Rectangle,Texture2D> _textureData;
        private Rectangle _destination;
        private Vector2 _asteroidCenter;
        private Vector2 _currentPosition;
        
        public AsteroidView(RenderService renderService, ScreenService screenService) : base(renderService, screenService)
        {
            
        }
        
        public void Init(SpriteService spriteService, Vector2 position)
        {
            _currentPosition = position;
            _textureData = spriteService.Get("asteroid-0");
            _destination = new Rectangle(_currentPosition.X, _currentPosition.Y, _textureData.Item1.width, _textureData.Item1.height);
            _asteroidCenter = Vector2.Zero;

            Bounds = _destination;
        }
        
        public void UpdateView(int rotation)
        {
            _destination.x = _currentPosition.X;
            _destination.y = _currentPosition.Y;
            _asteroidCenter.X = _destination.width * 0.5f;
            _asteroidCenter.Y = _destination.height * 0.5f;

            Bounds = _destination;
            
            RenderService.Render(_textureData.Item2, _textureData.Item1, Bounds, _asteroidCenter, rotation);
        }
    }
}