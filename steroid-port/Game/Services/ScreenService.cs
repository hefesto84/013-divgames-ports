using System.Numerics;
using Raylib_cs;

namespace steroid_port.Game.Services
{
    public class ScreenService
    {
        public Vector3 CurrentSize { get; private set; }
        public Vector2 CurrentScreenCenter { get; private set; }
        
        public void Init(int width, int height)
        {
            CurrentSize = new Vector3(width, height,0);
            CurrentScreenCenter = new Vector2(width / 2, height / 2);
        }
    }
}