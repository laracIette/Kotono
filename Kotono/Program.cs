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
                    Width = 1600,
                    Height = 800,
                    MaxFrameRate = 165,
                    WindowTitle = "Kotono - Application",
                    CursorState = CursorState.Confined
                }
            ).Run();
        }
    }
}