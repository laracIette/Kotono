using Kotono.Input;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Audio
{
    public class TestSound : Sound
    {
        public TestSound()
            : base(Path.ASSETS + @"test.wav")
        {
            Volume = 0.2f;
        }

        public void Update()
        {
            if (Keyboard.IsKeyPressed(Keys.Space))
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
}
