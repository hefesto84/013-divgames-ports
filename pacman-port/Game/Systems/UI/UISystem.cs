using System.Collections.Generic;
using System.Numerics;
using common.Core.Services.Render;
using common.Core.Services.Screen;
using pacman_port.Game.Services.Game;
using pacman_port.Game.Services.Sprite;
using pacman_port.Game.Views.UI;
using Raylib_cs;

namespace pacman_port.Game.Systems.UI
{
    public class UISystem : PacmanSystem
    {
        private readonly GameService _gameService;
        private FruitsView _fruitsView;
        private LivesView _livesView;
        public int CurrentLives { get; set; }
        public int CurrentScore { get; set; }
        public int CurrentRecordScore { get; set; }
        public List<int> CurrentFruitsId { get; set; }
        
        public UISystem(ScreenService screenService, RenderService renderService, SpriteService spriteService, GameService gameService) : 
            base(screenService, renderService, spriteService)
        {
            _gameService = gameService;

            CurrentLives = _gameService.CurrentLives;
            
            _gameService.OnLivesUpdated += OnLivesUpdated;
            _gameService.OnScoreUpdated += OnScoreUpdated;
            _gameService.OnFruitsUpdated += OnFruitsUpdated;
        }

        

        public override void Init()
        {
            SetupLivesView();
            SetupFruitsView();
            Reset();
        }

        private void SetupFruitsView()
        {
            if (_fruitsView == null)
            {
                _fruitsView = new FruitsView(RenderService, SpriteService);
            }
            
            _fruitsView.Init();
        }

        private void SetupLivesView()
        {
            if (_livesView == null)
            {
                _livesView = new LivesView(RenderService, SpriteService, this);
            }

            _livesView.Init(new Vector2(1,25.5f));
        }

        public override void Reset()
        {
            _livesView.Reset();
            _fruitsView.Reset();
        }

        public override void Update()
        {
            _livesView.Update();
            _fruitsView.Update();
        }

        ~UISystem()
        {
            _gameService.OnLivesUpdated -= OnLivesUpdated;
        }
        
        private void OnLivesUpdated(int lives)
        {
            CurrentLives = lives;
        }

        private void OnScoreUpdated(int currentScore, int recordScore)
        {
            CurrentScore = currentScore;
            CurrentRecordScore = recordScore;
        }

        private void OnFruitsUpdated(List<int> fruitsId)
        {
            CurrentFruitsId = fruitsId;
        }
    }
}