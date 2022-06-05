using steroid_port.Game.Factories.States;
using steroid_port.Game.States.Base;

namespace steroid_port.Game.Managers.Game
{
    public class GameManager
    {
        private State _currentState;
        private bool IsReady { get; set; }
        
        public StateFactory StateFactory { get; private set; }

        public void Init(StateFactory stateFactory)
        {
            StateFactory = stateFactory;
            
            CreateStatesAndSetDependencies();
        }
        
        public void SetState(State state)
        {
            _currentState?.Stop();
            _currentState = state;
            _currentState.Start();
        }
        
        public void Update()
        {
            if (!IsReady) return;
            
            _currentState?.DoState();
        }

        private void CreateStatesAndSetDependencies()
        {
            IsReady = true;
        }
    }
}