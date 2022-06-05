using System;
using System.Collections.Generic;
using steroid_port.Game.Services.Collision;
using steroid_port.Game.Systems.Asteroids;
using steroid_port.Game.Systems.Ship;
using steroid_port.Game.Systems.Shot;
using steroid_port.Game.Views.Asteroid;
using steroid_port.Game.Views.Ship;
using steroid_port.Game.Views.Shot;

namespace steroid_port.Game.Systems.Collision
{
    public class CollisionSystem : Base.System
    {
        private readonly CollisionService _collisionService;
        private readonly ShipSystem _shipSystem;
        private readonly AsteroidsSystem _asteroidsSystem;
        private readonly ShotSystem _shotSystem;
        
        public Action OnCollision { get; set; }
        public Action<int> OnAsteroidShot { get; set; }
        
        private List<ShotView> _shots;
        private List<AsteroidView> _asteroids;
        private ShipView _ship;
        
        
        public CollisionSystem(CollisionService collisionService, ShipSystem shipSystem, AsteroidsSystem asteroidsSystem, ShotSystem shotSystem)
        {
            _collisionService = collisionService;
            
            _shipSystem = shipSystem;
            _asteroidsSystem = asteroidsSystem;
            _shotSystem = shotSystem;
            
            _shots = new List<ShotView>();
            _asteroids = new List<AsteroidView>();
        }
 
        public override void Update()
        { 
            CheckShipAsteroidCollision();
            CheckShotAsteroidCollision();
        }

        private void CheckShipAsteroidCollision()
        {
            _ship = _shipSystem.Ship;
            _asteroids = _asteroidsSystem.Asteroids;

            // For this condition we can skip the iteration
            if (_asteroids.Count == 0) return;
            
            for (var i = 0; i < _asteroids.Count; i++)
            {
                if (!_collisionService.AreRectsColliding(_asteroids[i].Bounds, _ship.Bounds)) continue;
                OnCollision?.Invoke();
                return;
            }
        }

        private void CheckShotAsteroidCollision()
        {
            _shots = _shotSystem.Shots;
            _asteroids = _asteroidsSystem.Asteroids;

            // For these 2 conditions we can skip the iterations
            if (_shots.Count == 0) return;
            if (_asteroids.Count == 0) return;
            
            for (var i = 0; i < _shots.Count; i++)
            {
                var shotBounds = _shots[i].Bounds;
                
                for (var j = 0; j < _asteroids.Count; j++)
                {
                    if (_collisionService.AreRectsColliding(shotBounds, _asteroids[j].Bounds))
                    {
                        OnAsteroidShot?.Invoke(j);
                    }    
                }
            }
        }
    }
}