using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects.Lights
{
    public class PointLight
    {
        private Vector3 _position = Vector3.Zero;

        public PointLight(Vector3 position, int meshIndex)
        {
            Position = position;
            MeshIndex = meshIndex;
        }

        public void Update(Vector3 position)
        {
            Position = position;
        }

        internal void UpdateIndex()
        {
            MeshIndex--;
        }

        public Vector3 Position
        {
            get => _position;
            private set
            {
                _position.X = MathHelper.Clamp(value.X, -20.0f, 20.0f);
                _position.Y = MathHelper.Clamp(value.Y, -20.0f, 20.0f);
                _position.Z = MathHelper.Clamp(value.Z, -20.0f, 20.0f);
            }
        }

        public int MeshIndex { get; private set; }

        public Matrix4 Model => Matrix4.CreateScale(new Vector3(0.2f, 0.2f, 0.4f)) * Matrix4.CreateTranslation(Position);
    }
}