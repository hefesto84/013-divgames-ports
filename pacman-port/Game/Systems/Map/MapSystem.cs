﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using common.Core.Services.Render;
using common.Core.Services.Screen;
using pacman_port.Game.Enums;
using pacman_port.Game.Services;
using pacman_port.Game.Services.Sprite;
using pacman_port.Game.Views.Tile;

namespace pacman_port.Game.Systems.Map
{
    public class MapSystem : PacmanSystem
    {
        private TileView[,] _tileViews;
        private MapData _mapData;
        
        private int _column;
        private int _row;
        private int _nextColumn;
        private int _nextRow;

        public int Columns { get; set; }
        public int Rows { get; set; }
        public int MaxBigBalls { get; set; }
        public int MaxMiniBalls { get; set; }
        
        public Action PlayerIsOnMapLimit { get; set; }

        private readonly Dictionary<int, TileType> _tileTypes = new Dictionary<int, TileType>()
        {
            {-1,TileType.None},
            {0, TileType.MiniBall},
            {1, TileType.Wall},
            {2, TileType.BigBall}
        };
        
        public MapSystem(ScreenService screenService, RenderService renderService, SpriteService spriteService) : 
            base(screenService, renderService, spriteService) { }
        
        public override void Init()
        {
            Reset();
            LoadMapData();
            SetupMapView();
        }

        private void SetupMapView()
        {
            _tileViews = new TileView[_mapData.Data.GetLength(0), _mapData.Data.GetLength(1)];
            var k = 0;
            var p = _mapData.Data.GetLength(0); // 26
            var l = _mapData.Data.GetLength(1); // 56
            
            for (var j = 0; j < _mapData.Data.GetLength(1); j++)
            {
                for (var i = 0; i <_mapData.Data.GetLength(0); i++)
                {
                    if (_mapData.Data[i, j].T == TileType.BigBall){ MaxBigBalls++;}
                    if (_mapData.Data[i, j].T == TileType.MiniBall){ MaxMiniBalls++;}

                    k++;
                    
                    _tileViews[i, j] = new TileView(RenderService, SpriteService);
                    _tileViews[i,j].Init(new Vector2(i,j),_mapData.Data[i,j]);
                }
            }
            
            Console.WriteLine("M: "+k); // ESO DA
        }

        public override void Reset()
        {
            MaxBigBalls = 0;
            MaxMiniBalls = 0;
        }

        public override void Update()
        {
            for (var j = 0; j < _mapData.Data.GetLength(1); j++)
            {
                for (var i = 0; i < _mapData.Data.GetLength(0); i++)
                {
                    if (_tileViews[i, j] != null)
                    {
                        _tileViews[i, j].Update();
                    }
                }
            }
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

                
                if (_column < 0 || _column > Columns-1)
                {
                    PlayerIsOnMapLimit?.Invoke();
                    return true;
                }
                
                return _mapData.Data[_row, _column].T != TileType.Wall;
            }

            return true;
        }
        
        
        private void LoadMapData()
        {
            var contents = File.ReadAllLines(Directory.GetCurrentDirectory() + "/Resources/map.txt");
            
            Rows = contents.Length;
            Columns = contents[0].Split(",").Length;

            _mapData = new MapData
            {
                Data = new MapDataEntry[Rows,Columns]
            };

            for (var i = 0; i < Rows; i++)
            {
                var values = contents[i].Split(",");
                
                for (var j = 0; j < values.Length; j++)
                {
                    var isValid = Int32.TryParse(values[j], out var v);
                    _mapData.Data[i, j].X = j;
                    _mapData.Data[i, j].Y = i;
                    _mapData.Data[i, j].T = isValid ? _tileTypes[v] : TileType.MiniBall;
                    if(!isValid) Console.WriteLine("this is null: "+j+"-"+i);
                }
            }
        }

        public TileType Consume(Vector2 currentTile)
        {
            var x = (int) currentTile.X;
            var y = (int) currentTile.Y;
            var result = TileType.None;

            if (x < 0 || x > Columns - 1) return TileType.None;
            
            result = _mapData.Data[y, x].T;
            
            _mapData.Data[y, x].T = TileType.None;
            _tileViews[y, x].SetData(_mapData.Data[y, x]);
            
            Console.WriteLine($"Current Tile: {_mapData.Data[y, x].X},{_mapData.Data[y, x].Y},{_mapData.Data[y, x].T}");

            return result;
        }
    }
}