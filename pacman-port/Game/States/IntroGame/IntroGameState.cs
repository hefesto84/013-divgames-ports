using System;
using common.Core.Managers.Game;
using common.Core.States.Base;
using pacman_port.Game.States.PressStart;
using pacman_port.Game.Systems.UI;

namespace pacman_port.Game.States.IntroGame
{
    public class IntroGameState : State
    {
        public IntroGameState(GameManager gameManager, UISystem uiSystem) : base(gameManager, typeof(IntroGameState))
        {
        }

        public override void DoState()
        {
            Console.WriteLine("DO PRESENTATION");
            GameManager.SetState(GameManager.StateFactory.Get(typeof(PressStartState)));
        }
    }
}