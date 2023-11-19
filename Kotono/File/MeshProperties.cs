using Kotono.Utils;

namespace Kotono.File
{
    public class MeshProperties : Properties
    {
        public MeshProperties(string path) 
            : base(path) { }

        public void SetTransform(Transform t)
        {
            Floats["Transform.Location.X"] = t.Location.X;
            Floats["Transform.Location.Y"] = t.Location.Y;
            Floats["Transform.Location.Z"] = t.Location.Z;

            Floats["Transform.Rotation.X"] = t.Rotation.X;
            Floats["Transform.Rotation.Y"] = t.Rotation.Y;
            Floats["Transform.Rotation.Z"] = t.Rotation.Z;

            Floats["Transform.Scale.X"] = t.Scale.X;
            Floats["Transform.Scale.Y"] = t.Scale.Y;
            Floats["Transform.Scale.Z"] = t.Scale.Z;
        }

        public void SetColor(Color c)
        {
            Floats["Color.R"] = c.R;
            Floats["Color.G"] = c.G;
            Floats["Color.B"] = c.B;
            Floats["Color.A"] = c.A;
        }
    }
}
