﻿using System;
using System.Numerics;
using pacman_port.Game.Enums;
using pacman_port.Game.Services.Game;
using pacman_port.Game.Systems.Map;
using pacman_port.Game.Systems.Player;

namespace pacman_port.Game.Systems.Consumer
{
    public class ConsumerSystem : common.Core.Systems.Base.System
    {
        private readonly GameService _gameService;
        private readonly PlayerSystem _playerSystem;
        private readonly MapSystem _mapSystem;
        public Action<bool> OnBigBallConsumed { get; set; }
        public Action<bool> OnMiniBallConsumed { get; set; }
        private Vector2 _lastTile = new Vector2(-1, -1);
        private Vector2 _currentTile = Vector2.Zero;
        private bool _isReady;
        
        private int _currentBigBalls = 0;
        private int _currentMiniBalls = 0;
        private int _maxBigBalls = 0;
        private int _maxMiniBalls = 0;
        
        public ConsumerSystem(GameService gameService, PlayerSystem playerSystem, MapSystem mapSystem)
        {
            _gameService = gameService;
            _playerSystem = playerSystem;
            _mapSystem = mapSystem;
        }

        public override void Init()
        {
           Reset();
        }

        public override void Reset()
        {
            _isReady = true;
            _lastTile = new Vector2(-1, -1);
            _currentTile = Vector2.Zero;
            
            _maxBigBalls = _mapSystem.MaxBigBalls;
            _maxMiniBalls = _mapSystem.MaxMiniBalls;
            _currentBigBalls = 0;
            _currentMiniBalls = 0;
            
        }

        public override void Update()
        {
            if (!_isReady) return;

            Consume();
        }

        private void Consume()
        {
            var ct = _playerSystem.GetCurrentTile();

            _currentTile.X = ct[0];
            _currentTile.Y = ct[1];
            
            if (_lastTile == _currentTile) return;

            _lastTile = _currentTile;

            var result = _mapSystem.Consume(_currentTile);

            switch (result)
            {
                case TileType.MiniBall:
                    _currentMiniBalls++;
                    OnMiniBallConsumed?.Invoke(_currentMiniBalls == _maxMiniBalls);
                    break;
                case TileType.BigBall:
                    _currentBigBalls++;
                    OnBigBallConsumed?.Invoke(_currentBigBalls == _maxBigBalls);
                    break;
            }
            
            _gameService.UpdateScore(result);
        }
    }
}