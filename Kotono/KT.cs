using System;
using Kotono.Audio;
using Kotono.Engine;
using Kotono.Engine.UserInterface.AddMenu;
using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Managers;
using Kotono.Graphics.Print;
using Kotono.Input;
using Kotono.Physics;
using Kotono.Utils;
using Performance = Kotono.Graphics.Performance;

namespace Kotono
{
    public static class KT
    {
        #region WindowDest

        private static Rect _dest = Rect.Zero;

        public static Rect Dest
        {
            get => _dest;
            set => _dest = value;
        }

        public static Point Position
        {
            get => _dest.Position;
            set => _dest.Position = value;
        }

        public static Point Size
        {
            get => _dest.Size;
            set
            {
                _dest.Size = value;

                CameraManager.ActiveCamera.AspectRatio = _dest.Size.Ratio;

                ComponentManager.WindowViewport.Size = _dest.Size;
            }
        }

        #endregion WindowDest

        #region Printer

        /// <summary>
        /// Prints an object to the Window given a Color.
        /// </summary>
        /// <param name="obj"> The object to print. </param>
        /// <param name="color"> The Color of the text. </param>
        public static void Print(object? obj, Color color)
        {
            if (obj != null)
            {
                Printer.Print(obj.ToString(), color);
            }
        }

        /// <summary>
        /// Prints an object to the Window.
        /// </summary>
        /// <param name="obj"> The object to print. </param>
        /// <param name="rainbow"> A bool to determine whether the Color of the text should loop through RBG values. </param>
        public static void Print(object? obj, bool rainbow = false)
        {
            Print(obj, rainbow ? Color.Rainbow(0.01) : Color.White);
        }

        /// <summary>
        /// Prints an empty line.
        /// </summary>
        public static void Print()
        {
            Print("");
        }

        #endregion Printer

        #region PerformanceWindow

        internal static Performance.Window PerformanceWindow { get; } = new();

        internal static int MaxFrameRate { get; set; } = 60;

        #endregion PerformanceWindow

        #region UserMode

        private static readonly Mode _mode = new();

        public static UserMode UserMode => _mode.UserMode;

        #endregion UserMode

        #region Logger
        /// <summary>
        /// Writes an object to the console.
        /// </summary>
        /// <param name="obj"> The object to log. </param>
        internal static void Log(object? obj)
        {
            Console.WriteLine(obj);
        }
        #endregion Logger

        internal static void Init()
        {
            ShaderManager.Init();
            Printer.Init();
            PerformanceWindow.Init();
            Fizix.Init();
        }

        internal static void Update()
        {
            Time.Update();
            Mouse.Update();
            Keyboard.Update();
            Gizmo.Update();
            Printer.Update();
            ObjectManager.Update();
            ComponentManager.Update();
            CameraManager.Update();
            PerformanceWindow.Update();
            _mode.Update();
            MainMenu.Update();
        }

        internal static void Draw()
        {
            ShaderManager.Update();
            ObjectManager.Draw();
        }

        internal static void Save()
        {
            ObjectManager.Save();
        }

        internal static void Exit()
        {
            SoundManager.Dispose();
            ObjectManager.Dispose();
        }
    }
}
