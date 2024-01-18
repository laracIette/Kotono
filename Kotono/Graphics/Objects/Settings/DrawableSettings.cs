using Kotono.Utils;

namespace Kotono.Graphics.Objects.Settings
{

    internal class DrawableSettings
    {
        /// <summary>
        /// Wether the Drawable should be drawn.
        /// <para> Default value : true </para>
        /// </summary>
        [Parsable]
        public virtual bool IsDraw { get; set; } = true;
    }
}
