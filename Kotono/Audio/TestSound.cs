namespace Kotono.Audio
{
    internal class TestSound : Sound
    {
        internal TestSound()
        {
            SetSource(Path.FromAssets(@"test.wav"));
            Volume = 0.2f;
        }

        private void OnSpaceKeyPressed()
        {
            Switch();
        }
    }
}
