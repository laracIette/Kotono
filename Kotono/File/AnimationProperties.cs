using Kotono.Utils;

namespace Kotono.File
{
    public class AnimationProperties : Properties
    {
        public string Directory
        {
            get => Data["Directory"];
            set => Data["Directory"] = value;
        }

        public Rect Dest
        {
            get => Rect.FromProperties(this);
            set 
            {
                Data["Dest.X"] = value.X.ToString();
                Data["Dest.Y"] = value.Y.ToString();
                Data["Dest.W"] = value.W.ToString();
                Data["Dest.H"] = value.H.ToString();
            }
        }

        public Color Color 
        { 
            get => Color.FromProperties(this);
            set
            {
                Data["Color.R"] = value.R.ToString();
                Data["Color.G"] = value.G.ToString();
                Data["Color.B"] = value.B.ToString();
                Data["Color.A"] = value.A.ToString();
            }
        }

        public int Layer
        {
            get => int.Parse(Data["Layer"]);
            set => Data["Layer"] = value.ToString();
        }

        public int FrameRate
        {
            get => int.Parse(Data["FrameRate"]);
            set => Data["FrameRate"] = value.ToString();
        }

        public double StartTime
        {
            get => double.Parse(Data["StartTime"]);
            set => Data["StartTime"] = value.ToString();
        }

        public double Duration
        {
            get => double.Parse(Data["Duration"]);
            set => Data["Duration"] = value.ToString();
        }

        public AnimationProperties(string path) 
            : base(path) { }
    }
}
