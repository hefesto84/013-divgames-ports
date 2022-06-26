using System;
using System.IO;
using System.Numerics;
using common.Core.Services.Render;
using common.Core.Services.Screen;
using pacman_port.Game.Enums;
using pacman_port.Game.Services;
using pacman_port.Game.Views.Map;

namespace pacman_port.Game.Systems.Map
{
    public class MapSystem : PacmanSystem
    {
        private MapView _view;
        private int[,] _mapData;
        private int _column;
        private int _row;
        private int _nextColumn;
        private int _nextRow;
        
        public int Columns { get; private set; }
        public int Rows { get; private set; }
        
        public MapSystem(ScreenService screenService, RenderService renderService, SpriteService spriteService) : 
            base(screenService, renderService, spriteService) { }
        
        public override void Init()
        {
            LoadMapData();
            
            SetupMapView();
            Reset();
        }

        private void SetupMapView()
        {
            if (_view != null) return;
            
            _view = new MapView(RenderService, SpriteService);
            _view.Init(_mapData);
        }

        public override void Reset() { }

        public override void Update()
        {
            _view.Update();
        }
        
        public bool CanMove(Vector2 playerPosition, MovementDirection movementDirection)
        {
            if (playerPosition.X % 24 == 0 && playerPosition.Y % 24 == 0)
            {
                switch (movementDirection)
                {
                    case MovementDirection.Right:
                        _nextColumn = (int)playerPosition.X + 24;
                        _column = _nextColumn / 24;
                        _row = (int) playerPosition.Y / 24;
                        break;
                    case MovementDirection.Left:
                        _nextColumn = (int)playerPosition.X - 24;
                        _column = _nextColumn / 24;
                        _row = (int) playerPosition.Y / 24;
                        break;
                    case MovementDirection.Down:
                        _nextRow = (int)playerPosition.Y + 24;
                        _row = _nextRow / 24;
                        _column = (int) playerPosition.X / 24;
                        break;
                    case MovementDirection.Up:
                        _nextRow = (int)playerPosition.Y - 24;
                        _row = _nextRow / 24;
                        _column = (int) playerPosition.X / 24;
                        break;
                }
                
                return _mapData[_row, _column] != 1;
            }

            return true;
        }
        
        
        private void LoadMapData()
        {
            var contents = File.ReadAllLines(Directory.GetCurrentDirectory() + "/Resources/map.txt");
            
            Rows = contents.Length;
            Columns = contents[0].Length;

            _mapData = new int[Rows, Columns];

            for (var i = 0; i < Rows; i++)
            {
                var values = contents[i].ToCharArray();
                
                for (var j = 0; j < values.Length; j++)
                {
                    var isValid = Int32.TryParse(values[j].ToString(), out var v);
                    _mapData[i, j] = isValid ? v : 0;
                }
            }
        }
    }
}