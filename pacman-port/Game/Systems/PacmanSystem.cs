using common.Core.Services.Render;
using common.Core.Services.Screen;
using pacman_port.Game.Services;

namespace pacman_port.Game.Systems
{
    public class PacmanSystem : common.Core.Systems.Base.System
    {
        protected readonly ScreenService ScreenService;
        protected readonly RenderService RenderService;
        protected readonly SpriteService SpriteService;

        protected PacmanSystem(ScreenService screenService, RenderService renderService, SpriteService spriteService)
        {
            ScreenService = screenService;
            RenderService = renderService;
            SpriteService = spriteService;
        }
    }
}