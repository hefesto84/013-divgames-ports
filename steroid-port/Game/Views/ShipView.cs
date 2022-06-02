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

        public ShipView(RenderService renderService) : base(renderService)
        {
        }
        
        public void Init(SpriteService spriteService)
        {
            _textureData = spriteService.Get("ship");
        }
        
        public void UpdateView(Vector3 position)
        {
            RenderService.Render(_textureData.Item2, _textureData.Item1, new Rectangle(position.X,position.Y,_textureData.Item1.width, _textureData.Item1.height));
        }
    }
}