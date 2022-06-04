using System;
using System.Numerics;
using Raylib_cs;
using steroid_port.Game.Services;
using steroid_port.Game.Systems.Ship;
using steroid_port.Game.Views.Base;

namespace steroid_port.Game.Views
{
    public class ShipView : View
    {
        private Tuple<Rectangle,Texture2D> _textureData;
        private Rectangle _destination;
        private Vector2 _shipCenter;

        public ShipView(RenderService renderService, ScreenService screenService) : base(renderService, screenService)
        {
            
        }
        
        public void Init(SpriteService spriteService)
        {
            _textureData = spriteService.Get("ship");
            _destination = new Rectangle(0, 0, _textureData.Item1.width, _textureData.Item1.height);
            _shipCenter = Vector2.Zero;
        }
        
        public void UpdateView(Vector2 position, int rotation)
        {
            _destination.x = position.X;
            _destination.y = position.Y;
            _shipCenter.X = _destination.width * 0.5f;
            _shipCenter.Y = _destination.height * 0.5f;
            
            RenderService.Render(_textureData.Item2, _textureData.Item1, _destination, _shipCenter, rotation);
        }
    }
}