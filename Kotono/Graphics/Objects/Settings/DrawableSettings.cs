using Kotono.Utils;
using System;
using System.Collections.Generic;

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

        public override string ToString()
        {
            return $"IsDraw: {IsDraw}\n";
        }
    }
}
