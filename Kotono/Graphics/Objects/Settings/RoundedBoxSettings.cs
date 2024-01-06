using Kotono.Utils;

namespace Kotono.Graphics.Objects.Settings
{
    internal class RoundedBoxSettings : Object2DSettings
    {
        /// <summary>
        /// The color of the RoundedBox.
        /// <para> Default value : Color.White </para>
        /// </summary>
        internal Color Color { get; set; } = Color.White;

        /// <summary>
        /// The fall off of the RoundedBox.
        /// <para> Default value : 0 </para>
        /// </summary>
        internal float FallOff { get; set; } = 0;
        
        /// <summary>
        /// The corner size of the RoundedBox.
        /// <para> Default value : 0 </para>
        /// </summary>
        internal float CornerSize { get; set; } = 0;
    }
}
