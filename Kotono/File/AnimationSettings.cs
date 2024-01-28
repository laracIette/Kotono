using Kotono.Graphics.Objects;

namespace Kotono.File
{
    /// <summary>
    /// Settings class for creating an <see cref="Object2D"/>.
    /// </summary>
    internal class AnimationSettings : Object2DSettings
    {
        /// <summary>
        /// The directory where the Animation's frames are.
        /// <para> Default value : "" </para>
        /// </summary>
        public string Directory { get; set; } = "";

        /// <summary>
        /// The frame rate of the Animation.
        /// <para> Default value : 60 </para>
        /// </summary>
        public int FrameRate { get; set; } = 60;

        /// <summary>
        /// The start time of the Animation.
        /// <para> Default value : 0 </para>
        /// </summary>
        public double StartTime { get; set; } = 0;

        /// <summary>
        /// The duration of the Animation.
        /// <para> Default value : 0 </para>
        /// </summary>
        public double Duration { get; set; } = 0;
    }
}
