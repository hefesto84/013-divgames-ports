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
        
        public CollisionSystem(ShipSystem shipSystem, AsteroidsSystem asteroidsSystem, ShotSystem shotSystem)
        {
            _shipSystem = shipSystem;
            _asteroidsSystem = asteroidsSystem;
            _shotSystem = shotSystem;
        }
 
        public override void Update()
        {
            //Console.WriteLine($"Checking collisions between ship {_shipSystem.Ship} and {_shotSystem.Shots.Count} shots and {_asteroidsSystem.Asteroids.Count} asteroids.");
            CheckShipAsteroidCollision();
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
    }
}