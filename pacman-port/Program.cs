using pacman_port.Game;
using pacman_port.Game.Configurations;
using Raylib_cs;

namespace pacman_port
{
    // https://www.youtube.com/watch?v=IL9Kbcwys1A
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
                Raylib.DrawFPS(0,0);
            }
        }
    }
}