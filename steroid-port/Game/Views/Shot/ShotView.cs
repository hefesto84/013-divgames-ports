using System;
using System.Numerics;
using Raylib_cs;
using steroid_port.Game.Services.Render;
using steroid_port.Game.Services.Screen;
using steroid_port.Game.Services.Sprite;
using steroid_port.Game.Views.Base;

namespace steroid_port.Game.Views.Shot
{
    public class ShotView : View
    {
        private readonly ScreenService _screenService;
        private Tuple<Rectangle,Texture2D> _textureData;
        private Rectangle _destination;
        private Vector2 _shotCenter;
        private Vector2 _currentPosition;
        private Vector2 _velocity = Vector2.Zero;
        private int _rotation;
        private const float Pi180 = MathF.PI / 180;
        private float _rads;
        private const int ShotSpeed = 8;
        private const int InitialShotSpeed = 10;

        public bool IsReady { get; set; }
        
        public ShotView(RenderService renderService, ScreenService screenService) : base(renderService)
        {
            _screenService = screenService;
        }
        
        public void Init(SpriteService spriteService)
        {
            _textureData = spriteService.Get("shot");
            _destination = new Rectangle(0, 0, _textureData.Item1.width, _textureData.Item1.height);
            _shotCenter = Vector2.Zero;

            Bounds = _destination;
        }

        public void SetView(Vector2 position, Vector2 velocity, int rotation)
        {
            IsReady = false;
            
            _rotation = rotation;

            _rads = rotation * Pi180;
            
            _currentPosition.X = position.X + InitialShotSpeed * MathF.Cos(_rads);
            _currentPosition.Y = position.Y + InitialShotSpeed * MathF.Sin(_rads);
            _velocity.X = ShotSpeed * MathF.Cos(_rads);
            _velocity.Y = ShotSpeed * MathF.Sin(_rads);
        }
        public void UpdateView()
        {
            _currentPosition += _velocity;
            
            _destination.x = _currentPosition.X;
            _destination.y = _currentPosition.Y;
            _shotCenter.X = _destination.width * 0.5f;
            _shotCenter.Y = _destination.height * 0.5f;

            Bounds = _destination;
            
            RenderService.Render(_textureData.Item2, _textureData.Item1, Bounds, _shotCenter, _rotation);
            
            CheckIfItsOutOfScreen();
        }

        public void Recycle()
        {
            IsReady = true;
        }
        
        private void CheckIfItsOutOfScreen()
        {
            if (_currentPosition.X > _screenService.CurrentSize.X ||
                _currentPosition.X < 0 ||
                _currentPosition.Y > _screenService.CurrentSize.Y ||
                _currentPosition.Y < 0)
            {
                IsReady = true;
            }
        }
    }
}