﻿using common.Core.Managers.Game;
using common.Core.States.Base;
using steroid_port.Game.States.Cleared;
using steroid_port.Game.States.GameOver;
using steroid_port.Game.Systems.Asteroids;
using steroid_port.Game.Systems.Background;
using steroid_port.Game.Systems.Collision;
using steroid_port.Game.Systems.Game;
using steroid_port.Game.Systems.Ship;
using steroid_port.Game.Systems.Shot;
using steroid_port.Game.Systems.UI;

namespace steroid_port.Game.States.Game
{
    public class GameState : State
    {
        private readonly ShipSystem _shipSystem;
        private readonly AsteroidsSystem _asteroidsSystem;
        private readonly ShotSystem _shotSystem;
        private readonly CollisionSystem _collisionSystem;
        private readonly BackgroundSystem _backgroundSystem;
        private readonly UISystem _uiSystem;
        private readonly GameSystem _gameSystem;
        
        public GameState(
            GameManager gameManager, 
            BackgroundSystem backgroundSystem, 
            ShipSystem shipSystem, 
            AsteroidsSystem asteroidsSystem, 
            ShotSystem shotSystem,
            CollisionSystem collisionSystem,
            UISystem uiSystem, 
            GameSystem gameSystem) : base(gameManager, typeof(GameState))
        {
            _backgroundSystem = backgroundSystem;
            _shipSystem = shipSystem;
            _shotSystem = shotSystem;
            _asteroidsSystem = asteroidsSystem;
            _collisionSystem = collisionSystem;
            _uiSystem = uiSystem;
            _gameSystem = gameSystem;
        }

        public override void Start()
        {
            _backgroundSystem.Init();
            _shipSystem.Init();
            _shotSystem.Init();
            _asteroidsSystem.Init();
            _collisionSystem.Init();
            _uiSystem.Init();
            _gameSystem.Init();
            
            _gameSystem.OnGameOver += OnGameOver;
            _gameSystem.OnGameCleared += OnGameCleared;
            
            _uiSystem.SetState(this);
        }

        public override void DoState()
        {
            _gameSystem.Update();
            
            _backgroundSystem.Update();
            _shipSystem.Update();
            _asteroidsSystem.Update();
            _shotSystem.Update();
            
            _collisionSystem.Update();
            
            _uiSystem.Update();
        }

        public override void Stop()
        {
            _gameSystem.OnGameOver -= OnGameOver;
            _gameSystem.OnGameCleared -= OnGameCleared;
        }
        
        private void OnGameOver()
        {
            GameManager.SetState(GameManager.StateFactory.Get(typeof(GameOverState)));
        }

        private void OnGameCleared()
        {
            _gameSystem.Reset();
            GameManager.SetState(GameManager.StateFactory.Get(typeof(ClearedState)));
        }
    }
}