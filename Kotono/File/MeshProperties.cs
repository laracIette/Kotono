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
                Data["Transform.Location.X"] = value.Location.X.ToString();
                Data["Transform.Location.Y"] = value.Location.Y.ToString();
                Data["Transform.Location.Z"] = value.Location.Z.ToString();

                Data["Transform.Rotation.X"] = value.Rotation.X.ToString();
                Data["Transform.Rotation.Y"] = value.Rotation.Y.ToString();
                Data["Transform.Rotation.Z"] = value.Rotation.Z.ToString();

                Data["Transform.Scale.X"] = value.Scale.X.ToString();
                Data["Transform.Scale.Y"] = value.Scale.Y.ToString();
                Data["Transform.Scale.Z"] = value.Scale.Z.ToString();
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

        public MeshProperties(string path) 
            : base(path) { }
    }
}
