using Kotono.Graphics.Shaders;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects.Hitboxes
{
    internal sealed class Sphere : Hitbox
    {
        private const int SEGMENTS = 64;

        private static readonly Vector[] _vertices = new Vector[SEGMENTS];

        private static readonly VertexArraySetup _vertexArraySetup = new();

        public override Shader Shader => HitboxShader.Instance;

        internal float Radius => RelativeScale.X;

        static Sphere()
        {
            for (int i = 0; i < SEGMENTS; i++)
            {
                float rotation = i / (float)SEGMENTS * Math.Tau;
                _vertices[i] = new Vector(
                    0.5f * Math.Cos(rotation),
                    0.5f * Math.Sin(rotation),
                    0.0f
                );
            }

            _vertexArraySetup.SetData(_vertices, Vector.SizeInBytes);
            _vertexArraySetup.VertexArrayObject.SetVertexAttributesLayout(HitboxShader.Instance);
        }

        public override void UpdateShader()
        {
            if (Shader is HitboxShader hitboxShader)
            {
                hitboxShader.SetColor(Color);
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
                Matrix4.CreateScale((Vector3)WorldScale)
                * Matrix4.CreateRotationX(WorldRotation.Roll + rotation.Roll)
                * Matrix4.CreateRotationY(WorldRotation.Pitch + rotation.Pitch)
                * Matrix4.CreateRotationZ(WorldRotation.Yaw + rotation.Yaw)
                * Matrix4.CreateTranslation((Vector3)WorldLocation);

            if (Shader is HitboxShader hitboxShader)
            {
                hitboxShader.SetModel(model);
            }

            _vertexArraySetup.VertexArrayObject.Bind();
            GL.DrawArrays(PrimitiveType.LineLoop, 0, _vertices.Length);
        }

        public override bool CollidesWith(IHitbox hitbox)
        {
            switch (hitbox)
            {
                case Sphere sphere:
                    return Vector.Distance(this, sphere) <= Math.Half(Radius + sphere.Radius);

                case IObject3D object3D:
                    return Math.Abs(RelativeLocation.X - object3D.RelativeLocation.X) <= Math.Half(Radius + object3D.RelativeScale.X)
                        && Math.Abs(RelativeLocation.Y - object3D.RelativeLocation.Y) <= Math.Half(Radius + object3D.RelativeScale.Y)
                        && Math.Abs(RelativeLocation.Z - object3D.RelativeLocation.Z) <= Math.Half(Radius + object3D.RelativeScale.Z);

                default:
                    return false;
            }
        }
    }
}