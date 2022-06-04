using System;
using System.Numerics;
using Raylib_cs;
using steroid_port.Game.Services;
using steroid_port.Game.Systems.Ship;
using steroid_port.Game.Views;

namespace steroid_port.Game.Systems.Shot
{
    public class ShotSystem : Base.System
    {
        private readonly ScreenService _screenService;
        private readonly SpriteService _spriteService;
        private readonly RenderService _renderService;
        private readonly ShipSystem _shipSystem;
        private ShotView _view;
        
        public ShotSystem(ScreenService screenService, SpriteService spriteService, RenderService renderService, ShipSystem shipSystem)
        {
            _screenService = screenService;
            _spriteService = spriteService;
            _renderService = renderService;
            _shipSystem = shipSystem;
        }

        public override void Init()
        {
            Reset();
        }

        public override void Reset()
        {
            
        }

        public override void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_F))
            {
                Shoot();
            }

            _view?.UpdateView();
        }

        private void Shoot()
        {
            SetupShotView();
            _view.SetView(_shipSystem.CurrentPosition, _shipSystem.CurrentVelocity, _shipSystem.CurrentRotation);
        }
        
        private void SetupShotView()
        {
            if (_view != null) return;
            
            _view = new ShotView(_renderService);
            _view.Init(_spriteService);
        }
    }
}