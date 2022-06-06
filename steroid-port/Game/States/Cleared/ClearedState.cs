using common.Core.Managers.Game;
using common.Core.States.Base;
using Raylib_cs;
using steroid_port.Game.States.Game;
using steroid_port.Game.Systems.Background;
using steroid_port.Game.Systems.UI;

namespace steroid_port.Game.States.Cleared
{
    public class ClearedState : State
    {
        private readonly UISystem _uiSystem;
        private readonly BackgroundSystem _backgroundSystem;
        
        public ClearedState(GameManager gameManager, BackgroundSystem backgroundSystem, UISystem uiSystem) : base(gameManager, typeof(ClearedState))
        {
            _uiSystem = uiSystem;
            _backgroundSystem = backgroundSystem;
        }

        public override void Start()
        {
            _uiSystem.Init();
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