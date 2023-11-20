using Kotono.Utils;

namespace Kotono.File
{
    public class AnimationProperties : Properties
    {
        public string Directory
        {
            get => Strings["Directory"];
            set => Strings["Directory"] = value;
        }

        public Rect Dest
        {
            get => Rect.FromProperties(this);
            set 
            {
                Floats["Dest.X"] = value.X;
                Floats["Dest.Y"] = value.Y;
                Floats["Dest.W"] = value.W;
                Floats["Dest.H"] = value.H;
            }
        }

        public Color Color 
        { 
            get => Color.FromProperties(this);
            set
            {
                Floats["Color.R"] = value.R;
                Floats["Color.G"] = value.G;
                Floats["Color.B"] = value.B;
                Floats["Color.A"] = value.A;
            }
        }

        public int Layer
        {
            get => Ints["Layer"];
            set => Ints["Layer"] = value;
        }

        public int FrameRate
        {
            get => Ints["FrameRate"];
            set => Ints["FrameRate"] = value;
        }

        public double StartTime
        {
            get => Doubles["StartTime"];
            set => Doubles["StartTime"] = value;
        }

        public double Duration
        {
            get => Doubles["Duration"];
            set => Doubles["Duration"] = value;
        }

        public AnimationProperties(string path) 
            : base(path) { }
    }
}
