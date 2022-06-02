using steroid_port.Game.Managers;
using steroid_port.Game.States.Base;
using steroid_port.Game.Systems.Background;
using steroid_port.Game.Systems.Ship;

namespace steroid_port.Game.States
{
    public class GameState : State
    {
        private readonly ShipSystem _shipSystem;
        private readonly BackgroundSystem _backgroundSystem;
        
        public GameState(GameManager gameManager, BackgroundSystem backgroundSystem, ShipSystem shipSystem, StateType stateType) : base(gameManager, stateType)
        {
            _backgroundSystem = backgroundSystem;
            _shipSystem = shipSystem;
        }

        public override void Start()
        {
            _backgroundSystem.Init();
            _shipSystem.Init();
        }

        public override void DoState()
        {
            _backgroundSystem.Update();
            _shipSystem.Update();
        }
    }
}