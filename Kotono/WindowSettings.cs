using Kotono.Input;
using Kotono.Utils;

namespace Kotono
{
    public class WindowSettings
    {
        public int Width { get; set; } = 1280;

        public int Height { get; set; } = 720;

        public int MaxFrameRate { get; set; } = 60;

        public string WindowTitle { get; set; } = "Kotono - Application";

        public string KotonoPath { get; set; } = "";

        public string ProjectPath { get; set; } = "";

        public CursorState CursorState { get; set; } = CursorState.Normal;

        public Color ClearColor { get; set; } = Color.White;
    }
}
