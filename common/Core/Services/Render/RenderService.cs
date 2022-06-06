using System.Numerics;
using common.Core.Configurations.Base;
using Raylib_cs;

namespace common.Core.Services.Render
{
    public class RenderService
    {
        private readonly Config _config;
        
        public RenderService(Config steroidConfig)
        {
            _config = steroidConfig;
        }

        public void Init()
        {
            Raylib.InitWindow(_config.Width,_config.Height,_config.Name);
            Raylib.SetTargetFPS(_config.Fps);
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