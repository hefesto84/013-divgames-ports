using System;
using System.Collections.Generic;
using pacman_port.Game.Enums;
using Raylib_cs;

namespace pacman_port.Game.Services.Game
{
    public class GameService
    {
        private int CurrentScore { get; set; }
        private int RecordScore { get; set; }
        private int MaxLives { get; set; }
        public int CurrentLives { get;  set; }
        
        public Action<int,int> OnScoreUpdated { get; set; }
        public Action<int> OnLivesUpdated { get; set; }
        public Action<List<int>> OnFruitsUpdated { get; set; }
        
        public GameService()
        {
                
        }

        public void Init()
        {
            Reset();
        }

        public void Reset()
        {
            CurrentScore = 0;
            RecordScore = 0;
            MaxLives = 3;
            CurrentLives = 3;
        }
        
        public void UpdateScore(TileType result)
        {
            switch (result)
            {
                case TileType.BigBall:
                    CurrentScore += (int) ConsumableType.BigBall;
                    break;
                case TileType.MiniBall:
                    CurrentScore += (int) ConsumableType.MiniBall;
                    break;
            }
            
            OnScoreUpdated?.Invoke(CurrentScore, RecordScore);
        }
    }
}