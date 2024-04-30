using Kotono.Input;
using Kotono.Utils;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Audio
{
    internal class TestSound : Sound
    {
        internal TestSound()
            : base(Path.ASSETS + @"test.wav")
        {
            Volume = 0.2f;

            Keyboard.SubscribeKeyPressed(OnSpacePressed, Keys.Space);
        }

        private void OnSpacePressed(object? sender, TimedEventArgs e)
        {
            if (IsPlaying)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }
    }
}
