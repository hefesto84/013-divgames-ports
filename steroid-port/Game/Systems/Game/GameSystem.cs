using System;
using Raylib_cs;
using steroid_port.Game.Services;
using steroid_port.Game.Systems.Asteroids;
using steroid_port.Game.Systems.Collision;
using steroid_port.Game.Systems.Ship;

namespace steroid_port.Game.Systems.Game
{
    public class GameSystem : Base.System
    {
        public Action OnGameOver { get; set; }
        public Action OnGameCleared { get; set; }
        
        private readonly GameService _gameService;
        private readonly CollisionSystem _collisionSystem;
        private readonly ShipSystem _shipSystem;
        private readonly AsteroidsSystem _asteroidsSystem;
        private int _currentLives = 0;

        public GameSystem(GameService gameService, CollisionSystem collisionSystem, ShipSystem shipSystem, AsteroidsSystem asteroidsSystem)
        {
            _gameService = gameService;
            _collisionSystem = collisionSystem;
            _shipSystem = shipSystem;
            _asteroidsSystem = asteroidsSystem;

            _collisionSystem.OnCollision += OnCollision;
            _collisionSystem.OnAsteroidShot += OnAsteroidShot;
        }
        
        ~GameSystem()
        {
            _collisionSystem.OnCollision -= OnCollision;
            _collisionSystem.OnAsteroidShot -= OnAsteroidShot;
        }

        public override void Init()
        {
            Reset();
        }

        public override void Reset()
        {
            _currentLives = 3;
            _gameService.SetLives(3);
            _gameService.CurrentScore = 0;
        }

        public override void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
            {
                _currentLives--;
                _gameService.SetLives(_currentLives);
            }

            if (_currentLives == 0)
            {
                _gameService.CurrentScore = 100;
                OnGameOver?.Invoke();
            }

            if (_asteroidsSystem.Asteroids.Count == 0)
            {
                OnGameCleared?.Invoke();
                _gameService.CurrentLevel++;
            }
        }
        
        private void OnCollision()
        {
            _currentLives--;
            _gameService.SetLives(_currentLives);
            _shipSystem.Reset();
        }

        private void OnAsteroidShot(int asteroidId)
        {
            _asteroidsSystem.Hit(asteroidId);
        }
    }
}