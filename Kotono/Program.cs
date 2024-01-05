using Kotono.Input;

namespace Kotono
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            new Application(
                new WindowSettings
                {
                    // This is the program's Window Width, this can be resized at runtime
                    Width = 1280,
                    // This is the program's Window Height, this can be resized at runtime
                    Height = 720,
                    // This is the Max FrameRate you want your program to run at, this can be changed at runtime using KT.MaxFrameRate
                    MaxFrameRate = 165,
                    // This is the Title of the program's Window 
                    WindowTitle = "Kotono - Application",
                    // This is the State of the Cursor, this can be changed at runtime
                    CursorState = CursorState.Confined
                }
            ).Run();
                
        }
    }
}