using System;
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
        
        public CollisionSystem(ShipSystem shipSystem, AsteroidsSystem asteroidsSystem, ShotSystem shotSystem)
        {
            _shipSystem = shipSystem;
            _asteroidsSystem = asteroidsSystem;
            _shotSystem = shotSystem;
        }
        
        public override void Init()
        {
            base.Init();
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update()
        {
            Console.WriteLine("Checking collisions");
        }
    }
}