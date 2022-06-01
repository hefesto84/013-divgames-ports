using System;
using System.IO;
using System.Numerics;
using Raylib_cs;

namespace steroid.Game
{
    public class Ship
    {
        private Texture2D _texture;
        private bool _isReady = false;
        private float _rotation = 90f;
        private double _x;
        private double _y;
        private double _velX;
        private double _velY;
        private Rectangle _bounds;
        private int _life = 3;
        
        public void Init()
        {
            _texture = Raylib.LoadTexture(Directory.GetCurrentDirectory()+"/steroid.png");

            _x = 320;
            _y = 240;
            
            _isReady = true;
            _bounds = new Rectangle();
        }
        
        public void Update()
        {
            if (!_isReady) return;


            _bounds.x = (int) _x;
            _bounds.y = (int) _y;
            _bounds.width = 33;
            _bounds.height = 21;
            
            Raylib.DrawTexturePro(_texture, new Rectangle(0,0,_bounds.width,_bounds.height), _bounds, new Vector2(_bounds.width/2, _bounds.height/2), _rotation,Color.WHITE);

            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)) _rotation += 1f;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) _rotation -= 1f;

            Calculate();
            FixPosition();
        }

        
        private void Calculate()
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP))
            {
                var rads = _rotation * (Math.PI / 180);

                var xVector = Math.Sin(rads);
                var yVector = Math.Cos(rads);

                var magnitude = Math.Sqrt(xVector * xVector + yVector * yVector);
                _velX = xVector / magnitude;
                _velY = yVector / magnitude;
            }
            
            var distanceToTravel = 1;
            _x += _velX* distanceToTravel;
            _y += _velY * distanceToTravel;
            _bounds.x = (int) _x;
            _bounds.y = (int) _y;
        }

        public Rectangle Bounds => _bounds;

        public void Hit()
        {
            _life -= 1;
            _x = 320;
            _y = 240;
        }
        
        private void FixPosition()
        {
            if (_y > 480) _y = 0;
            if (_y < 0) _y = 480;
            if (_x < 0) _x = 640;
            if (_x > 640) _x = 0;
        }
    }
}