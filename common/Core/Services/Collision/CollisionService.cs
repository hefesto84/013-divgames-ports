using Raylib_cs;

namespace common.Core.Services.Collision
{
    public class CollisionService
    {
        public void Init() { }
        
        public bool AreRectsColliding(Rectangle a, Rectangle b)
        {
            return Raylib.CheckCollisionRecs(a, b);
        }
    }
}