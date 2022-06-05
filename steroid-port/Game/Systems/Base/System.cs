using steroid_port.Game.Factories;
using steroid_port.Game.Factories.States;
using steroid_port.Game.States.Base;

namespace steroid_port.Game.Systems.Base
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