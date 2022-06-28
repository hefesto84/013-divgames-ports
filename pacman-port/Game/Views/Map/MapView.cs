using System;
using System.Collections.Generic;
using System.Numerics;
using common.Core.Services.Render;
using common.Core.Views.Base;
using pacman_port.Game.Services;
using pacman_port.Game.Systems.Map;
using pacman_port.Game.Views.Tile;

namespace pacman_port.Game.Views.Map
{
    public class MapView : View
    {
        private readonly SpriteService _spriteService;
        //private int[,] _mapData;
        private MapData _mapData;
        
        public Action OnUpdate { get; set; }

        public TileView[,] TileViews;
        
        public MapView(RenderService renderService, SpriteService spriteService) : base(renderService)
        {
            _spriteService = spriteService;
        }

        public void Init(MapData mapData)
        {
            _mapData = mapData;

            TileViews = new TileView[_mapData.Data.GetLength(0), _mapData.Data.GetLength(1)];

            //var k = 0;
            
            for (var i = 0; i <_mapData.Data.GetLength(0); i++)
            {
                for (var j = 0; j < _mapData.Data.GetLength(1); j++)
                {
                    if(_mapData.Data[i,j].T == -1) continue;

                    TileViews[i, j] = new TileView(RenderService, _spriteService);
                    TileViews[i,j].Init(new Vector2(i,j),ref _mapData.Data[i,j],this);
                }
            }
        }

        public void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}