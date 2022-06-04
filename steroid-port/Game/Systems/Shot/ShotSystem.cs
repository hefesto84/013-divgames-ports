using System;
using Raylib_cs;
using steroid_port.Game.Services;

namespace steroid_port.Game.Systems.Shot
{
    public class ShotSystem : Base.System
    {
        private ScreenService _screenService;
        private SpriteService _spriteService;
        private RenderService _renderService;
        
        public ShotSystem(ScreenService screenService, SpriteService spriteService, RenderService renderService)
        {
            _screenService = screenService;
            _spriteService = spriteService;
            _renderService = renderService;
        }

        public override void Init()
        {
            base.Init();
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_F))
            {
                Console.WriteLine("SHOT!");
            }
        }
    }
}