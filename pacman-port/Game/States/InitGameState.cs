using System;
using common.Core.Managers.Game;
using common.Core.States.Base;

namespace pacman_port.Game.States
{
    public class InitGameState : State
    {
        public InitGameState(GameManager gameManager, Type type) : base(gameManager, type)
        {
        }

        public override void DoState()
        {
            
        }
    }
}