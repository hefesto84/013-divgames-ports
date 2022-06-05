using System;
using Raylib_cs;
using steroid_port.Game.Systems.Asteroids;
using steroid_port.Game.Systems.Ship;
using steroid_port.Game.Systems.Shot;

namespace steroid_port.Game.Systems.Collision
{
    public class CollisionSystem : Base.System
    {
        private readonly ShipSystem _shipSystem;
        private readonly AsteroidsSystem _asteroidsSystem;
        private readonly ShotSystem _shotSystem;
        
        public Action OnCollision { get; set; }
        public Action<int> OnAsteroidShot { get; set; }
        
        public CollisionSystem(ShipSystem shipSystem, AsteroidsSystem asteroidsSystem, ShotSystem shotSystem)
        {
            _shipSystem = shipSystem;
            _asteroidsSystem = asteroidsSystem;
            _shotSystem = shotSystem;
        }
 
        public override void Update()
        { 
            CheckShipAsteroidCollision();
            CheckShotAsteroidCollision();
        }

        private void CheckShipAsteroidCollision()
        {
            var ship = _shipSystem.Ship;
            var asteroids = _asteroidsSystem.Asteroids;
            
            for (var i = 0; i < asteroids.Count; i++)
            {
                if (Raylib.CheckCollisionRecs(asteroids[i].Bounds, ship.Bounds))
                {
                    OnCollision?.Invoke();
                }    
            }
        }

        private void CheckShotAsteroidCollision()
        {
            var shots = _shotSystem.Shots;
            var asteroids = _asteroidsSystem.Asteroids;

            for (var i = 0; i < shots.Count; i++)
            {
                var shotBounds = shots[i].Bounds;
                
                for (var j = 0; j < asteroids.Count; j++)
                {
                    if (Raylib.CheckCollisionRecs(shotBounds, asteroids[j].Bounds))
                    {
                        OnAsteroidShot?.Invoke(j);
                    }    
                }
            }
        }
    }
}