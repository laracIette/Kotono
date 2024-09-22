using Kotono.Input;

namespace Kotono
{
    internal static class Program
    {
        internal static void Main()
        {
            new Application(
                new WindowSettings
                {
                    WindowSize = (1600, 800),
                    MaxFrameRate = 60.0f,
                    CursorState = CursorState.Confined
                }
            ).Run();
        }
    }
}