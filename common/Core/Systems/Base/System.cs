using common.Core.Factories.States;
using common.Core.States.Base;

namespace common.Core.Systems.Base
{
    public abstract class System
    {
        protected State CurrentState;
        
        public StateFactory StateFactory;
        
        public virtual void Init(){}
        public virtual void Update(){}
        public virtual void Reset(){}

        public void SetState(State state)
        {
            CurrentState = state;
        }
    }
}