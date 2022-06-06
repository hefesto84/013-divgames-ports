using System;
using pacman_port.Game;
using pacman_port.Game.Configurations;

namespace pacman_port
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new PacmanConfig("Resources/properties.ini");
            
            var bootstrap = new Bootstrap(config);
            
            bootstrap.Init();

            while (!bootstrap.IsQuit)
            {
                bootstrap.Update();
            }
        }
    }
}