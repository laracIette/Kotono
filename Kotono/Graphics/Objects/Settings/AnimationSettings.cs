using Kotono.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects.Settings
{
    internal class AnimationSettings : Object2DSettings
    {
        /// <summary>
        /// The directory where the Animation's frames are.
        /// <para> Default value : "" </para>
        /// </summary>
        [Parsable]
        public string Directory { get; set; } = "";

        /// <summary>
        /// The color of the Animation.
        /// <para> Default value : Color.White </para>
        /// </summary>        
        [Parsable]
        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// The frame rate of the Animation.
        /// <para> Default value : 60 </para>
        /// </summary>
        [Parsable]
        public int FrameRate { get; set; } = 60;

        /// <summary>
        /// The start time of the Animation.
        /// <para> Default value : 0 </para>
        /// </summary>
        [Parsable]
        public double StartTime { get; set; } = 0;

        /// <summary>
        /// The duration of the Animation.
        /// <para> Default value : 0 </para>
        /// </summary>
        [Parsable]
        public double Duration { get; set; } = 0;

        public override string ToString()
        {
            return base.ToString()
                + $"Directory: {Directory}\n"
                + $"Color.R: {Color.R}\nColor.G: {Color.G}\nColor.B: {Color.B}\nColor.A: {Color.A}\n"
                + $"FrameRate: {FrameRate}\n" 
                + $"StartTime: {StartTime}\n"
                + $"Duration: {Duration}";
        }
    }
}
