using System;
using System.Numerics;
using common.Core.Services.Render;
using common.Core.Views.Base;
using Raylib_cs;
using steroid_port.Game.Services.Game;
using steroid_port.Game.Services.Sprite;

namespace steroid_port.Game.Views.Lives
{
    public class LivesView : View
    {
        private Tuple<Rectangle,Texture2D> _textureData;
        private int _rotation;
        private Vector2 _centerOfView = Vector2.Zero;
        private Vector2 _finalSizeOfView = Vector2.Zero;
        private float _initialY = 0;
        private LifeViewData[] _livesViewData;
        private readonly GameService _gameService;
        
        public LivesView(RenderService renderService, GameService gameService) : base(renderService)
        {
            _gameService = gameService;
        }
        
        public void Init(SpriteService spriteService)
        {
            _textureData = spriteService.Get("ship");
            _finalSizeOfView.X = _textureData.Item1.width / 2;
            _finalSizeOfView.Y = _textureData.Item1.height / 2;
            _centerOfView.X = _finalSizeOfView.X/2;
            _centerOfView.Y = _finalSizeOfView.Y/2;
            _initialY = _finalSizeOfView.Y*1.5f;
            Reset();
        }

        public void Reset()
        {
            _livesViewData = new LifeViewData[_gameService.MaxLives];
            
            _finalSizeOfView.X = _textureData.Item1.width / 1.5f;
            _finalSizeOfView.Y = _textureData.Item1.height / 1.5f;
            _centerOfView.X = _finalSizeOfView.X/1.5f;
            _centerOfView.Y = _finalSizeOfView.Y/1.5f;
            _initialY = _finalSizeOfView.Y*1.5f;

            for (var i = 0; i < _gameService.MaxLives; i++)
            {
                _livesViewData[i] = new LifeViewData
                {
                    Center = _centerOfView,
                    From = _textureData.Item1,
                    To = new Rectangle((i+1)*_textureData.Item1.width, _initialY, _finalSizeOfView.X, _finalSizeOfView.Y)
                };
            }
        }
        
        public void UpdateView()
        {
            _rotation += 1;
            
            for (var i = 1; i < _gameService.CurrentLives+1; i++)
            {
                RenderService.Render(_textureData.Item2, _livesViewData[i-1].From, _livesViewData[i-1].To, _livesViewData[i-1].Center, _rotation);
            }
        }
    }

    public struct LifeViewData
    {
        public Rectangle From;
        public Rectangle To;
        public Vector2 Center;
    }
}