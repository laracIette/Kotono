namespace Kotono.Graphics.Objects.Buttons
{
    internal interface IButton
    {
        public bool IsDown { get; }

        public bool WasDown { get; }

        public bool IsPressed { get; }

        public bool IsReleased { get; }

        public void OnPressed();

        public void OnReleased();
    }
}
