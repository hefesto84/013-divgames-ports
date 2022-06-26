using System;
using System.IO;
using System.Numerics;
using common.Core.Services.Render;
using common.Core.Services.Screen;
using pacman_port.Game.Services;
using pacman_port.Game.Views.Map;
using Raylib_cs;

namespace pacman_port.Game.Systems.Map
{
    public class MapSystem : PacmanSystem
    {
        private MapView _view;
        private int[,] _mapData;
        private readonly int[] _currentTilePosition = {0, 0};
        
        public MapSystem(ScreenService screenService, RenderService renderService, SpriteService spriteService) : 
            base(screenService, renderService, spriteService) { }
        
        public override void Init()
        {
            _mapData = LoadMapData();
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

        public bool CanMove(Vector2 playerPosition, int radius)
        {
            for (var i = 0; i < _view.TileViews.Count; i++)
            {
                var isCollided = Raylib.CheckCollisionCircleRec(playerPosition, radius, _view.TileViews[i].Bounds);
                if (isCollided) break;
            }
            return true;
        }
        
        private int[,] LoadMapData()
        {
            var result = new int[10, 20];

            var contents = File.ReadAllLines(Directory.GetCurrentDirectory() + "/Resources/map.txt");

            var i = 0;
            
            foreach (var entry in contents)
            {
                for (var j = 0; j < 20; j++)
                {
                    var isValid = Int32.TryParse(entry[j].ToString(), out var v);
                    result[i, j] = isValid ? v : 0;
                }

                i++;
            }

            return result;
        }

        
        private void CheckPosition(Vector2 playerPosition)
        {
            _currentTilePosition[0] = (int)MathF.Ceiling(playerPosition.X / 24) - 1;
            _currentTilePosition[1] = (int)MathF.Ceiling(playerPosition.Y / 24) - 1;
        }
    }
}