using Kotono.Graphics.Shaders;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects.Hitboxes
{
    internal class Box : Hitbox
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

        private static readonly Object3DShader _shader = (Object3DShader)ShaderManager.Shaders["hitbox"];

        static Box()
        {
            _vertexArraySetup.SetData(_vertices, sizeof(float));

            Shader.SetVertexAttributeData(0, 3, VertexAttribPointerType.Float, 0, 0);
        }

        public override void Draw()
        {
            _shader.SetColor(Color);
            _shader.SetModelMatrix(Transform.Model);

            _vertexArraySetup.Bind();
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