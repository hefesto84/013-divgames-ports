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
            MaxLives = 3;
            CurrentLives = 3;
        }
    }
}