using System;
using System.Numerics;
using common.Core.Managers.Game;
using common.Core.Services.Screen;
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
            _fruitSystem.Init();
            _mapSystem.Init();
            _playerSystem.Init();
        }

        public override void DoState()
        {
            _mapSystem.Update();
            
            _playerSystem.Update();
            
            //CheckPosition();
            
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_A))
            {
                _fruitSystem.NextFruit();
            }
            
            _fruitSystem.Update();
        }

        private Vector2 _playerPosition;
        private Vector2 _mapCoordinate;
        private void CheckPosition()
        {
            _playerPosition = _fruitSystem.GetPosition();

            var a = MathF.Ceiling(_playerPosition.X / 24) -1;
            var b = MathF.Ceiling(_playerPosition.Y / 24) - 1;
            
            Console.WriteLine($"{a} {b}");
            //var a =(int) (_mousePosition.X / 12 + _mousePosition.Y / 12) /2;
            //var b =(int) (_mousePosition.Y / 12 - (_mousePosition.X / 12)) / 2;
            //_mapCoordinate.X = MathF.Ceiling(_mousePosition.X / 12 + _mousePosition.Y / 12) / 2;
            //_mapCoordinate.Y = MathF.Ceiling(_mousePosition.Y / 12 - _mousePosition.X / 12) / 2;

            //Console.WriteLine($"{a} {b}");

        }
    }
}