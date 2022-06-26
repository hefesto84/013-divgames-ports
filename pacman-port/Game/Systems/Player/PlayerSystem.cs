using System;
using System.Numerics;
using common.Core.Services.Render;
using common.Core.Services.Screen;
using pacman_port.Game.Enums;
using pacman_port.Game.Services;
using pacman_port.Game.Systems.Map;
using pacman_port.Game.Views.Player;
using Raylib_cs;

namespace pacman_port.Game.Systems.Player
{
    public class PlayerSystem : PacmanSystem
    {
        private MapSystem _mapSystem;
        private PlayerView _view;
        private Vector2 _currentPosition;
        private const int PlayerSpeed = 2;
        private MovementDirection _requestedMovementDirection = MovementDirection.None;
        private MovementDirection _currentMovementDirection = MovementDirection.None;

        public PlayerSystem(ScreenService screenService, RenderService renderService, SpriteService spriteService) :
            base(screenService, renderService, spriteService)
        {
        }

        public void Init(MapSystem mapSystem)
        {
            _mapSystem = mapSystem;

            SetupPlayerView();
            Reset();
        }

        private void SetupPlayerView()
        {
            if (_view != null) return;

            _view = new PlayerView(RenderService, SpriteService);
            _currentPosition = new Vector2(24 * 1, 24 * 1);
            _view.Init(_currentPosition);
        }

        public override void Reset()
        {
            _currentPosition = new Vector2(24 * 1, 24 * 1);
        }

        public override void Update()
        {
            Move();

            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
            {
                if (_currentMovementDirection == MovementDirection.Down)
                {
                    _currentMovementDirection = MovementDirection.Up;
                }

                _requestedMovementDirection = MovementDirection.Up;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
            {
                if (_currentMovementDirection == MovementDirection.Up)
                {
                    _currentMovementDirection = MovementDirection.Down;
                }

                _requestedMovementDirection = MovementDirection.Down;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                if (_currentMovementDirection == MovementDirection.Right)
                {
                    _currentMovementDirection = MovementDirection.Left;
                }

                _requestedMovementDirection = MovementDirection.Left;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                if (_currentMovementDirection == MovementDirection.Left)
                {
                    _currentMovementDirection = MovementDirection.Right;
                }

                _requestedMovementDirection = MovementDirection.Right;
            }

            _view.UpdateView(_currentPosition);
        }

        private void Move()
        {
            if (_currentMovementDirection != _requestedMovementDirection)
            {
                if (_currentPosition.X % 24 == 0 && _currentPosition.Y % 24 == 0)
                {
                    if (_mapSystem.CanMove(_currentPosition, _requestedMovementDirection))
                    {
                        _currentMovementDirection = _requestedMovementDirection;
                    }
                    else
                    {
                        _requestedMovementDirection = _currentMovementDirection;
                    }
                }
            }

            if (!_mapSystem.CanMove(_currentPosition, _currentMovementDirection))
                return;

            switch (_currentMovementDirection)
            {
                case MovementDirection.Up:
                    _currentPosition.Y -= PlayerSpeed;
                    break;
                case MovementDirection.Down:
                    _currentPosition.Y += PlayerSpeed;
                    break;
                case MovementDirection.Left:
                    _currentPosition.X -= PlayerSpeed;
                    break;
                case MovementDirection.Right:
                    _currentPosition.X += PlayerSpeed;
                    break;
            }
        }
    }
}