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
        
        private Vector2 _initialPlayerPosition;
        private MovementDirection _requestedMovementDirection = MovementDirection.None;
        private MovementDirection _currentMovementDirection = MovementDirection.None;
        private int TileWidth { get; set; }
        private int TileHeight { get; set; }
        
        private const int InitialTileX = 1;
        private const int InitialTileY = 4;
        private const int PlayerSpeed = 2;
        
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
            
            _view.Init();

            TileWidth = (int)_view.Bounds.width;
            TileHeight = (int) _view.Bounds.height;
            
            _initialPlayerPosition = new Vector2(TileWidth * InitialTileX, TileHeight * InitialTileY);
        }

        public override void Reset()
        {
            _currentPosition = _initialPlayerPosition;
            
            _currentMovementDirection = MovementDirection.None;
            _requestedMovementDirection = MovementDirection.None;
        }

        public override void Update()
        {
            Move();

            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
            {
                _requestedMovementDirection = MovementDirection.Up;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
            {
                _requestedMovementDirection = MovementDirection.Down;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                _requestedMovementDirection = MovementDirection.Left;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                _requestedMovementDirection = MovementDirection.Right;
            }

            _view.UpdateView(_currentPosition, _currentMovementDirection);
        }

        private void Move()
        {
            CheckIfDirectionCanBeChanged();

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

        private void CheckIfDirectionCanBeChanged()
        {
            if (_currentMovementDirection == _requestedMovementDirection) return;

            if (_currentPosition.X % TileWidth != 0 || _currentPosition.Y % TileHeight != 0) return;
            
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
}