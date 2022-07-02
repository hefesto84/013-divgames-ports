using System;
using common.Core.Managers.Game;
using common.Core.States.Base;
using pacman_port.Game.States.LoadingGame;
using pacman_port.Game.Systems.UI;

namespace pacman_port.Game.States.PressStart
{
    public class PressStartState : State
    {
        public PressStartState(GameManager gameManager, UISystem uiSystem) : base(gameManager, typeof(PressStartState))
        {
        }

        public override void DoState()
        {
            Console.WriteLine("DO PRESS START");
            GameManager.SetState(GameManager.StateFactory.Get(typeof(LoadingGameState)));
        }
    }
}