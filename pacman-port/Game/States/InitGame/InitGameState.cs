using System;
using common.Core.Managers.Game;
using common.Core.States.Base;
using pacman_port.Game.States.IntroGame;
using pacman_port.Game.Systems.UI;

namespace pacman_port.Game.States.InitGame
{
    public class InitGameState : State
    {
        public InitGameState(GameManager gameManager, UISystem uiSystem) : base(gameManager, typeof(InitGameState))
        {
        }

        public override void DoState()
        {
            Console.WriteLine("DO INIT");
            GameManager.SetState(GameManager.StateFactory.Get(typeof(IntroGameState)));
        }
    }
}