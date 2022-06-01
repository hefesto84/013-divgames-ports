using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace steroid.Game.Managers
{
    public class AsteroidManager
    {
        private Dictionary<int, Rectangle> _asteroidSpriteData;
        
        private List<Asteroid> _asteroids;
        
        public AsteroidManager()
        {
            _asteroidSpriteData = new Dictionary<int, Rectangle>();
            _asteroidSpriteData.Add(0, new Rectangle(53,0,50,50));
            _asteroidSpriteData.Add(1, new Rectangle(104,0,40,40));
            _asteroidSpriteData.Add(2, new Rectangle(146,0,24,24));
        }

        public void Init()
        {
            Reset();  
            Spawn(4);
        }
        public void HitAsteroid(int id)
        {
            
        }

        private void Reset()
        {
            if (_asteroids == null) _asteroids = new List<Asteroid>();

            for (var i = 0; i < _asteroids.Count; i++)
            {
                _asteroids[i] = null;
            }
            
            _asteroids.Clear();
        }
        
        private void Spawn(int num)
        {
            var r = new Random();

            for (var i = 0; i < num; i++)
            {
                var asteroid = new Asteroid(this);
                var position = new Vector2(r.Next(0, 640), r.Next(0, 480));
                asteroid.Init(position);

                _asteroids.Add(asteroid);
            }
        }

        public List<Asteroid> Asteroids => _asteroids;

        public Dictionary<int, Rectangle> AsteroidSpriteData => _asteroidSpriteData;
    }
}