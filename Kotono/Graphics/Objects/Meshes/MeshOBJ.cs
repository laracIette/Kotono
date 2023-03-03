using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Random = Kotono.Utils.Random;

namespace Kotono.Graphics.Objects.Meshes
{
    public class MeshOBJ : IMesh, IDisposable
    {
        private Vector3 _position;

        private Vector3 _positionVelocity = new Vector3(0.0f);

        private Vector3 _angleVelocity = new Vector3(0.0f);

        public MeshOBJ(int vertexArrayObject, int indexBufferObject, int indexCount, Vector3 position, Vector3 angle)
        {
            VertexArrayObject = vertexArrayObject;
            IndexBufferObject = indexBufferObject;
            IndexCount = indexCount;
            Position = position;
            Angle = angle;
        }

        public void Update(float deltaTime, IEnumerable<IMesh> models)
        {
            AngleVelocity += Random.Vector3(-0.1f, 0.1f);
            Angle += AngleVelocity * deltaTime;

            PositionVelocity += Random.Vector3(-0.1f, 0.1f);

            bool collides = false;

            foreach (var cube in models)
            {
                if (Vector3.Distance(Position + PositionVelocity * deltaTime, cube.Position) <= 1.5f)
                {
                    collides = true;
                    break;
                }
            }

            if (!collides)
            {
                Position += PositionVelocity * deltaTime;
            }
        }

        public void Dispose()
        {
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            GL.DeleteVertexArray(VertexArrayObject);
            GL.DeleteBuffer(IndexBufferObject);
        }

        public int VertexArrayObject { get; }

        public int IndexBufferObject { get; }

        public int IndexCount { get; }

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

        private Vector3 Angle
        {
            get;
            set;
        }

        private Vector3 AngleVelocity
        {
            get => _angleVelocity;
            set
            {
                _angleVelocity.X = MathHelper.Clamp(value.X, -2.5f, 2.5f);
                _angleVelocity.Y = MathHelper.Clamp(value.Y, -2.5f, 2.5f);
                _angleVelocity.Z = MathHelper.Clamp(value.Z, -2.5f, 2.5f);
            }
        }

        public Matrix4 ModelMatrix => Matrix4.CreateTranslation(Position) * Matrix4.CreateRotationX(Angle.X) * Matrix4.CreateRotationY(Angle.Y) * Matrix4.CreateRotationZ(Angle.Z);

    }
}