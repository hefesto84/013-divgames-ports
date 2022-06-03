using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using steroid_port.Game.Services;
using steroid_port.Game.Views.Base;

namespace steroid_port.Game.Views
{
    public class LifesView : View
    {
        private Tuple<Rectangle,Texture2D> _textureData;
        private int _rotation;
        private Vector2 _centerOfView = Vector2.Zero;
        private Vector2 _finalSizeOfView = Vector2.Zero;
        private float _initialY = 0;
        private int _currentLifes = 0;

        private LifeViewData[] _livesViewData;
        
        public LifesView(RenderService renderService) : base(renderService)
        {
            
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
            _livesViewData = new LifeViewData[3];
            
            _finalSizeOfView.X = _textureData.Item1.width / 2;
            _finalSizeOfView.Y = _textureData.Item1.height / 2;
            _centerOfView.X = _finalSizeOfView.X/2;
            _centerOfView.Y = _finalSizeOfView.Y/2;
            _initialY = _finalSizeOfView.Y*1.5f;

            for (var i = 0; i < 3; i++)
            {
                _livesViewData[i] = new LifeViewData
                {
                    Center = _centerOfView,
                    From = _textureData.Item1,
                    To = new Rectangle(i*_textureData.Item1.width - 5*i, _initialY, _finalSizeOfView.X, _finalSizeOfView.Y)
                };
            }
        }
        
        public void UpdateView(int lifes)
        {
            _rotation += 1;
            
            for (var i = 1; i < lifes+1; i++)
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