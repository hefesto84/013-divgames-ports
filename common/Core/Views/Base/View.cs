using common.Core.Services.Render;
using Raylib_cs;

namespace common.Core.Views.Base
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