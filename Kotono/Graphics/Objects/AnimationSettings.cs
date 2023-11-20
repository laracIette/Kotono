using Kotono.Utils;

namespace Kotono.Graphics.Objects
{
    public class AnimationSettings
    {
        public string Path { get; set; } = "";

        public int FrameRate { get; set; } = 60;

        public double StartTime { get; set; } = 0;

        public double Duration { get; set; } = 0;

        public ImageSettings ImageSettings { get; set; } = new();
    }
}
