using Kotono.Graphics.Objects;

namespace Kotono.Settings
{
    /// <summary>
    /// Settings class for creating an <see cref="Animation"/>.
    /// </summary>
    internal class AnimationSettings : Object2DSettings
    {
        /// <summary>
        /// The directory where the Animation's frames are.
        /// </summary>
        /// <remarks> 
        /// Default value : "" 
        /// </remarks>
        public string Directory { get; set; } = "";

        /// <summary>
        /// The frame rate of the Animation.
        /// </summary>
        /// <remarks> 
        /// Default value : 60 
        /// </remarks>
        public int FrameRate { get; set; } = 60;

        /// <summary>
        /// The start time of the Animation.
        /// </summary>
        /// <remarks> 
        /// Default value : 0.0f 
        /// </remarks>
        public float StartTime { get; set; } = 0.0f;

        /// <summary>
        /// The duration of the Animation.
        /// </summary>
        /// <remarks> 
        /// Default value : 0.0f 
        /// </remarks>
        public float Duration { get; set; } = 0.0f;
    }
}
