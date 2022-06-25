using System;
using common.Core.Managers.Game;
using common.Core.States.Base;
using pacman_port.Game.States.IntroGame;

namespace pacman_port.Game.States.InitGame
{
    public class InitGameState : State
    {
        public InitGameState(GameManager gameManager, Type type) : base(gameManager, type)
        {
        }

        public override void DoState()
        {
            Console.WriteLine("DO INIT");
            GameManager.SetState(GameManager.StateFactory.Get(typeof(IntroGameState)));
        }
    }
}