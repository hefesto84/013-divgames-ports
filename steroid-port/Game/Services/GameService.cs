using System;

namespace steroid_port.Game.Services
{
    public class GameService
    {
        public Action<int> OnLivesUpdated { get; set; }
        public int MaxLives { get; set; }
        public int CurrentScore { get; set; }
        public int CurrentLevel { get; set; }
        
        private int _currentLives = 0;
        
        public GameService()
        {
            MaxLives = 3;
        }

        public void Init()
        {
            Reset();
        }

        public void SetLives(int lives)
        {
            _currentLives = lives;
            OnLivesUpdated?.Invoke(_currentLives);
        }
        
        private void Reset()
        {
            _currentLives = 3;
            CurrentScore = 0;
            CurrentLevel = 1;
        }
    }
}