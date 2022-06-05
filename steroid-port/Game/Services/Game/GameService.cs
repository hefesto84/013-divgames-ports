namespace steroid_port.Game.Services.Game
{
    public class GameService
    {
        public int MaxLives { get; }
        public int CurrentScore { get; set; }
        public int CurrentLevel { get; set; }
        public int CurrentLives { get; set; }
        
        public GameService()
        {
            MaxLives = 3;
        }

        public void Init()
        {
            Reset();
        }

        private void Reset()
        {
            CurrentLives = MaxLives;
            CurrentScore = 0;
            CurrentLevel = 1;
        }
    }
}