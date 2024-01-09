using Kotono.Utils;
using System.Collections.Generic;
using System;
using System.IO;

namespace Kotono.Graphics.Objects.Settings
{
    internal class Object2DSettings : DrawableSettings
    {
        /// <summary>
        /// The dest of the Object2D.
        /// <para> Default value : Rect.Zero </para>
        /// </summary>
        public Rect Dest { get; set; } = Rect.Zero;
        
        /// <summary>
        /// The layer of the Object2D.
        /// <para> Default value : 0 </para>
        /// </summary>
        public int Layer { get; set; } = 0; 
        
        public override string ToString()
        {
            return base.ToString()
                + $"Dest.X: {Dest.X}\nDest.Y: {Dest.Y}\nDest.W: {Dest.W}\nDest.H: {Dest.H}\n"
                + $"Layer: {Layer}\n";
        }
    }
}
