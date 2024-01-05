using Kotono.Input;

namespace Kotono
{
    public class WindowSettings
    {
        public int Width { get; set; } = 1280;

        public int Height { get; set; } = 720;

        public int MaxFrameRate { get; set; } = 60;

        public string WindowTitle { get; set; } = "Kotono - Application";

        public CursorState CursorState { get; set; } = CursorState.Normal;
    }
}
