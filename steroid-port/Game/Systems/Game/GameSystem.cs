using System;
using Raylib_cs;
using steroid_port.Game.Services;
using steroid_port.Game.Systems.Collision;
using steroid_port.Game.Systems.Ship;

namespace steroid_port.Game.Systems.Game
{
    public class GameSystem : Base.System
    {
        public Action OnGameOver { get; set; }
        
        private readonly GameService _gameService;
        private CollisionSystem _collisionSystem;
        private ShipSystem _shipSystem;
        private int _currentLives = 0;

        public GameSystem(GameService gameService, CollisionSystem collisionSystem, ShipSystem shipSystem)
        {
            _gameService = gameService;
            _collisionSystem = collisionSystem;
            _shipSystem = shipSystem;

            _collisionSystem.OnCollision += OnCollision;
        }
        
        ~GameSystem()
        {
            _collisionSystem.OnCollision -= OnCollision;
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
        }
        
        private void OnCollision()
        {
            _currentLives--;
            _gameService.SetLives(_currentLives);
            _shipSystem.Reset();
        }
    }
}