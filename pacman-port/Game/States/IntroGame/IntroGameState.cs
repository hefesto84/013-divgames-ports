using System;
using common.Core.Managers.Game;
using common.Core.States.Base;
using pacman_port.Game.States.PressStart;

namespace pacman_port.Game.States.IntroGame
{
    public class IntroGameState : State
    {
        public IntroGameState(GameManager gameManager, Type type) : base(gameManager, type)
        {
        }

        public override void DoState()
        {
            Console.WriteLine("DO PRESENTATION");
            GameManager.SetState(GameManager.StateFactory.Get(typeof(PressStartState)));
        }
    }
}