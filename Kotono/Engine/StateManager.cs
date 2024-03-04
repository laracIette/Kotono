using Kotono.Input;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Engine
{
    internal static class StateManager
    {
        internal static EngineState EngineState { get; private set; } = EngineState.Navigate;

        internal static void Update()
        {
            if (Mouse.IsButtonDown(MouseButton.Right))
            {
                EngineState = EngineState.Play;
            }
            else
            {
                EngineState = EngineState.Navigate;
            }
        }
    }
}
