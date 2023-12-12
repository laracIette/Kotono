using Kotono.Utils;

namespace Kotono.File
{
    public class AnimationProperties : Properties
    {
        public string Directory
        {
            get => this["Directory"];
            set => this["Directory"] = value;
        }

        public Rect Dest
        {
            get => Rect.FromProperties(this);
            set 
            {
                this["Dest.X"] = value.X.ToString();
                this["Dest.Y"] = value.Y.ToString();
                this["Dest.W"] = value.W.ToString();
                this["Dest.H"] = value.H.ToString();
            }
        }

        public Color Color 
        { 
            get => Color.FromProperties(this);
            set
            {
                this["Color.R"] = value.R.ToString();
                this["Color.G"] = value.G.ToString();
                this["Color.B"] = value.B.ToString();
                this["Color.A"] = value.A.ToString();
            }
        }

        public int Layer
        {
            get => int.Parse(this["Layer"]);
            set => this["Layer"] = value.ToString();
        }

        public int FrameRate
        {
            get => int.Parse(this["FrameRate"]);
            set => this["FrameRate"] = value.ToString();
        }

        public double StartTime
        {
            get => double.Parse(this["StartTime"]);
            set => this["StartTime"] = value.ToString();
        }

        public double Duration
        {
            get => double.Parse(this["Duration"]);
            set => this["Duration"] = value.ToString();
        }

        public AnimationProperties(string path) 
            : base(path) { }
    }
}
