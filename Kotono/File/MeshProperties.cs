using Kotono.Utils;

namespace Kotono.File
{
    public class MeshProperties(string path) 
        : Properties(path)
    {
        public Transform Transform
        {
            get => Transform.FromProperties(this);
            set
            {
                this["Transform.Location.X"] = value.Location.X.ToString();
                this["Transform.Location.Y"] = value.Location.Y.ToString();
                this["Transform.Location.Z"] = value.Location.Z.ToString();

                this["Transform.Rotation.X"] = value.Rotation.X.ToString();
                this["Transform.Rotation.Y"] = value.Rotation.Y.ToString();
                this["Transform.Rotation.Z"] = value.Rotation.Z.ToString();

                this["Transform.Scale.X"] = value.Scale.X.ToString();
                this["Transform.Scale.Y"] = value.Scale.Y.ToString();
                this["Transform.Scale.Z"] = value.Scale.Z.ToString();
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
    }
}
