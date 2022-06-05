using System;
using System.Numerics;
using Raylib_cs;
using steroid_port.Game.Services.Render;
using steroid_port.Game.Services.Sprite;
using steroid_port.Game.Views.Base;

namespace steroid_port.Game.Views.Background
{
    public class BackgroundView : View
    {
        private Tuple<Rectangle,Texture2D> _textureData;
        private Rectangle _destination;
        private Vector2 _backgroundCenter;
        
        public BackgroundView(RenderService renderService) : base(renderService) { }
        
        public void Init(SpriteService spriteService)
        {
            _textureData = spriteService.Get("background");
            _destination = new Rectangle(0, 0, _textureData.Item1.width, _textureData.Item1.height);
            _backgroundCenter = Vector2.Zero;
            
            Bounds = _destination;
        }
        
        public void UpdateView()
        {
            _backgroundCenter.X = 0;
            _backgroundCenter.Y = 0;
            
            Bounds = _destination;
            
            RenderService.Render(_textureData.Item2, _textureData.Item1, Bounds, _backgroundCenter, 0);
        }
    }
}