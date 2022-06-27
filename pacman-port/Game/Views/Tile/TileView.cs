using System;
using System.Numerics;
using common.Core.Services.Render;
using common.Core.Views.Base;
using pacman_port.Game.Services;
using pacman_port.Game.Views.Map;
using Raylib_cs;

namespace pacman_port.Game.Views.Tile
{
    public class TileView : View
    {
        private SpriteService _spriteService;
        private Tuple<Rectangle,Texture2D> _textureData;
        private Rectangle _destination;
        private Vector2 _center = Vector2.Zero;
        private MapView _mapView;
        
        public TileView(RenderService renderService, SpriteService spriteService) : base(renderService)
        {
            _spriteService = spriteService;
        }

        public void Init(Vector2 position, int id, MapView mapView)
        {
            _textureData = _spriteService.Get(id);
            _destination = new Rectangle(position.Y*24, position.X*24, _textureData.Item1.width, _textureData.Item1.height);
            
            Bounds = _destination;

            mapView.OnUpdate += OnUpdate;
        }

        private void OnUpdate()
        {
            RenderService.Render(_textureData.Item2, _textureData.Item1, Bounds, _center, 0);
            
        }

        ~TileView()
        {
            _mapView.OnUpdate -= OnUpdate;
        }
    }
}