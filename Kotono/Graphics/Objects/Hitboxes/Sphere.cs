using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects.Hitboxes
{
    internal class Sphere : Hitbox
    {
        private const int SEGMENTS = 64;

        private static readonly Vector[] _vertices = new Vector[SEGMENTS];

        private static readonly VertexArraySetup _vertexArraySetup = new();

        private static readonly Object3DShader _shader = (Object3DShader)ShaderManager.Shaders["hitbox"];

        private static bool _isFirst = true;

        internal float Radius => RelativeScale.X;

        internal Sphere()
        {
            if (_isFirst)
            {
                _isFirst = false;

                for (int i = 0; i < SEGMENTS; i++)
                {
                    float rotation = i / (float)SEGMENTS * Math.Tau;
                    _vertices[i] = new Vector
                    {
                        X = 0.5f * Math.Cos(rotation),
                        Y = 0.5f * Math.Sin(rotation),
                        Z = 0.0f
                    };
                }

                _vertexArraySetup.VertexArrayObject.Bind();
                _vertexArraySetup.VertexBufferObject.SetData(_vertices, Vector.SizeInBytes);

                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vector.SizeInBytes, 0);
            }
        }

        public override void Draw()
        {
            DrawCircle(new Rotator(Math.PiOver2, 0.0f, 0.0f));
            DrawCircle(new Rotator(0.0f, Math.PiOver2, 0.0f));
            DrawCircle(new Rotator(0.0f, 0.0f, Math.PiOver2));
        }

        private void DrawCircle(Rotator rotation)
        {
            var model =
                Matrix4.CreateScale((Vector3)RelativeScale)
                * Matrix4.CreateRotationX(RelativeRotation.Roll + rotation.Roll)
                * Matrix4.CreateRotationY(RelativeRotation.Pitch + rotation.Pitch)
                * Matrix4.CreateRotationZ(RelativeRotation.Yaw + rotation.Yaw)
                * Matrix4.CreateTranslation((Vector3)RelativeLocation);

            _shader.SetColor(Color);
            _shader.SetModelMatrix(model);

            _vertexArraySetup.VertexArrayObject.Bind();
            _vertexArraySetup.VertexBufferObject.Bind();
            GL.DrawArrays(PrimitiveType.LineLoop, 0, _vertices.Length);
        }

        public override bool CollidesWith(IHitbox hitbox)
        {
            switch (hitbox)
            {
                case Sphere sphere:
                    return Vector.Distance(this, sphere) <= Math.Avg(Radius, sphere.Radius);

                case IObject3D object3D:
                    return Math.Abs(RelativeLocation.X - object3D.RelativeLocation.X) <= Math.Avg(Radius, object3D.RelativeScale.X)
                        && Math.Abs(RelativeLocation.Y - object3D.RelativeLocation.Y) <= Math.Avg(Radius, object3D.RelativeScale.Y)
                        && Math.Abs(RelativeLocation.Z - object3D.RelativeLocation.Z) <= Math.Avg(Radius, object3D.RelativeScale.Z);

                default:
                    return false;
            }
        }
    }
}