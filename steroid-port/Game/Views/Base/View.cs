using System.Numerics;
using Raylib_cs;
using steroid_port.Game.Services.Render;
using steroid_port.Game.Services.Screen;

namespace steroid_port.Game.Views.Base
{
    public class View
    {
        public Rectangle Bounds { get; protected set; }

        protected View(RenderService renderService)
        {
            RenderService = renderService;
        }
        
        protected RenderService RenderService { get; }
    }
}