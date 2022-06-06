using System.Collections.Generic;
using common.Core.Configurations.Base;
using common.Core.Factories.States;
using common.Core.Managers.Game;
using common.Core.Services.Collision;
using common.Core.Services.Config;
using common.Core.Services.Render;
using common.Core.Services.Screen;
using common.Core.Utils;
using Raylib_cs;

namespace common.Core.Bootstrap
{
    public abstract class BaseBootstrap<T,TU> where T : class where TU : Config
    {
        public bool IsQuit => Raylib.WindowShouldClose();
        
        protected ConfigService<TU> ConfigService;
        protected GameManager GameManager;
        protected StateFactory StateFactory;
        protected Utilities Utilities;
        protected RenderService RenderService;
        protected ScreenService ScreenService;
        protected CollisionService CollisionService;

        private readonly List<Systems.Base.System> _loadedSystems;

        private TU _config;

        protected BaseBootstrap(TU config)
        {
            _config = config;
            _loadedSystems = new List<Systems.Base.System>();
        }

        protected abstract void InitCustomServices();
        protected abstract void BuildCustomSystems();
        protected abstract void RegisterCustomStates();
        
        public void Init()
        {
            GameManager = new GameManager();

            InitUtilities();
            InitServices();
            BuildSystems();
            InitFactories();
            RegisterStates();
           
            GameManager.Init(StateFactory);
            GameManager.SetState(StateFactory.Get(typeof(T)));
        }

        protected virtual void InitUtilities()
        {
            Utilities = new Utilities();
        }

        public void RegisterSystem(Systems.Base.System system)
        {
            _loadedSystems.Add(system);    
        }

        protected void UnloadSystems()
        {
            for (var i = 0; i < _loadedSystems.Count; i++)
            {
                _loadedSystems[i] = null;
            }    
        }
        
        private void InitFactories()
        {
            StateFactory = new StateFactory();
            StateFactory.Init();
        }

        private void RegisterStates()
        {
            RegisterCustomStates();
        }

        private void BuildSystems()
        {
            BuildCustomSystems();
        }
        
        private void InitServices()
        {
            CollisionService = new CollisionService();
            ConfigService = new ConfigService<TU>(_config);
            RenderService = new RenderService();
            ScreenService = new ScreenService();
            
            ScreenService.Init(_config.Width,_config.Height);
            RenderService.Init(_config);
            CollisionService.Init();
            
            InitCustomServices();
        }

        public void Update()
        {
            RenderService.Begin();
            
            GameManager.Update();
            
            RenderService.End();
        }
    }
}