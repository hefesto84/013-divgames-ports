using System;
using Raylib_cs;
using steroid_port.Game.Configurations;
using steroid_port.Game.Factories;
using steroid_port.Game.Managers;
using steroid_port.Game.Services;
using steroid_port.Game.States;
using steroid_port.Game.States.Base;
using steroid_port.Game.Systems.Asteroids;
using steroid_port.Game.Systems.Background;
using steroid_port.Game.Systems.Game;
using steroid_port.Game.Systems.Render;
using steroid_port.Game.Systems.Ship;
using steroid_port.Game.Systems.Shot;
using steroid_port.Game.Systems.UI;
using steroid_port.Game.Utils;

namespace steroid_port.Game
{
    public class Bootstrap
    {
        private GameManager _gameManager;
        private StateFactory _stateFactory;
        private ConfigService _configService;
        private RenderService _renderService;
        private ScreenService _screenService;
        private SpriteService _spriteService;
        private GameService _gameService;
        private Utilities _utilities;

        private ShipSystem _shipSystem;
        private AsteroidsSystem _asteroidsSystem;
        private ShotSystem _shotSystem;
        private BackgroundSystem _backgroundSystem;
        private RenderSystem _renderSystem;
        private GameSystem _gameSystem;
        private UISystem _uiSystem;
        
        
        public bool IsQuit => Raylib.WindowShouldClose();

        private readonly SteroidConfig _steroidConfig;

        public Bootstrap(SteroidConfig steroidConfig)
        {
            _steroidConfig = steroidConfig;
        }

        public void Init()
        {
            _gameManager = new GameManager();

            InitUtilities();
            InitServices();
            BuildSystems();
            InitFactories();
            
            _gameManager.Init(_stateFactory);
            _gameManager.SetState(_stateFactory.Get(StateType.InitGameState));
        }
        
        public void Update()
        {
            _renderService.Begin();
            
            _gameManager.Update();
            
            _renderService.End();
        }

        private void InitUtilities()
        {
            _utilities = new Utilities();
        }
        
        private void InitServices()
        {
            _configService = new ConfigService();
            _renderService = new RenderService(_steroidConfig);
            _screenService = new ScreenService();
            _spriteService = new SpriteService(_screenService);
            _gameService = new GameService();
            
            
            _configService.Init(_steroidConfig);
            _screenService.Init(_steroidConfig.Width,_steroidConfig.Height);
            _renderService.Init();
            _spriteService.Init();
            _gameService.Init();
        }
        
        private void BuildSystems()
        {
            _shipSystem = new ShipSystem(_screenService, _spriteService, _renderService);
            _asteroidsSystem = new AsteroidsSystem(_screenService, _spriteService, _renderService);
            _shotSystem = new ShotSystem(_screenService, _spriteService, _renderService);
            _backgroundSystem = new BackgroundSystem(_spriteService, _renderService);
            _renderSystem = new RenderSystem();
            _uiSystem = new UISystem(_configService, _screenService, _renderService, _spriteService, _gameService, _utilities);
            _gameSystem = new GameSystem(_gameService);
        }
        
        private void InitFactories()
        {
            _stateFactory = new StateFactory();
            _stateFactory.Init();
            _stateFactory.RegisterState(new InitGameState(_gameManager, _uiSystem, _backgroundSystem, StateType.InitGameState));
            _stateFactory.RegisterState(new GameState(_gameManager, _backgroundSystem, _shipSystem, _asteroidsSystem, _shotSystem, _uiSystem, _gameSystem, StateType.GameState));
            _stateFactory.RegisterState(new GameOverState(_gameManager, _backgroundSystem, _uiSystem, StateType.GameOverState));
        }

        ~Bootstrap()
        {
            Console.WriteLine("Destroy here EVERYTHING");
        }
    }
}