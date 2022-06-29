using System;
using System.Numerics;
using common.Core.Services.Render;
using common.Core.Views.Base;
using pacman_port.Game.Enums;
using pacman_port.Game.Services;
using pacman_port.Game.Services.Sprite;
using pacman_port.Game.Systems.Map;
using Raylib_cs;

namespace pacman_port.Game.Views.Tile
{
    public class TileView : View
    {
        private readonly SpriteService _spriteService;
        private readonly Vector2 _center = Vector2.Zero;
        
        private Tuple<Rectangle,Texture2D> _textureData;
        private Rectangle _destination;
        private MapDataEntry _mapDataEntry;
        private bool isRenderable = true;
        
        public TileView(RenderService renderService, SpriteService spriteService) : base(renderService)
        {
            _spriteService = spriteService;
        }

        public void Init(Vector2 position, MapDataEntry mapDataEntry)
        {
            isRenderable = mapDataEntry.T != TileType.None;
            
            _mapDataEntry = mapDataEntry;

            if (!isRenderable) return;
            
            _textureData = _spriteService.Get((int)_mapDataEntry.T);
            
            _destination = new Rectangle(position.Y*24, position.X*24, _textureData.Item1.width, _textureData.Item1.height);
            
            Bounds = _destination;

        }

        /*
        public void UpdateData(MapDataEntry mapDataEntry)
        {
            
            _mapDataEntry = mapDataEntry;
            _textureData = _spriteService.Get(_mapDataEntry.T);
            _mapSystem.OnUpdate -= OnUpdate;
        }
        */
        public void Update()
        {
            if (!isRenderable) return;
            RenderService.Render(_textureData.Item2, _textureData.Item1, Bounds, _center, 0);
        }

        public void SetData(MapDataEntry mapDataEntry)
        {
            isRenderable = mapDataEntry.T != TileType.None;
        }
    }
}