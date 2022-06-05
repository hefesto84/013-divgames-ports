using steroid_port.Game.Enums;
using steroid_port.Game.Managers;
using steroid_port.Game.Managers.Game;

namespace steroid_port.Game.States.Base
{
    public abstract class State
    {
        protected readonly GameManager GameManager;
        
        public StateType StateType { get; }
        
        protected State(GameManager gameManager, StateType stateType)
        {
            GameManager = gameManager;
            StateType = stateType;
        }

        public virtual void Start(){}
        
        public abstract void DoState();

        public virtual void Stop(){}
    }
}