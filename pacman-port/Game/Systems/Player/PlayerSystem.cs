using System.Numerics;
using common.Core.Services.Render;
using common.Core.Services.Screen;
using pacman_port.Game.Services;
using pacman_port.Game.Views.Player;
using Raylib_cs;

namespace pacman_port.Game.Systems.Player
{
    public class PlayerSystem : PacmanSystem
    {
        private PlayerView _view;
        private Vector2 _currentPosition;
        
        public PlayerSystem(ScreenService screenService, RenderService renderService, SpriteService spriteService) : 
            base(screenService, renderService, spriteService) { }
        
        public override void Init()
        {
            SetupPlayerView();
            Reset();
        }

        private void SetupPlayerView()
        {
            if (_view != null) return;
            
            _view = new PlayerView(RenderService, SpriteService);
            _view.Init(new Vector2(24*5,24*5));
        }

        public override void Reset()
        {
            _currentPosition = new Vector2(24 * 3, 24 * 3);
        }

        public override void Update()
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
            {
                _currentPosition.X += 2;
            }else if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            {
                _currentPosition.X -= 2;
            }else if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
            {
                _currentPosition.Y -= 2;
            }else if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
            {
                _currentPosition.Y += 2;
            }
            
            
            _view.UpdateView(_currentPosition);
        }

        public Vector2 GetPosition()
        {
            return _currentPosition;
        }
    }
}