using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using Raylib_cs;
using steroid.Game.Managers;

namespace steroid.Game
{
    public class Asteroid
    {
        private int _life;
        private Texture2D _texture;
        private bool _isReady = false;
        private double _x;
        private double _y;
        private double _velX;
        private double _velY;
        private float _rotation = 0f;
        private Random _r = new Random();
        private int _spriteId;
        private Rectangle _spriteData;
        private Rectangle _bounds;

        private AsteroidManager _asteroidManager;
        
        public Asteroid(AsteroidManager asteroidManager)
        {
            _asteroidManager = asteroidManager;
            _life = 3;
        }

        public void Init(Vector2 position)
        {
            _x = position.X;
            _y = position.Y;
            _texture = Raylib.LoadTexture(Directory.GetCurrentDirectory()+"/steroid.png");
            _velX = _r.Next(1,3);
            _velY = _r.Next(1,3);
            _spriteData = _asteroidManager.AsteroidSpriteData[_r.Next(0, 3)];
            _bounds = new Rectangle();
        }
        
        public void Update()
        {
            // 53,0
            // 50,50
            
            // 40,40

            _bounds.x = (int) _x;
            _bounds.y = (int) _y;
            _bounds.width = _spriteData.width;
            _bounds.height = _spriteData.height;
            
            Raylib.DrawTexturePro(_texture, _spriteData, _bounds, new Vector2(_spriteData.width/2,_spriteData.height/2),_rotation,Color.WHITE);
            //Raylib.DrawTexture(_texture, (int)_position.X, (int)_position.Y, Color.WHITE);
            
            
            Calculate();
            FixPosition();
        }

        private void Calculate()
        {
            _rotation += 1f;
            _x += _velX;
            _y += _velY;
        }

        public void Hit()
        {
            Console.WriteLine("HIT!");
            _life -= 1;
        }

        public Rectangle Bounds => _bounds;

        private void FixPosition()
        {
            if (_y > 480) _y = 0;
            if (_y < 0) _y = 480;
            if (_x < 0) _x = 640;
            if (_x > 640) _x = 0;
        }
        ~Asteroid()
        {
            Console.WriteLine("REMOVING...");
        }
    }
}