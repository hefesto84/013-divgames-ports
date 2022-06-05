using System;
using System.Collections.Generic;
using System.Numerics;
using steroid_port.Game.Services;
using steroid_port.Game.Services.Render;
using steroid_port.Game.Services.Screen;
using steroid_port.Game.Services.Sprite;
using steroid_port.Game.Views;
using steroid_port.Game.Views.Asteroid;

namespace steroid_port.Game.Systems.Asteroids
{
    public class AsteroidsSystem : Base.System
    {
        private readonly ScreenService _screenService;
        private readonly SpriteService _spriteService;
        private readonly RenderService _renderService;

        private List<AsteroidView> _views;
        private int _rotation;
        private int _initialAsteroids = 6;

        private readonly Random _random;

        public List<AsteroidView> Asteroids => _views;

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
            _rotation = 0;
        }

        public override void Update()
        {
            _rotation += 2;

            for (var i = 0; i < _views.Count; i++)
            {
                _views[i].UpdateView(_rotation);
            }
        }

        private void SetupAsteroidView()
        {
            for (var i = 0; i < _initialAsteroids; i++)
            {
                _views.Add(new AsteroidView(_renderService));
                _views[i].Init(_spriteService, new Vector2(_random.Next(0, (int) _screenService.CurrentSize.X), _random.Next(0, (int) _screenService.CurrentSize.Y)));
            }
        }

        public void Hit(int asteroidId)
        {
            _views.RemoveAt(asteroidId);
        }
    }
}