using System;
using Raylib_cs;
using steroid_port.Game.Services;
using steroid_port.Game.States.Base;
using steroid_port.Game.Utils;
using steroid_port.Game.Views;

namespace steroid_port.Game.Systems.UI
{
    public class UISystem : Base.System
    {
        private readonly ConfigService _configService;
        private readonly ScreenService _screenService;
        private readonly Utilities _utilities;
        private readonly RenderService _renderService;
        private readonly SpriteService _spriteService;
        private readonly GameService _gameService;
        private int[] _textSizes;
        private string[] _texts;
        private LivesView _livesView;
        
        public UISystem(ConfigService configService, ScreenService screenService,  RenderService renderService, SpriteService spriteService, GameService gameService, Utilities utilities)
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
            _textSizes = new int[5];
            _texts = new string[5];
            
            _texts[0] = _configService.Config.Texts["GAME_TITLE_1_KEY"];
            _texts[1] = _configService.Config.Texts["GAME_TITLE_2_KEY"];
            _texts[2] = _configService.Config.Texts["GAME_PRESS_PLAY_KEY"];
            _texts[3] = _configService.Config.Texts["HELP_KEY"];
            _texts[4] = $"Score: {_gameService.CurrentScore}";

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
        }

        private void SetupLivesView()
        {
            if (_livesView != null) return;
            
            _livesView = new LivesView(_renderService, _gameService, _screenService);
            _livesView.Init(_spriteService);
        }
        
        private void DrawTexts()
        {
            if (CurrentState.StateType != StateType.InitGameState) return;
            
            Raylib.DrawText(_texts[0], (int)_screenService.CurrentSize.X/2 - _textSizes[0]/2, 0, 16, Color.YELLOW);
            Raylib.DrawText(_texts[1], (int)_screenService.CurrentSize.X/2 - _textSizes[1]/2, 20, 16, Color.YELLOW);
            
            Raylib.DrawText(_texts[2], (int)_screenService.CurrentSize.X/2 - _textSizes[2]/2, (int)_screenService.CurrentSize.Y /2 - 8, 16, Color.YELLOW);
            Raylib.DrawText(_texts[3], (int)_screenService.CurrentSize.X/2 - _textSizes[3]/2, (int)_screenService.CurrentSize.Y - 16, 16, Color.YELLOW);
        }

        private void DrawLives()
        {
            if (CurrentState.StateType != StateType.GameState) return;
            _livesView.UpdateView();
        }

        private void DrawScore()
        {
            if (CurrentState.StateType != StateType.GameOverState) return;
            
            Raylib.DrawText(_texts[4], (int)_screenService.CurrentSize.X/2 - _textSizes[4]/2, (int)_screenService.CurrentSize.Y /2 - 8, 16, Color.YELLOW);
        }
    }
}