using System;
using steroid_port.Game;
using steroid_port.Game.Configurations;
using steroid_port.Game.States.InitGame;

namespace steroid_port
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var config = new SteroidConfig("Resources/properties.ini");
            
            var bootstrap = new Bootstrap(config);
            
            bootstrap.Init();

            
            while (!bootstrap.IsQuit)
            {
                bootstrap.Update();
            }
        }
    }
}