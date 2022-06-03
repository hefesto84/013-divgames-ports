using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using steroid_port.Game.Configurations;
using steroid_port.Game.Views;
using steroid_port.Game.Views.Base;

namespace steroid_port.Game.Services
{
    public class RenderService
    {
        private readonly SteroidConfig _steroidConfig;
        private List<View> _views;
        
        public RenderService(SteroidConfig steroidConfig)
        {
            _steroidConfig = steroidConfig;
        }

        public void Init()
        {
            Raylib.InitWindow(_steroidConfig.Width,_steroidConfig.Height,_steroidConfig.Name);
            Raylib.SetTargetFPS(_steroidConfig.Fps);
            _views = new List<View>();
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
        
        public void Render(Texture2D texture, Rectangle from, Rectangle to)
        {
            Raylib.DrawTexturePro(texture, from, to, new Vector2(0,0), 0, Color.WHITE);
        }

        public void Render(Texture2D texture, Rectangle from, Rectangle to, Vector2 center, int rotation)
        {
            Raylib.DrawTexturePro(texture, from,to, center,
                rotation, Color.WHITE);
        }
    }
}