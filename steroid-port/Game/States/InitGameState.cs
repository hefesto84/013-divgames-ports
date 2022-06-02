using Raylib_cs;
using steroid_port.Game.Factories;
using steroid_port.Game.Managers;
using steroid_port.Game.Services;
using steroid_port.Game.States.Base;
using steroid_port.Game.Systems.Background;
using steroid_port.Game.Systems.UI;
using steroid_port.Game.Utils;

namespace steroid_port.Game.States
{
    public class InitGameState : State
    {
        private readonly BackgroundSystem _backgroundSystem;
        private readonly UISystem _uiSystem;
        

        public InitGameState(GameManager gameManager, UISystem uiSystem , BackgroundSystem backgroundSystem, StateType stateType) : base(gameManager, stateType)
        {
            _uiSystem = uiSystem;
            _backgroundSystem = backgroundSystem;
        }

        public override void Start()
        {
            _uiSystem.Init();
            _backgroundSystem.Init();
            
            _uiSystem.SetState(this);
        }

        public override void DoState()
        {
            _backgroundSystem.Update();
            _uiSystem.Update();
            
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                GameManager.SetState(GameManager.StateFactory.Get(StateType.GameState));
            }
        }
    }
}