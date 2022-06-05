using System.Numerics;
using Raylib_cs;
using steroid_port.Game.Configurations.Steroid;

namespace steroid_port.Game.Services.Render
{
    public class RenderService
    {
        private readonly SteroidConfig _steroidConfig;
        
        public RenderService(SteroidConfig steroidConfig)
        {
            _steroidConfig = steroidConfig;
        }

        public void Init()
        {
            Raylib.InitWindow(_steroidConfig.Width,_steroidConfig.Height,_steroidConfig.Name);
            Raylib.SetTargetFPS(_steroidConfig.Fps);
        }

        public void Begin()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
        }

        public void End()
        {
            Raylib.EndDrawing();
        }

        public void Render(Texture2D texture, Rectangle from)
        {
            Raylib.DrawTexture(texture, (int)from.x, (int)from.y, Color.WHITE);    
        }
 
        public void Render(Texture2D texture, Rectangle from, Rectangle to, Vector2 center, int rotation)
        {
            Raylib.DrawTexturePro(texture, from,to, center,
                rotation, Color.WHITE);
        }

        public void RenderText(string text, int x,int y, int fontSize, Color color)
        {
            Raylib.DrawText(text,x,y,fontSize,color);
        }
    }
}