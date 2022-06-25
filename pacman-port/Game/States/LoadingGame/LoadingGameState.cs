using System;
using common.Core.Managers.Game;
using common.Core.States.Base;
using pacman_port.Game.States.Game;

namespace pacman_port.Game.States.LoadingGame
{
    public class LoadingGameState : State
    {
        public LoadingGameState(GameManager gameManager, Type type) : base(gameManager, type)
        {
        }

        public override void DoState()
        {
            Console.WriteLine("DO LOADING GAME");
            GameManager.SetState(GameManager.StateFactory.Get(typeof(GameState)));
        }
    }
}