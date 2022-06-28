using System.Numerics;
using common.Core.Services.Render;
using common.Core.Services.Screen;
using pacman_port.Game.Services;
using pacman_port.Game.Views.Fruit;
using Raylib_cs;

namespace pacman_port.Game.Systems.Fruit
{
    public class FruitSystem : PacmanSystem
    {
        private FruitView _view;
        private int _lastFruitIndex = 0;
        private Vector2 _currentPosition;
        public FruitSystem(ScreenService screenService, RenderService renderService, SpriteService spriteService) : 
            base(screenService, renderService, spriteService) { }
        
        public override void Init()
        {
            SetupFruitView();
            Reset();
        }

        private void SetupFruitView()
        {
            if (_view != null) return;
            
            _view = new FruitView(RenderService);
            _view.Init(SpriteService, 3);
        }

        public override void Reset()
        {
            _currentPosition = new Vector2(_view.Bounds.width * 3, _view.Bounds.height * 3);
        }

        public override void Update()
        {
            _view.UpdateView(_currentPosition,0);
        }

        public void NextFruit()
        {
            _lastFruitIndex += 1;
            _lastFruitIndex %= 6;
            _view.SetFruitId(_lastFruitIndex);
        }

        public Vector2 GetPosition()
        {
            return _currentPosition;
        }
    }
}