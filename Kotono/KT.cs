﻿using Kotono.Audio;
using Kotono.Engine;
using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Managers;
using Kotono.Graphics.Print;
using Kotono.Input;
using Kotono.Physics;
using Kotono.Utils;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Performance = Kotono.Graphics.Performance;
using Text = Kotono.Graphics.Objects.Text;

namespace Kotono
{
    public static class KT
    {
        //private static readonly ComponentManager _componentManager = new();

        #region Viewport

        public static readonly Viewport _viewport = new();

        public static Viewport ActiveViewport => _viewport;

        #endregion Viewport

        #region WindowSize

        private static Rect _windowDest = Rect.Zero;

        public static Rect Dest => _windowDest;

        public static Point Position => _windowDest.Position;

        public static Point Size => _windowDest.Size;

        public static void SetWindowPosition(Point position)
        {
            _windowDest.Position = position;
        }

        public static void SetWindowSize(Point size)
        {
            _windowDest.Size = size;

            CameraManager.ActiveCamera.AspectRatio = size.X / size.Y;

            ActiveViewport.SetSize(size);
        }

        #endregion WindowSize

        #region Printer

        public static void Print(object? obj, Color color)
        {
            if (obj != null)
            {
                Printer.Print(obj.ToString(), color);
            }
        }

        public static void Print(object? obj)
        {
            Print(obj, Color.White);
        }

        public static void Print()
        {
            Print("");
        }

        #endregion Printer

        #region PerformanceWindow

        private static readonly Performance.Window _performanceWindow = new();

        public static int MaxFrameRate { get; set; } = 60;

        public static void AddFrameTime(double frameTime)
        {
            _performanceWindow.AddFrameTime(frameTime);
        }

        public static void AddUpdateTime(double updateTime)
        {
            _performanceWindow.AddUpdateTime(updateTime);
        }

        public static double GetFrameTime()
        {
            return _performanceWindow.FrameTime;
        }

        public static double GetUpdateTime()
        {
            return _performanceWindow.UpdateTime;
        }

        public static double GetFrameRate()
        {
            return _performanceWindow.FrameRate;
        }

        public static double GetUpdateRate()
        {
            return _performanceWindow.UpdateRate;
        }

        #endregion PerformanceWindow

        #region UserMode

        private static readonly Mode _mode = new();

        public static UserMode UserMode => _mode.UserMode;

        #endregion UserMode

        public static void Init(MouseState mouseState, KeyboardState keyboardState)
        {
            Mouse.Init(mouseState);
            Keyboard.Init(keyboardState);
            ShaderManager.Init();
            SquareVertices.Init();
            Gizmo.Init();
            ObjectManager.Init();
            Text.InitPaths();
            Printer.Init();
            _performanceWindow.Init();
            //_componentManager.Init();
            Fizix.Init();
            _mode.Init();
            SoundManager.Init();
        }

        public static void Update()
        {
            Time.Update();
            Mouse.Update();
            Keyboard.Update();
            Gizmo.Update();
            ObjectManager.Update();
            //_componentManager.Update();
            CameraManager.Update();
            Printer.Update();
            _performanceWindow.Update();
            _mode.Update();
        }

        public static void RenderFrame()
        {
            UpdateShaders();
            Draw();
        }

        private static void UpdateShaders()
        {
            ObjectManager.UpdateShaders();
            //_componentManager.UpdateShaders();
            ShaderManager.Update();
        }

        private static void Draw()
        {
            ObjectManager.Draw();
            //_componentManager.Draw();
        }

        public static void Save()
        {
            ObjectManager.Save();
        }

        public static void Exit()
        {
            SoundManager.Dispose();
        }
    }
}
