using System.Numerics;
using Raylib_cs;
using steroid_port.Game.Services;
using steroid_port.Game.Views;

namespace steroid_port.Game.Systems.Ship
{
    public class ShipSystem : Base.System
    {
        private readonly ScreenService _screenService;
        private readonly RenderService _renderService;
        private readonly SpriteService _spriteService;
        
        private ShipView _view;
        private Vector3 _currentPosition = Vector3.Zero;
        
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
        }

        public override void Update()
        {
            _view.UpdateView(_currentPosition);
            _currentPosition.X += 1;

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                Reset();
            }
        }

        private void SetupShipView()
        {
            if (_view != null) return;
            
            _view = new ShipView(_renderService);
            _view.Init(_spriteService);
        }
    }
}