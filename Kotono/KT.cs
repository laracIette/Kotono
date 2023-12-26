using Kotono.Audio;
using Kotono.Engine;
using Kotono.Engine.Interface.AddMenu;
using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Managers;
using Kotono.Graphics.Print;
using Kotono.Input;
using Kotono.Physics;
using Kotono.Utils;
using Performance = Kotono.Graphics.Performance;
using Text = Kotono.Graphics.Objects.Text;

namespace Kotono
{
    public static class KT
    {
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

            ComponentManager.Window.Viewport.SetSize(size);
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
            Print(
                obj,
                new Color
                {
                    R = (Math.Sin(0.01 * Time.Now + 0.0) * 0.5f) + 0.5f,
                    G = (Math.Sin(0.01 * Time.Now + 2.0) * 0.5f) + 0.5f,
                    B = (Math.Sin(0.01 * Time.Now + 4.0) * 0.5f) + 0.5f
                }
            );
        }

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

        internal static void Init()
        {
            ShaderManager.Init();
            Text.InitPaths();
            ObjectManager.Init();
            Printer.Init();
            PerformanceWindow.Init();
            ComponentManager.Init();
            Fizix.Init();
            _mode.Init();
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
            ObjectManager.Draw();
        }

        internal static void UpdateShaders()
        {
            ObjectManager.UpdateShaders();
            ShaderManager.Update();
        }

        public static void Save()
        {
            ObjectManager.Save();
        }

        internal static void Exit()
        {
            SoundManager.Dispose();
        }
    }
}
