using Kotono.Utils;

namespace Kotono.File
{
    public class MeshProperties : Properties
    {
        public Transform Transform
        {
            get => Transform.FromProperties(this);
            set 
            {
                Floats["Transform.Location.X"] = value.Location.X;
                Floats["Transform.Location.Y"] = value.Location.Y;
                Floats["Transform.Location.Z"] = value.Location.Z;

                Floats["Transform.Rotation.X"] = value.Rotation.X;
                Floats["Transform.Rotation.Y"] = value.Rotation.Y;
                Floats["Transform.Rotation.Z"] = value.Rotation.Z;

                Floats["Transform.Scale.X"] = value.Scale.X;
                Floats["Transform.Scale.Y"] = value.Scale.Y;
                Floats["Transform.Scale.Z"] = value.Scale.Z;
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

        public MeshProperties(string path) 
            : base(path) { }
    }
}
