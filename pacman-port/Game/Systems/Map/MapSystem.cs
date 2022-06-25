using System;
using System.IO;
using common.Core.Services.Render;
using common.Core.Services.Screen;
using pacman_port.Game.Services;
using pacman_port.Game.Views.Map;

namespace pacman_port.Game.Systems.Map
{
    public class MapSystem : PacmanSystem
    {
        private MapView _view;
        private int[,] _mapData;
        
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

        public override void Reset()
        {
            
        }

        public override void Update()
        {
            _view.Update();
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
                    result[i, j] = Int32.Parse(entry[j].ToString());
                }

                i++;
            }

            return result;
        }
    }
}