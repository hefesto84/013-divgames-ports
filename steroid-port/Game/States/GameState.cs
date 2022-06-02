using steroid_port.Game.Managers;
using steroid_port.Game.States.Base;
using steroid_port.Game.Systems.Background;
using steroid_port.Game.Systems.Ship;
using steroid_port.Game.Systems.UI;

namespace steroid_port.Game.States
{
    public class GameState : State
    {
        private readonly ShipSystem _shipSystem;
        private readonly BackgroundSystem _backgroundSystem;
        private readonly UISystem _uiSystem;
        
        public GameState(GameManager gameManager, BackgroundSystem backgroundSystem, ShipSystem shipSystem, UISystem uiSystem, StateType stateType) : base(gameManager, stateType)
        {
            _backgroundSystem = backgroundSystem;
            _shipSystem = shipSystem;
            _uiSystem = uiSystem;
        }

        public override void Start()
        {
            _backgroundSystem.Init();
            _shipSystem.Init();
            _uiSystem.Init();
            
            _uiSystem.SetState(this);
        }

        public override void DoState()
        {
            _backgroundSystem.Update();
            _shipSystem.Update();
            _uiSystem.Update();
        }
    }
}