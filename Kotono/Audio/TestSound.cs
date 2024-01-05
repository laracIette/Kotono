using Kotono.Input;
using Kotono.Utils;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Audio
{
    public class TestSound : Sound
    {
        public TestSound()
            : base(Path.Assets + @"test.wav")
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
