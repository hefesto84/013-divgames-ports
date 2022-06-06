using common.Core.Bootstrap;
using steroid_port.Game.Configurations;
using steroid_port.Game.Services.Game;
using steroid_port.Game.Services.Sprite;
using steroid_port.Game.States.Cleared;
using steroid_port.Game.States.Game;
using steroid_port.Game.States.GameOver;
using steroid_port.Game.States.InitGame;
using steroid_port.Game.Systems.Asteroids;
using steroid_port.Game.Systems.Background;
using steroid_port.Game.Systems.Collision;
using steroid_port.Game.Systems.Game;
using steroid_port.Game.Systems.Ship;
using steroid_port.Game.Systems.Shot;
using steroid_port.Game.Systems.UI;

namespace steroid_port.Game
{
    public class Bootstrap : BaseBootstrap<InitGameState, SteroidConfig>
    {
        private SpriteService _spriteService;
        private GameService _gameService;

        private ShipSystem _shipSystem;
        private AsteroidsSystem _asteroidsSystem;
        private ShotSystem _shotSystem;
        private CollisionSystem _collisionSystem;
        private BackgroundSystem _backgroundSystem;
        private GameSystem _gameSystem;
        private UISystem _uiSystem;
        
        public Bootstrap(SteroidConfig steroidConfig) : base(steroidConfig) { }

        protected override void InitCustomServices()
        {
            _spriteService = new SpriteService(ScreenService);
            _gameService = new GameService();
            
            _spriteService.Init();
            _gameService.Init();
        }
        
        protected override void BuildCustomSystems()
        {
            _shipSystem = new ShipSystem(ScreenService, _spriteService, RenderService);
            _asteroidsSystem = new AsteroidsSystem(ScreenService, _spriteService, RenderService);
            _shotSystem = new ShotSystem(ScreenService, _spriteService, RenderService, _shipSystem);
            _collisionSystem = new CollisionSystem(CollisionService, _shipSystem, _asteroidsSystem, _shotSystem);
            _backgroundSystem = new BackgroundSystem(_spriteService, RenderService);
            _uiSystem = new UISystem(ConfigService, ScreenService, RenderService, _spriteService, _gameService, Utilities);
            _gameSystem = new GameSystem(_gameService, _collisionSystem, _shipSystem, _asteroidsSystem, _shotSystem);
            
            RegisterSystem(_shipSystem);
            RegisterSystem(_asteroidsSystem);
            RegisterSystem(_shotSystem);
            RegisterSystem(_collisionSystem);
            RegisterSystem(_backgroundSystem);
            RegisterSystem(_uiSystem);
            RegisterSystem(_gameSystem);
        }
        
        protected override void RegisterCustomStates()
        {
            StateFactory.RegisterState(new InitGameState(GameManager, _uiSystem, _gameSystem, _backgroundSystem));
            StateFactory.RegisterState(new GameState(GameManager, _backgroundSystem, _shipSystem, _asteroidsSystem, _shotSystem, _collisionSystem, _uiSystem, _gameSystem));
            StateFactory.RegisterState(new GameOverState(GameManager, _backgroundSystem, _uiSystem));
            StateFactory.RegisterState(new ClearedState(GameManager,_backgroundSystem,_uiSystem));
        }

        ~Bootstrap()
        {
            UnloadSystems();
        }
    }
}