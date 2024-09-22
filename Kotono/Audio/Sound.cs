namespace Kotono.Audio
{
    internal class Sound : Object
    {
        /// <summary>
        /// The audio source of the <see cref="Sound"/>.
        /// </summary>
        internal Source? Source { get; set; } = null;

        /// <summary>
        /// The volume of the <see cref="Audio.Source"/> of the <see cref="Sound"/>, clamped in range [0, 1].
        /// </summary>
        internal float Volume
        {
            get => Source?.Volume ?? 0.0f;
            set
            {
                if (Source != null)
                {
                    Source.Volume = value;
                }
            }
        }

        /// <summary>
        /// Wether the <see cref="Audio.Source"/> of the <see cref="Sound"/> is currently playing. 
        /// </summary>
        internal bool IsPlaying => Source?.IsPlaying ?? false;

        /// <summary>
        /// Wether the <see cref="Audio.Source"/> of the <see cref="Sound"/> is currently paused. 
        /// </summary>
        internal bool IsPaused => Source?.IsPaused ?? false;

        /// <summary>
        /// Wether the <see cref="Audio.Source"/> of the <see cref="Sound"/> is currently stopped. 
        /// </summary>
        internal bool IsStopped => Source?.IsStopped ?? false;

        /// <summary>
        /// Play the <see cref="Audio.Source"/> of the <see cref="Sound"/>. 
        /// </summary>
        internal void Play() => Source?.Play();

        /// <summary>
        /// Pause the <see cref="Audio.Source"/> of the <see cref="Sound"/>. 
        /// </summary>
        internal void Pause() => Source?.Pause();

        /// <summary>
        /// Rewind the <see cref="Audio.Source"/> of the <see cref="Sound"/>. 
        /// </summary>
        internal void Rewind() => Source?.Rewind();

        /// <summary>
        /// Stop the <see cref="Audio.Source"/> of the <see cref="Sound"/>. 
        /// </summary>
        internal void Stop() => Source?.Stop();

        /// <summary>
        /// Play / pause the <see cref="Audio.Source"/> of the <see cref="Sound"/>. 
        /// </summary>
        internal void Switch() => Source?.Switch();

        public override void Dispose()
        {
            Source?.Dispose();

            base.Dispose();
        }
    }
}
