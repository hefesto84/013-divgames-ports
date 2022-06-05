using System;
using System.Diagnostics;
using steroid_port.Game.Managers;

namespace steroid_port.Game.States.Base
{
    public enum StateType
    {
        InitGameState,
        GameState,
        GameOverState,
        ClearedState
    }
    
    public abstract class State
    {
        protected readonly GameManager GameManager;
        
        public StateType StateType { get; private set; }
        
        protected State(GameManager gameManager, StateType stateType)
        {
            GameManager = gameManager;
            StateType = stateType;
        }

        public virtual void Start()
        {
            Console.WriteLine($"{GetType()} Start");
        }
        
        public abstract void DoState();

        public virtual void Stop()
        {
            Console.WriteLine($"{GetType()} Stop");
        }
    }
}