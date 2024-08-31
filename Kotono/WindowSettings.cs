using Kotono.Input;
using Kotono.Utils.Coordinates;

namespace Kotono
{
    /// <summary>
    /// Settings class for creating a <see cref="Window"/>.
    /// </summary>
    internal class WindowSettings
    {
        /// <summary>
        /// This is the Title of the program's Window.
        /// </summary>
        /// <remarks> 
        /// Default value : "Kotono - Application" 
        /// </remarks>
        public string WindowTitle { get; set; } = "Kotono - Application";

        /// <summary>
        /// This is the program's Window Size,
        /// this can be changed at runtime using Window.Size.
        /// </summary>
        /// <remarks> 
        /// Default value : new PointI(1280, 720)
        /// </remarks>
        public PointI WindowSize { get; set; } = new(1280, 720);

        /// <summary>
        /// This is the Max FrameRate you want your program to run at,
        /// this can be changed at runtime using PerformanceWindow.MaxFrameRate.
        /// </summary>
        /// <remarks> 
        /// Default value : 60.0f 
        /// </remarks>
        public float MaxFrameRate { get; set; } = 60.0f;

        /// <summary>
        /// This is the State of the Cursor,
        /// this can be changed at runtime using Mouse.CursorState.
        /// </summary>
        /// <remarks> 
        /// Default value : CursorState.Normal 
        /// </remarks>
        public CursorState CursorState { get; set; } = CursorState.Normal;
    }
}
