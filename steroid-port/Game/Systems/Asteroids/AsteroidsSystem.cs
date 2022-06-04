using System;
using System.Collections.Generic;
using System.Numerics;
using steroid_port.Game.Services;
using steroid_port.Game.Views;

namespace steroid_port.Game.Systems.Asteroids
{
    public class AsteroidsSystem : Base.System
    {
        private readonly ScreenService _screenService;
        private readonly SpriteService _spriteService;
        private readonly RenderService _renderService;

        private List<AsteroidView> _views;
        private Vector2 _currentPosition = Vector2.Zero;
        private int _rotation;
        private int _initialAsteroids = 6;

        private readonly Random _random;
        
        public AsteroidsSystem(ScreenService screenService, SpriteService spriteService, RenderService renderService)
        {
            _screenService = screenService;
            _spriteService = spriteService;
            _renderService = renderService;
            _random = new Random();
        }

        public override void Init()
        {
            Reset();
            SetupAsteroidView();
        }

        public override void Reset()
        {
            _views = new List<AsteroidView>();
            _currentPosition = _screenService.CurrentScreenCenter;
            _rotation = 0;
        }

        public override void Update()
        {
            _rotation += 2;

            for (var i = 0; i < _initialAsteroids; i++)
            {
                _views[i].UpdateView(_rotation);
            }
            
        }

        private void SetupAsteroidView()
        {
            for (var i = 0; i < _initialAsteroids; i++)
            {
                _views.Add(new AsteroidView(_renderService, _screenService));
                _views[i].Init(_spriteService, new Vector2(_random.Next(0, (int) _screenService.CurrentSize.X), _random.Next(0, (int) _screenService.CurrentSize.Y)));
            }
        }
    }
}