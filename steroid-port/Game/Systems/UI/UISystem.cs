using common.Core.Services.Config;
using common.Core.Services.Render;
using common.Core.Services.Screen;
using common.Core.Utils;
using Raylib_cs;
using steroid_port.Game.Configurations;
using steroid_port.Game.Services.Game;
using steroid_port.Game.Services.Sprite;
using steroid_port.Game.States;
using steroid_port.Game.States.Cleared;
using steroid_port.Game.States.Game;
using steroid_port.Game.States.GameOver;
using steroid_port.Game.States.InitGame;
using steroid_port.Game.Views.Lives;

namespace steroid_port.Game.Systems.UI
{
    public class UISystem : common.Core.Systems.Base.System
    {
        private readonly ConfigService<SteroidConfig> _configService;
        private readonly ScreenService _screenService;
        private readonly Utilities _utilities;
        private readonly RenderService _renderService;
        private readonly SpriteService _spriteService;
        private readonly GameService _gameService;
        private int[] _textSizes;
        private string[] _texts;
        private LivesView _livesView;
        
        public UISystem(ConfigService<SteroidConfig> configService, ScreenService screenService,  RenderService renderService, SpriteService spriteService, GameService gameService, Utilities utilities)
        {
            _screenService = screenService;
            _configService = configService;
            _renderService = renderService;
            _spriteService = spriteService;
            _gameService = gameService;
            _utilities = utilities;
        }
        public override void Init()
        {
            _textSizes = new int[6];
            _texts = new string[6];
            
            _texts[0] = _configService.Config.Texts["GAME_TITLE_1_KEY"];
            _texts[1] = _configService.Config.Texts["GAME_TITLE_2_KEY"];
            _texts[2] = _configService.Config.Texts["GAME_PRESS_PLAY_KEY"];
            _texts[3] = _configService.Config.Texts["HELP_KEY"];
            _texts[4] = $"GameOver\n Score: {_gameService.CurrentScore}";
            _texts[5] = $"Level {_gameService.CurrentLevel} Cleared\n Score: {_gameService.CurrentScore}";

            for (var i = 0; i < _texts.Length; i++)
            {
                _textSizes[i] = _utilities.GetTextWidth(_texts[i], 16);
            }

            SetupLivesView();
        }

        public override void Update()
        {
            DrawTexts();
            DrawLives();
            DrawScore();
            DrawLevelCleared();
        }

        private void SetupLivesView()
        {
            if (_livesView != null) return;
            
            _livesView = new LivesView(_renderService, _gameService);
            _livesView.Init(_spriteService);
        }
        
        private void DrawTexts()
        {
            if (CurrentState.StateType.Type != typeof(InitGameState)) return;
            
            _renderService.RenderText(_texts[0], (int)_screenService.CurrentSize.X/2 - _textSizes[0]/2, 0, 16, Color.YELLOW);
            _renderService.RenderText(_texts[1], (int)_screenService.CurrentSize.X/2 - _textSizes[1]/2, 20, 16, Color.YELLOW);
            
            _renderService.RenderText(_texts[2], (int)_screenService.CurrentSize.X/2 - _textSizes[2]/2, (int)_screenService.CurrentSize.Y /2 - 8, 16, Color.YELLOW);
            _renderService.RenderText(_texts[3], (int)_screenService.CurrentSize.X/2 - _textSizes[3]/2, (int)_screenService.CurrentSize.Y - 16, 16, Color.YELLOW);
        }

        private void DrawLives()
        {
            if (CurrentState.StateType.Type != typeof(GameState)) return;
            _livesView.UpdateView();
        }

        private void DrawScore()
        {
            if (CurrentState.StateType.Type != typeof(GameOverState)) return;
            
            _renderService.RenderText(_texts[4], (int)_screenService.CurrentSize.X/2 - _textSizes[4]/2, (int)_screenService.CurrentSize.Y /2 - 8, 16, Color.YELLOW);
        }

        private void DrawLevelCleared()
        {
            if (CurrentState.StateType.Type != typeof(ClearedState)) return;
            
            _renderService.RenderText(_texts[5], (int)_screenService.CurrentSize.X/2 - _textSizes[5]/2, (int)_screenService.CurrentSize.Y / 2 - 8, 16, Color.YELLOW);
        }
    }
}