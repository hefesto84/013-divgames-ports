using Raylib_cs;

namespace common.Core.Utils
{
    public class Utilities
    {
        public int GetTextWidth(string text, int fontSize)
        {
            return Raylib.MeasureText(text, fontSize);
        }
    }
}