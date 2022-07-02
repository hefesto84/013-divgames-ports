using System;
using common.Core.Managers.Game;
using common.Core.States.Base;
using pacman_port.Game.States.Game;
using pacman_port.Game.Systems.UI;

namespace pacman_port.Game.States.LoadingGame
{
    public class LoadingGameState : State
    {
        public LoadingGameState(GameManager gameManager, UISystem uiSystem) : base(gameManager, typeof(LoadingGameState))
        {
        }

        public override void DoState()
        {
            Console.WriteLine("DO LOADING GAME");
            GameManager.SetState(GameManager.StateFactory.Get(typeof(GameState)));
        }
    }
}