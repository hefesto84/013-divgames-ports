using System.Numerics;

namespace common.Core.Services.Screen
{
    public class ScreenService
    {
        public Vector2 CurrentSize { get; private set; }
        public Vector2 CurrentScreenCenter { get; private set; }
        
        public void Init(int width, int height)
        {
            CurrentSize = new Vector2(width, height);
            CurrentScreenCenter = new Vector2(width / 2, height / 2);
        }
    }
}