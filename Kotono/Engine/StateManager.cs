namespace Kotono.Engine
{
    internal class StateManager : Object
    {
        private static StateManager _instance = new();

        private StateManager() { }

        internal static EngineState EngineState { get; private set; } = EngineState.Navigate;

        private void OnRightButtonPressed()
        {
            EngineState = EngineState.Play;
        }

        private void OnRightButtonReleased()
        {
            EngineState = EngineState.Navigate;
        }
    }
}
