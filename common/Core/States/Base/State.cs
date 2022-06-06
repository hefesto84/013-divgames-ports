using System;
using common.Core.Managers.Game;

namespace common.Core.States.Base
{
    public abstract class State
    {
        protected readonly GameManager GameManager;
        
        public StateType StateType { get; }
        
        protected State(GameManager gameManager, Type type)
        {
            GameManager = gameManager;
            StateType = new StateType(type);
        }

        public virtual void Start(){}
        
        public abstract void DoState();

        public virtual void Stop(){}
    }
}