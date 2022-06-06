using common.Core.Managers.Game;
using common.Core.States.Base;
using Raylib_cs;
using steroid_port.Game.States.Game;
using steroid_port.Game.Systems.Background;
using steroid_port.Game.Systems.Game;
using steroid_port.Game.Systems.UI;

namespace steroid_port.Game.States.InitGame
{
    public class InitGameState : State
    {
        private readonly BackgroundSystem _backgroundSystem;
        private readonly UISystem _uiSystem;
        private readonly GameSystem _gameSystem;
        
        public InitGameState(
            GameManager gameManager, 
            UISystem uiSystem , 
            GameSystem gameSystem, 
            BackgroundSystem backgroundSystem) : base(gameManager, typeof(InitGameState))
        {
            _uiSystem = uiSystem;
            _gameSystem = gameSystem;
            _backgroundSystem = backgroundSystem;
        }

        public override void Start()
        {
            _uiSystem.Init();
            _backgroundSystem.Init();
            _gameSystem.Init();
            
            _uiSystem.SetState(this);
        }

        public override void DoState()
        {
            _backgroundSystem.Update();
            _uiSystem.Update();
            
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                GameManager.SetState(GameManager.StateFactory.Get(typeof(GameState)));
            }
        }
    }
}