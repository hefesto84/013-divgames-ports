using System;
using System.Collections.Generic;
using System.Numerics;
using common.Core.Services.Render;
using common.Core.Views.Base;
using pacman_port.Game.Services;
using pacman_port.Game.Views.Tile;

namespace pacman_port.Game.Views.Map
{
    public class MapView : View
    {
        private readonly SpriteService _spriteService;
        
        public Action OnUpdate { get; set; }
        public List<TileView> TileViews { get; private set; }
        
        public MapView(RenderService renderService, SpriteService spriteService) : base(renderService)
        {
            _spriteService = spriteService;
        }

        public void Init(int[,] mapData)
        {
            TileViews = new List<TileView>();

            for (var i = 0; i <mapData.GetLength(0); i++)
            {
                for (var j = 0; j < mapData.GetLength(1); j++)
                {
                    if (mapData[i, j] == 1)
                    {
                        TileViews.Add(new TileView(RenderService, _spriteService));
                        TileViews[^1].Init(new Vector2(24*i,24*j), "test-tile-blocked", this);
                    }
                }
            }
        }
        
        public void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}