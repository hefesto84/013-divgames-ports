using System;
using System.IO;
using System.Numerics;
using Raylib_cs;

namespace steroid.Game
{
    public class MiniShip
    {
        private Texture2D _texture;
        private bool _isReady = false;
        private float _rotation = 0f;
        private int _life = 5;
        
        public MiniShip()
        {
            
        }
        
        public void Init()
        {
            var assetPath = Directory.GetCurrentDirectory();
            
            _texture = Raylib.LoadTexture("steroid.png");

            _isReady = true;


        }
        
        public void Update()
        {
            if (!_isReady) return;

            for (var i = 1; i < _life+1; i++)
            {
                Raylib.DrawTexturePro(_texture, new Rectangle(0,0,33,21), new Rectangle((16*i)+8*(i-1),16,16,10), new Vector2(8, 5), _rotation,Color.WHITE);

            }
            //Raylib.DrawTexture(_texture, 100, 100, Color.WHITE);
            _rotation += 1;
        }

        public void Hit()
        {
            _life--;
        }
    }
}