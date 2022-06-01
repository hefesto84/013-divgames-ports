using System;
using steroid.Game;

namespace steroid
{
    class Program
    {
        static void Main(string[] args)
        {
            var steroid = new Steroid();
            
            steroid.Init();

            while (steroid.IsRunning)
            {
                steroid.Update();
            }
        }
    }
}