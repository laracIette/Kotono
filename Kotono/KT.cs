using Kotono.Audio;
using Kotono.Engine;
using Kotono.Engine.UserInterface.AddMenu;
using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Managers;
using Kotono.Graphics.Print;
using Kotono.Graphics.Statistics;
using Kotono.Input;
using Kotono.Utils;
using System;

namespace Kotono
{
    internal static class KT
    {

        #region WindowDest

        private static Rect _dest = new(0.0f, 0.0f, 1280.0f, 720.0f);

        internal static Rect Dest
        {
            get => _dest;
            set
            {
                Position = value.Position;
                Size = value.Size;
            }
        }

        internal static Point Position
        {
            get => _dest.Position;
            set => _dest.Position = value;
        }

        internal static Point Size
        {
            get => _dest.Size;
            set
            {
                _dest.Size = value;

                CameraManager.ActiveCamera.AspectRatio = Size.Ratio;

                ComponentManager.WindowViewport.Size = Size;

                PerformanceWindow.UpdatePosition();

                if (Size > Point.Zero)
                {
                    ObjectManager.SetSize(Size);
                }
            }
        }

        #endregion WindowDest

        #region Printer

        /// <summary>
        /// Prints an object to the Window given a Color.
        /// </summary>
        /// <param name="obj"> The object to print. </param>
        /// <param name="color"> The Color of the text. </param>
        internal static void Print(object? obj, Color color)
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
        internal static void Print(object? obj, bool rainbow = false)
        {
            Print(obj, rainbow ? Color.Rainbow(0.01) : Color.White);
        }

        /// <summary>
        /// Prints an empty line.
        /// </summary>
        internal static void Print()
        {
            Print("");
        }

        #endregion Printer

        #region UserMode

        private static readonly Mode _mode = new();

        internal static UserMode UserMode => _mode.UserMode;

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

        /// <summary>
        /// Writes an empty line to the console.
        /// </summary>
        internal static void Log()
        {
            Log("");
        }

        #endregion Logger

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
            _mode.Update();
            MainMenu.Update();
        }

        internal static void Save()
        {
            ObjectManager.Save();
        }

        internal static void Exit()
        {
            SoundManager.Dispose();
            Texture.DisposeAll();
            ObjectManager.Dispose();
        }
    }
}
