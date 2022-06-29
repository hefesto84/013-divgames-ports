using System;
using pacman_port.Game.Enums;
using Raylib_cs;

namespace pacman_port.Game.Services.Game
{
    public class GameService
    {
        private int CurrentScore { get; set; }
        private int MaxLives { get; set; }
        private int CurrentLives { get; set; }
        
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
            
            Console.WriteLine($"Current Score: {CurrentScore}");
        }
    }
}