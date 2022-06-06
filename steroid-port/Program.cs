using steroid_port.Game;
using steroid_port.Game.Configurations.Steroid;

namespace steroid_port
{
    class Program
    {
        public static string AppName = "DivGames Steroid port - Raylib 4.0.0";
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