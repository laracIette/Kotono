using Kotono.Graphics.Shaders;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects.Hitboxes
{
    internal sealed class Box : Hitbox
    {
        private static readonly float[] _vertices =
        [
            -0.5f,
            -0.5f,
            -0.5f,
            0.5f,
            -0.5f,
            -0.5f,

            0.5f,
            -0.5f,
            -0.5f,
            0.5f,
            0.5f,
            -0.5f,

            0.5f,
            0.5f,
            -0.5f,
            -0.5f,
            0.5f,
            -0.5f,

            -0.5f,
            0.5f,
            -0.5f,
            -0.5f,
            -0.5f,
            -0.5f,


            -0.5f,
            -0.5f,
            0.5f,
            0.5f,
            -0.5f,
            0.5f,

            0.5f,
            -0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,

            0.5f,
            0.5f,
            0.5f,
            -0.5f,
            0.5f,
            0.5f,

            -0.5f,
            0.5f,
            0.5f,
            -0.5f,
            -0.5f,
            0.5f,


            -0.5f,
            -0.5f,
            0.5f,
            -0.5f,
            -0.5f,
            -0.5f,

            0.5f,
            -0.5f,
            0.5f,
            0.5f,
            -0.5f,
            -0.5f,

            0.5f,
            0.5f,
            0.5f,
            0.5f,
            0.5f,
            -0.5f,

            -0.5f,
            0.5f,
            0.5f,
            -0.5f,
            0.5f,
            -0.5f
        ];

        private static readonly VertexArraySetup _vertexArraySetup = new();

        public override Shader Shader => HitboxShader.Instance;

        static Box()
        {
            _vertexArraySetup.SetData(_vertices, sizeof(float));
            _vertexArraySetup.VertexArrayObject.SetVertexAttributesLayout(HitboxShader.Instance);
        }

        public override void UpdateShader()
        {
            if (Shader is HitboxShader hitboxShader)
            {
                hitboxShader.SetColor(Color);
                hitboxShader.SetModel(Transform.Model);
            }
        }

        public override void Draw()
        {
            _vertexArraySetup.VertexArrayObject.Bind();
            GL.DrawArrays(PrimitiveType.Lines, 0, _vertices.Length);
        }

        public override bool CollidesWith(IHitbox hitbox)
        {
            return (hitbox is IObject3D object3D)
                && (Math.Abs(RelativeLocation.X - object3D.RelativeLocation.X) <= Math.Half(RelativeScale.X + object3D.RelativeScale.X))
                && (Math.Abs(RelativeLocation.Y - object3D.RelativeLocation.Y) <= Math.Half(RelativeScale.Y + object3D.RelativeScale.Y))
                && (Math.Abs(RelativeLocation.Z - object3D.RelativeLocation.Z) <= Math.Half(RelativeScale.Z + object3D.RelativeScale.Z));
        }
    }
}