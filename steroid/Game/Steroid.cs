using System;
using Raylib_cs;
using steroid.Game.Managers;
using steroid.Game.Systems;

namespace steroid.Game
{
    public class Steroid
    {
        public bool IsRunning => !Raylib.WindowShouldClose();
        private Texture2D _texture;
        private MiniShip _miniShip;
        private Ship _ship;
        private AsteroidManager _asteroidManager;
        private CollisionSystem _collisionSystem;
        private GameSystem _gameSystem;
        private bool _isReady = false;
        
        public Steroid()
        {
            _asteroidManager = new AsteroidManager();
            _collisionSystem = new CollisionSystem(_asteroidManager);
            _gameSystem = new GameSystem();
        }

        public void Init()
        {
            Raylib.InitWindow(640,480,"DivGames Steroid port - Raylib 4.0.0");
            Raylib.SetTargetFPS(60);
            
            CreateBackground();
            CreateMiniship();
            CreateShip();
            CreateAsteroids();

            InitSystems();

            _isReady = true;
        }

        private void InitSystems()
        {
            _collisionSystem.Init(_ship);
            _collisionSystem.OnCollision += OnCollision;
        }

        private void OnCollision()
        {
            _isReady = false;
            _ship.Hit();
            _miniShip.Hit();
            _asteroidManager.Init();
            _isReady = true;
        }

        public void Update()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            
            Raylib.DrawTexture(_texture, 0,0,Color.WHITE);

            if (!_isReady) return;
            
            _collisionSystem.Update();
            
            _miniShip.Update();
            _ship.Update();
            
            _asteroidManager.Asteroids.ForEach(asteroid =>
            {
                asteroid.Update();
            });
            
            
            Raylib.EndDrawing();
        }

        private void CreateBackground()
        {
            var background = Raylib.GenImageColor(640, 480, Color.BLACK);

            var r = new Random();
            
            for (var i = 0; i < 499; i++)
            {
                Raylib.ImageDrawPixel(ref background, r.Next(0,639), r.Next(0,479), Color.WHITE);    
            }
            
            _texture = Raylib.LoadTextureFromImage(background);
            
            
        }

        private void CreateMiniship()
        {
            _miniShip = new MiniShip();
            _miniShip.Init();
        }
        
        private void CreateShip()
        {
            _ship = new Ship();
            _ship.Init();
        }

        private void CreateAsteroids()
        {
            _asteroidManager.Init();
        }
        
        ~Steroid()
        {
            _collisionSystem.OnCollision -= OnCollision;
        }
    }
}