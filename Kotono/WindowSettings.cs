using Kotono.Input;

namespace Kotono
{
    public class WindowSettings
    {
        /// <summary>
        /// This is the Title of the program's Window.
        /// <para> Default value : "Kotono - Application" </para>
        /// </summary>
        public string WindowTitle { get; set; } = "Kotono - Application";

        /// <summary>
        /// This is the program's Window Width,
        /// this can be changed at runtime using KT.Size.
        /// <para> Default value : 1280 </para>
        /// </summary>
        public int Width { get; set; } = 1280;

        /// <summary>
        /// This is the program's Window Height,
        /// this can be changed at runtime using KT.Size.
        /// <para> Default value : 720 </para>
        /// </summary>
        public int Height { get; set; } = 720;

        /// <summary>
        /// This is the Max FrameRate you want your program to run at,
        /// this can be changed at runtime using PerformanceWindow.MaxFrameRate.
        /// <para> Default value : 60 </para>
        /// </summary>
        public int MaxFrameRate { get; set; } = 60;

        /// <summary>
        /// This is the State of the Cursor,
        /// this can be changed at runtime using Mouse.CursorState.
        /// <para> Default value : CursorState.Normal </para>
        /// </summary>
        public CursorState CursorState { get; set; } = CursorState.Normal;
    }
}
