namespace Kotono.Audio
{
    internal sealed class TestSound : Sound
    {
        internal TestSound()
        {
            Source = new Source(Path.FromAssets(@"test.wav"));
            Volume = 0.2f;
        }

        private void OnSpaceKeyPressed() => Switch();
    }
}
