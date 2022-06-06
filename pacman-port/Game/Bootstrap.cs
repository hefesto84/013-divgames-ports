using System;
using common.Core.Bootstrap;
using pacman_port.Game.Configurations;
using pacman_port.Game.States;

namespace pacman_port.Game
{
    public class Bootstrap : BaseBootstrap<InitGameState, PacmanConfig>
    {
        public Bootstrap(PacmanConfig config) : base(config)
        {
            
        }

        protected override void InitCustomServices()
        {
            Console.WriteLine("Custom services");
        }

        protected override void BuildCustomSystems()
        {
            Console.WriteLine("Build systems");
        }

        protected override void RegisterCustomStates()
        {
            StateFactory.RegisterState(new InitGameState(GameManager, typeof(InitGameState)));
        }
    }
}