using Raylib_cs;

namespace steroid_port.Game.Services.Collision
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