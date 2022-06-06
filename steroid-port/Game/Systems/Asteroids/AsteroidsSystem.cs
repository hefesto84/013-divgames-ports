using System;
using System.Collections.Generic;
using System.Numerics;
using steroid_port.Game.Services.Render;
using steroid_port.Game.Services.Screen;
using steroid_port.Game.Services.Sprite;
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
        
        private const int InitialAsteroids = 6;
        private const int AsteroidsDividedNumber = 2;
        private const int AsteroidsSpeedRotation = 2;
        private const int InitialAsteroidLevel = 3;

        private readonly Random _random;

        public List<AsteroidView> Asteroids => _views;

        private List<Vector2> _asteroidInitialPositions;

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
            
            GenerateRandomInitialPositions();
        }

        public override void Update()
        {
            _rotation += AsteroidsSpeedRotation;

            for (var i = 0; i < _views.Count; i++)
            {
                _views[i].UpdateView(_rotation);
            }
        }

        private void GenerateRandomInitialPositions()
        {
            _asteroidInitialPositions ??= new List<Vector2>();
            
            _asteroidInitialPositions.Clear();
            
            /*
            _asteroidInitialPositions = new List<Vector2>
            {
                new Vector2(_random.Next(-50, 0), _random.Next(-50, 0)),
                new Vector2(_random.Next(_screenService.CurrentSize))
            };
            */
            
            _asteroidInitialPositions.Add(new Vector2(_random.Next(-50, 0), _random.Next(-50, 0)));
            _asteroidInitialPositions.Add(new Vector2(_random.Next(-50, 0), _random.Next((int)_screenService.CurrentSize.Y, (int)_screenService.CurrentSize.Y + 50)));
            //_asteroidInitialPositions.Add(new Vector2(_random.Next()));
        }

        private void SetupAsteroidView()
        {
            for (var i = 0; i < InitialAsteroids; i++)
            {
                CreateAndInitView(InitialAsteroidLevel, _asteroidInitialPositions[0]);
            }
        }

        public void Hit(int asteroidId)
        {
            var asteroidLevel = _views[asteroidId].Level;
            var initialPosition = _views[asteroidId].Bounds;

            _views.RemoveAt(asteroidId);

            if (asteroidLevel == 1) return;

            asteroidLevel--;

            SpawnAsteroids(asteroidLevel, new Vector2(initialPosition.x, initialPosition.y));
        }

        private void SpawnAsteroids(int asteroidLevel, Vector2 initialPosition)
        {
            for (var i = 0; i < AsteroidsDividedNumber; i++)
            {
                CreateAndInitView(asteroidLevel, initialPosition);
            }
        }

        private Vector2 GetRandomAsteroidVelocity()
        {
            return new( _random.Next(0, 2) * 2 - 1, _random.Next(0, 2) * 2 - 1);
        }

        private void CreateAndInitView(int asteroidLevel, Vector2 initialPosition)
        {
            var asteroidView = new AsteroidView(_renderService, _screenService);
            
            asteroidView.Init(
                _spriteService, 
                initialPosition, 
                GetRandomAsteroidVelocity(),
                asteroidLevel);
            
            _views.Add(asteroidView);
        }
    }
}