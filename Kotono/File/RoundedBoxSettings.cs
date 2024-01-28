using Kotono.Utils;

namespace Kotono.File
{
    internal class RoundedBoxSettings : Object2DSettings
    {
        /// <summary>
        /// The fall off of the RoundedBox.
        /// <para> Default value : 0.0f </para>
        /// </summary>
        public float FallOff { get; set; } = 0.0f;

        /// <summary>
        /// The corner size of the RoundedBox.
        /// <para> Default value : 0.0f </para>
        /// </summary>
        public float CornerSize { get; set; } = 0.0f;
    }
}
