using common.Core.Bootstrap;
using pacman_port.Game.Configurations;
using pacman_port.Game.Services.Game;
using pacman_port.Game.Services.Sprite;
using pacman_port.Game.States.Game;
using pacman_port.Game.States.InitGame;
using pacman_port.Game.States.IntroGame;
using pacman_port.Game.States.LoadingGame;
using pacman_port.Game.States.PressStart;
using pacman_port.Game.Systems.Consumer;
using pacman_port.Game.Systems.Fruit;
using pacman_port.Game.Systems.Map;
using pacman_port.Game.Systems.Player;
using pacman_port.Game.Systems.UI;

namespace pacman_port.Game
{
    public class Bootstrap : BaseBootstrap<InitGameState, PacmanConfig>
    {
        private SpriteService _spriteService;
        private GameService _gameService;

        private FruitSystem _fruitSystem;
        private PlayerSystem _playerSystem;
        private MapSystem _mapSystem;
        private ConsumerSystem _consumerSystem;
        private UISystem _uiSystem;
        
        public Bootstrap(PacmanConfig config) : base(config) { }

        protected override void InitCustomServices()
        {
            _spriteService = new SpriteService(ScreenService);
            _gameService = new GameService();
            _spriteService.Init();
            _gameService.Init();
        }

        protected override void BuildCustomSystems()
        {
            _fruitSystem = new FruitSystem(ScreenService, RenderService, _spriteService);
            _mapSystem = new MapSystem(ScreenService, RenderService, _spriteService);
            _playerSystem = new PlayerSystem(ScreenService, RenderService, _spriteService);
            _consumerSystem = new ConsumerSystem(_gameService, _playerSystem, _mapSystem);
            _uiSystem = new UISystem(ScreenService, RenderService, _spriteService, _gameService);
        }

        protected override void RegisterCustomStates()
        {
            StateFactory.RegisterState(new InitGameState(GameManager, _uiSystem));
            StateFactory.RegisterState(new IntroGameState(GameManager, _uiSystem));
            StateFactory.RegisterState(new PressStartState(GameManager, _uiSystem));
            StateFactory.RegisterState(new LoadingGameState(GameManager, _uiSystem));
            StateFactory.RegisterState(new GameState(GameManager, _gameService, _uiSystem, _fruitSystem, _mapSystem, _playerSystem, _consumerSystem));
        }
    }
}