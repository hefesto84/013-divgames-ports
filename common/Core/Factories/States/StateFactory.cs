using System;
using System.Collections.Generic;
using common.Core.States.Base;

namespace common.Core.Factories.States
{
    public class StateFactory
    {
        private Dictionary<Type, State> _states;

        public void Init()
        {
            _states = new Dictionary<Type, State>();
        }

        public void RegisterState(State state)
        {
            var result = _states.TryAdd(state.StateType.Type, state);
            
            if (!result)
            {
                throw new Exception("Value already registered");
            }
        }

        public State Get(Type stateType)
        {
            if (!_states.TryGetValue(stateType, out var state))
            {
                throw new Exception("State not registered.");
            }

            return state;
        }
    }
}