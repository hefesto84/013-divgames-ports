using System;
using Raylib_cs;
using steroid_port.Game.Services;

namespace steroid_port.Game.Systems.Game
{
    public class GameSystem : Base.System
    {
        public Action<int> OnGameOver { get; set; }
        
        private readonly GameService _gameService;
        private int _currentLives = 0;

        public GameSystem(GameService gameService)
        {
            _gameService = gameService;
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
                OnGameOver?.Invoke(100);
            }
        }
    }
}