using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotono.Graphics.Objects.Settings
{
    internal class RoundedBorderSettings : RoundedBoxSettings
    {
        /// <summary>
        /// The thickness of the RoundedBorder.
        /// <para> Default value : 1 </para>
        /// </summary>
        internal float Thickness { get; set; } = 1.0f;
    }
}
