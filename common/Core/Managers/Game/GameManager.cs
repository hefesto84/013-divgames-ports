using common.Core.Factories.States;
using common.Core.States.Base;

namespace common.Core.Managers.Game
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