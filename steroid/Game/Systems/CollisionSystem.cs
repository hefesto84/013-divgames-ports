using System;
using Raylib_cs;
using steroid.Game.Managers;

namespace steroid.Game.Systems
{
    public class CollisionSystem
    {
        private AsteroidManager _asteroidManager;
        private Ship _ship;
        private bool _isReady = false;
        
        public Action OnCollision { get; set; }
        
        public CollisionSystem(AsteroidManager asteroidManager)
        {
            _asteroidManager = asteroidManager;
        }

        public void Init(Ship ship)
        {
            _isReady = true;
            _ship = ship;
        }
        
        public void Update()
        {
            if (!_isReady) return;
            
            _asteroidManager.Asteroids.ForEach(asteroid =>
            {
                if (Raylib.CheckCollisionRecs(asteroid.Bounds, _ship.Bounds))
                {
                    OnCollision?.Invoke();
                }
            });
        }
    }
}