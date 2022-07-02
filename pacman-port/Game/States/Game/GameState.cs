using System;
using common.Core.Managers.Game;
using common.Core.States.Base;
using pacman_port.Game.Services.Game;
using pacman_port.Game.States.InitGame;
using pacman_port.Game.Systems.Consumer;
using pacman_port.Game.Systems.Fruit;
using pacman_port.Game.Systems.Map;
using pacman_port.Game.Systems.Player;
using pacman_port.Game.Systems.UI;

namespace pacman_port.Game.States.Game
{
    public class GameState : State
    {
        private readonly FruitSystem _fruitSystem;
        private readonly UISystem _uiSystem;
        private readonly MapSystem _mapSystem;
        private readonly PlayerSystem _playerSystem;
        private readonly ConsumerSystem _consumerSystem;
        private readonly GameService _gameService;
        
        private bool _isMiniBallConsumed;
        private bool _isBigBallConsumed;

        public GameState(GameManager gameManager, GameService gameService, UISystem uiSystem, FruitSystem fruitSystem,
            MapSystem mapSystem,
            PlayerSystem playerSystem, ConsumerSystem consumerSystem) : base(gameManager, typeof(GameState))
        {
            _gameService = gameService;
            _uiSystem = uiSystem;
            _fruitSystem = fruitSystem;
            _mapSystem = mapSystem;
            _playerSystem = playerSystem;
            _consumerSystem = consumerSystem;
        }

        public override void Start()
        {
            //_fruitSystem.Init();
            
            _gameService.Reset();
            
            _mapSystem.Init();
            _playerSystem.Init(_mapSystem);
            _consumerSystem.Init();
            _uiSystem.Init();
            
            _consumerSystem.OnBigBallConsumed += OnBigBallConsumed;
            _consumerSystem.OnMiniBallConsumed += OnMiniBallConsumed;
        }

        public override void Stop()
        {
            _consumerSystem.OnBigBallConsumed -= OnBigBallConsumed;
            _consumerSystem.OnMiniBallConsumed -= OnMiniBallConsumed;
        }

        public override void DoState()
        {
            _mapSystem.Update();
            
            _playerSystem.Update();
            
            _consumerSystem.Update();

            if (_isBigBallConsumed && _isMiniBallConsumed)
            {
                _isBigBallConsumed = false;
                _isMiniBallConsumed = false;
                GameManager.SetState(GameManager.StateFactory.Get(typeof(InitGameState)));
                
            }
            
            _uiSystem.Update();
            /*
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_A))
            {
                _fruitSystem.NextFruit();
            }
            
            _fruitSystem.Update();
            */
        }

        private void OnMiniBallConsumed(bool isLast)
        {
            _isMiniBallConsumed = isLast;
        }

        private void OnBigBallConsumed(bool isLast)
        {
            _isBigBallConsumed = isLast;
        }
    }
}