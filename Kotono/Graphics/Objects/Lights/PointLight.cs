using Kotono.Utils;
using OpenTK.Mathematics;
using Random = Kotono.Utils.Random;

namespace Kotono.Graphics.Objects.Lights
{
    public class PointLight
    {
        private Vector3 _position = Vector3.Zero;

        private Vector3 _positionVelocity = Vector3.Zero;

        public PointLight(Vector3 position)
        {
            Position = position;
        }

        public void Update()
        {
            PositionVelocity += Random.Vector3(-0.1f, 0.1f) * Time.Delta;
            Position += PositionVelocity;
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

        private Vector3 PositionVelocity
        {
            get => _positionVelocity; 
            set
            {
                _positionVelocity.X = MathHelper.Clamp(value.X, -1.0f, 1.0f);
                _positionVelocity.Y = MathHelper.Clamp(value.Y, -1.0f, 1.0f);
                _positionVelocity.Z = MathHelper.Clamp(value.Z, -1.0f, 1.0f);
            }
        }

        public Matrix4 Model => Matrix4.CreateScale(new Vector3(0.2f, 0.2f, 0.4f)) * Matrix4.CreateTranslation(Position);
    }
}