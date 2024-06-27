using Kotono.Input;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Engine
{
    internal class StateManager : Object
    {
        private StateManager _instance = new();

        private StateManager() : base() { }

        internal static EngineState EngineState { get; private set; } = EngineState.Navigate;

        public override void Update()
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
