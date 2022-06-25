using System;
using common.Core.Managers.Game;
using common.Core.States.Base;
using pacman_port.Game.States.LoadingGame;

namespace pacman_port.Game.States.PressStart
{
    public class PressStartState : State
    {
        public PressStartState(GameManager gameManager, Type type) : base(gameManager, type)
        {
        }

        public override void DoState()
        {
            Console.WriteLine("DO PRESS START");
            GameManager.SetState(GameManager.StateFactory.Get(typeof(LoadingGameState)));
        }
    }
}