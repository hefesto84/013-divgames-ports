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

        public ShipView(RenderService renderService) : base(renderService)
        {
        }
        
        public void Init(SpriteService spriteService)
        {
            _textureData = spriteService.Get("ship");
            _destination = new Rectangle(0, 0, _textureData.Item1.width, _textureData.Item1.height);
        }
        
        public void UpdateView(Vector2 position, int rotation)
        {
            Console.WriteLine("RO: "+rotation);
            
            _destination.x = position.X;
            _destination.y = position.Y;
            
            RenderService.Render(_textureData.Item2, _textureData.Item1, _destination, new Vector2(_destination.width/2, _destination.height/2), rotation);
            //RenderService.Render(_textureData.Item2, _textureData.Item1, new Rectangle(position.X,position.Y,_textureData.Item1.width, _textureData.Item1.height));
        }
    }
}