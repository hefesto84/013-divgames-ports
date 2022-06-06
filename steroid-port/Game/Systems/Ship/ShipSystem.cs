using System;
using System.Numerics;
using common.Core.Services.Render;
using common.Core.Services.Screen;
using Raylib_cs;
using steroid_port.Game.Configurations;
using steroid_port.Game.Services;
using steroid_port.Game.Services.Sprite;
using steroid_port.Game.Systems.Collision;
using steroid_port.Game.Views;
using steroid_port.Game.Views.Ship;

namespace steroid_port.Game.Systems.Ship
{
    public class ShipSystem : common.Core.Systems.Base.System
    {
        private readonly ScreenService _screenService;
        private readonly RenderService _renderService;
        private readonly SpriteService _spriteService;
        
        private ShipView _view;
        
        private int _rotation;
        private Vector2 _velocity = Vector2.Zero;
        private Vector2 _thrust = Vector2.Zero;
        private Vector2 _currentPosition = Vector2.Zero;

        public Vector2 CurrentPosition => _currentPosition;
        public int CurrentRotation => _rotation;
        public Vector2 CurrentVelocity => _velocity;

        public ShipView Ship => _view;
        
        public ShipSystem(ScreenService screenService, SpriteService  spriteService, RenderService renderService)
        {
            _screenService = screenService;
            _renderService = renderService;
            _spriteService = spriteService;
        }

        public override void Init()
        {
            SetupShipView();
            Reset();
        }

        public override void Reset()
        {
            _currentPosition = _screenService.CurrentScreenCenter;
            _rotation = 0;
            _thrust = Vector2.Zero;
            _velocity = Vector2.Zero;
        }

        public override void Update()
        {
            ProcessMovement();
            FixPosition();
            
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                Reset();
            }
            
            _view.UpdateView(_currentPosition, _rotation);
        }

        private void SetupShipView()
        {
            if (_view != null) return;
            
            _view = new ShipView(_renderService);
            _view.Init(_spriteService);
        }

        private void ProcessMovement()
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)) _rotation += 5;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) _rotation -= 5;
            if(Raylib.IsKeyDown(KeyboardKey.KEY_UP)) Boost();

            _currentPosition.X += _velocity.X;
            _currentPosition.Y += _velocity.Y;
        }

        private void Boost()
        {
            var rads = _rotation * MathF.PI / 180;
            _thrust.X = MathF.Cos(rads) * 0.1f;
            _thrust.Y = MathF.Sin(rads) * 0.1f;
            _velocity.X += _thrust.X;
            _velocity.Y += _thrust.Y;
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