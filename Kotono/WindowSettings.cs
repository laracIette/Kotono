using Kotono.Input;

namespace Kotono
{
    /// <summary>
    /// Settings class for creating a <see cref="Window"/>.
    /// </summary>
    internal class WindowSettings : ObjectSettings
    {
        /// <summary>
        /// This is the Title of the program's Window.
        /// </summary>
        /// <remarks> 
        /// Default value : "Kotono - Application" 
        /// </remarks>
        public string WindowTitle { get; set; } = "Kotono - Application";

        /// <summary>
        /// This is the program's Window Width,
        /// this can be changed at runtime using Window.Size.
        /// </summary>
        /// <remarks> 
        /// Default value : 1280 
        /// </remarks>
        public int Width { get; set; } = 1280;

        /// <summary>
        /// This is the program's Window Height,
        /// this can be changed at runtime using Window.Size.
        /// </summary>
        /// <remarks> 
        /// Default value : 720 
        /// </remarks>
        public int Height { get; set; } = 720;

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
