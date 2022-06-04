﻿using System.Numerics;
using Raylib_cs;
using steroid_port.Game.Components;
using steroid_port.Game.Services;

namespace steroid_port.Game.Views.Base
{
    public class View : IRenderable
    {
        public Rectangle Bounds { get; set; }
        public Texture2D Texture2D { get; set; }
        public Vector3 Position { get; set; }
        public int Id { get; protected set; }
        protected Vector2 CurrentScreenSize { get; set; }
        
        public View(RenderService renderService, ScreenService screenService)
        {
            RenderService = renderService;
            CurrentScreenSize = screenService.CurrentSize;
        }
        
        protected RenderService RenderService { get; }
    }
}