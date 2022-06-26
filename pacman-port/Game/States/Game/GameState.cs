using common.Core.Managers.Game;
using common.Core.States.Base;
using pacman_port.Game.Systems.Fruit;
using pacman_port.Game.Systems.Map;
using pacman_port.Game.Systems.Player;
using Raylib_cs;

namespace pacman_port.Game.States.Game
{
    public class GameState : State
    {
        private readonly FruitSystem _fruitSystem;
        private readonly MapSystem _mapSystem;
        private readonly PlayerSystem _playerSystem;
        
        public GameState(GameManager gameManager, FruitSystem fruitSystem, MapSystem mapSystem,
            PlayerSystem playerSystem) : base(gameManager, typeof(GameState))
        {
            _fruitSystem = fruitSystem;
            _mapSystem = mapSystem;
            _playerSystem = playerSystem;
        }

        public override void Start()
        {
            //_fruitSystem.Init();
            _mapSystem.Init();
            _playerSystem.Init(_mapSystem);
        }

        public override void DoState()
        {
            _mapSystem.Update();
            
            _playerSystem.Update();
            
            /*
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_A))
            {
                _fruitSystem.NextFruit();
            }
            
            _fruitSystem.Update();
            */
        }
    }
}