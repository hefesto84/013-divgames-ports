using Raylib_cs;

namespace steroid_port.Game.Utils
{
    public class Utilities
    {
        public int GetTextWidth(string text, int fontSize)
        {
            return Raylib.MeasureText(text, fontSize);
        }
    }
}