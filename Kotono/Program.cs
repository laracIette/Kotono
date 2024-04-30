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
                    Width = 1600,
                    Height = 800,
                    MaxFrameRate = 165.0f,
                    CursorState = CursorState.Normal
                }
            ).Run();
        }
    }
}