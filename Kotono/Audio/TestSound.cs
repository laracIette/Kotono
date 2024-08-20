﻿namespace Kotono.Audio
{
    internal class TestSound : Sound
    {
        internal TestSound()
            : base(Path.FromAssets(@"test.wav"))
        {
            Volume = 0.2f;
        }

        private void OnSpaceKeyPressed()
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
