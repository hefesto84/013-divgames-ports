using steroid_port.Game.Services.Render;
using steroid_port.Game.Services.Sprite;
using steroid_port.Game.Views.Background;

namespace steroid_port.Game.Systems.Background
{
    public class BackgroundSystem : Base.System
    {
        private readonly SpriteService _spriteService;
        private readonly RenderService _renderService;

        private BackgroundView _view;
        
        public BackgroundSystem(SpriteService spriteService, RenderService renderService)
        {
            _spriteService = spriteService;
            _renderService = renderService;
        }
        
        public override void Init()
        {
            SetupBackgroundView();
            Reset();
        }

        public override void Update()
        {
            _view.UpdateView();
        }

        private void SetupBackgroundView()
        {
            if (_view != null) return;
            
            _view = new BackgroundView(_renderService);
            _view.Init(_spriteService);
        }
    }
}