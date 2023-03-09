using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using static OpenTK.Graphics.OpenGL.GL;
using Random = Kotono.Utils.Random;

namespace Kotono.Graphics.Objects.Meshes
{
    public class MeshOBJ : IMesh, IDisposable
    {
        private Vector3 _position = Vector3.Zero;

        private Vector3 _positionVelocity = Vector3.Zero;

        private Vector3 _angleVelocity = Vector3.Zero;

        public MeshOBJ(int vertexArrayObject, int vertexBufferObject, int verticesCount, Vector3 position, Vector3 angle, Vector3 scale, int diffuseMap, int specularMap)
        {
            VertexArrayObject = vertexArrayObject;
            VertexBufferObject = vertexBufferObject;
            VerticesCount = verticesCount;
            Position = position;
            Angle = angle;
            Scale = scale;
            DiffuseMap = diffuseMap;
            SpecularMap = specularMap;
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

        public void Draw()
        {
            TextureManager.UseTexture(DiffuseMap, TextureUnit.Texture0);
            TextureManager.UseTexture(SpecularMap, TextureUnit.Texture1);

            ShaderManager.LightingShader.SetMatrix4("model", Model);

            GL.BindVertexArray(VertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, VerticesCount);
        }

        public int VertexBufferObject { get; }

        public int VertexArrayObject { get; }

        public int VerticesCount { get; }

        public int DiffuseMap { get; }

        public int SpecularMap { get; }

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

        private Vector3 Angle { get; set; }

        private Vector3 Scale { get; set; }

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

        public Matrix4 Model =>
            Matrix4.CreateScale(Scale)
            * Matrix4.CreateRotationX(Angle.X)
            * Matrix4.CreateRotationY(Angle.Y)
            * Matrix4.CreateRotationZ(Angle.Z)
            * Matrix4.CreateTranslation(Position);

        public void Dispose()
        {
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            GL.DeleteVertexArray(VertexArrayObject);
        }
    }
}