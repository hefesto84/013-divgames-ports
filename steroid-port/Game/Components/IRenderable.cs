using Raylib_cs;

namespace steroid_port.Game.Components
{
    public interface IRenderable
    {
        Rectangle Bounds { get; set; }
        Texture2D Texture2D { get; set; }
    }
}