using System;
using Raylib_cs;
using steroid_port.Game.Services;
using steroid_port.Game.Services.Game;
using steroid_port.Game.Systems.Asteroids;
using steroid_port.Game.Systems.Collision;
using steroid_port.Game.Systems.Ship;
using steroid_port.Game.Systems.Shot;

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
        private readonly ShotSystem _shotSystem;

        public GameSystem(GameService gameService, CollisionSystem collisionSystem, ShipSystem shipSystem, AsteroidsSystem asteroidsSystem, ShotSystem shotSystem)
        {
            _gameService = gameService;
            _collisionSystem = collisionSystem;
            _shipSystem = shipSystem;
            _asteroidsSystem = asteroidsSystem;
            _shotSystem = shotSystem;

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
            _gameService.CurrentScore = 0;
        }

        public override void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
            {
                _gameService.CurrentLives--;
            }

            if (_gameService.CurrentLives == 0)
            {
                OnGameOver?.Invoke();
            }

            if (_asteroidsSystem.Asteroids.Count != 0) return;
            
            OnGameCleared?.Invoke();
            _gameService.CurrentLevel++;
        }
        
        private void OnCollision()
        {
            _gameService.CurrentLives--;
            _shipSystem.Reset();
        }

        private void OnAsteroidShot(int asteroidId, int shotId)
        {
            _asteroidsSystem.Hit(asteroidId);
            _shotSystem.Shots[shotId].Recycle();
            
            _gameService.CurrentScore += 10;
        }
    }
}