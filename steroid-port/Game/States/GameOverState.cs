using System;
using Raylib_cs;
using steroid_port.Game.Managers;
using steroid_port.Game.States.Base;
using steroid_port.Game.Systems.Background;
using steroid_port.Game.Systems.UI;

namespace steroid_port.Game.States
{
    public class GameOverState : State
    {
        private readonly UISystem _uiSystem;
        private readonly BackgroundSystem _backgroundSystem;
        
        public GameOverState(GameManager gameManager, BackgroundSystem backgroundSystem, UISystem uiSystem, StateType stateType) : base(gameManager, stateType)
        {
            _uiSystem = uiSystem;
            _backgroundSystem = backgroundSystem;
        }

        public override void Start()
        {
            _uiSystem.SetState(this);
        }

        public override void DoState()
        {
            _backgroundSystem.Update();
            _uiSystem.Update();
            
            
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_P))
            {
                GameManager.SetState(GameManager.StateFactory.Get(StateType.InitGameState));
            }
        }

        public override void Stop()
        {
            
        }
    }
}