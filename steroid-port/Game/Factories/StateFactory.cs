using System;
using System.Collections.Generic;
using steroid_port.Game.States.Base;

namespace steroid_port.Game.Factories
{
    public class StateFactory
    {
        private Dictionary<StateType, State> _states;

        public void Init()
        {
            _states = new Dictionary<StateType, State>();
        }

        public void RegisterState(State state)
        {
            var result = _states.TryAdd(state.StateType, state);
            
            if (!result)
            {
                throw new Exception("Value already registered");
            }
        }

        public State Get(StateType stateType)
        {
            if (!_states.TryGetValue(stateType, out var state))
            {
                throw new Exception("State not registered.");
            }

            return state;
        }
    }
}