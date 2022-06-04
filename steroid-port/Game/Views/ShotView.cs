using System;
using System.Numerics;
using Raylib_cs;
using steroid_port.Game.Services;
using steroid_port.Game.Views.Base;

namespace steroid_port.Game.Views
{
    public class ShotView : View
    {
        private Tuple<Rectangle,Texture2D> _textureData;
        private Rectangle _destination;
        private Vector2 _shotCenter;
        private Vector2 _position;
        private Vector2 _velocity = Vector2.Zero;
        private int _rotation;
        
        public ShotView(RenderService renderService) : base(renderService)
        {
            
        }
        
        public void Init(SpriteService spriteService)
        {
            _textureData = spriteService.Get("shot");
            _destination = new Rectangle(0, 0, _textureData.Item1.width, _textureData.Item1.height);
            _shotCenter = Vector2.Zero;
        }

        public void SetView(Vector2 position, Vector2 velocity, int rotation)
        {
            _rotation = rotation;
            //_position = position;

            var rads = rotation * MathF.PI / 180;
            _position.X = position.X + 10 * MathF.Cos(rads);
            _position.Y = position.Y + 10 * MathF.Sin(rads);
            _velocity.X = 5 * MathF.Cos(rads) + velocity.X;
            _velocity.Y = 5 * MathF.Sin(rads) + velocity.Y;
        }
        public void UpdateView()
        {
            _position += _velocity;
            
            _destination.x = _position.X;
            _destination.y = _position.Y;
            _shotCenter.X = _destination.width * 0.5f;
            _shotCenter.Y = _destination.height * 0.5f;
            
            RenderService.Render(_textureData.Item2, _textureData.Item1, _destination, _shotCenter, _rotation);
        }
    }
}