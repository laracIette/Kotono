using OpenTK.Mathematics;
using Random = Kotono.Utils.Random;

namespace Kotono.Graphics.Objects.Lights
{
    public class PointLight
    {
        private Vector3 _position;

        private Vector3 _positionVelocity = new Vector3(0.0f, 0.0f, 0.0f);

        public PointLight(Vector3 position)
        {
            Position = position;
        }

        public void Update(float deltaTime)
        {
            PositionVelocity += Random.Vector3(-0.1f, 0.1f);
            Position += PositionVelocity * deltaTime;
        }

        public Vector3 Position
        {
            get { return _position; }
            private set
            {
                _position.X = MathHelper.Clamp(value.X, -20.0f, 20.0f);
                _position.Y = MathHelper.Clamp(value.Y, -20.0f, 20.0f);
                _position.Z = MathHelper.Clamp(value.Z, -20.0f, 20.0f);
            }
        }

        private Vector3 PositionVelocity
        {
            get { return _positionVelocity; }
            set
            {
                _positionVelocity.X = MathHelper.Clamp(value.X, -1.0f, 1.0f);
                _positionVelocity.Y = MathHelper.Clamp(value.Y, -1.0f, 1.0f);
                _positionVelocity.Z = MathHelper.Clamp(value.Z, -1.0f, 1.0f);
            }
        }

        public Matrix4 Model
        {
            get
            {
                return Matrix4.CreateScale(0.2f) * Matrix4.CreateTranslation(Position);
            }
        }
    }
}
