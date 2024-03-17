using Kotono.Graphics.Shaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

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

        private static int _vertexArrayObject;

        private static int _vertexBufferObject;

        private static bool _isFirst = true;

        internal Box(HitboxSettings settings)
            : base(settings)
        {
            if (_isFirst)
            {
                _isFirst = false;

                // Create vertex array
                _vertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(_vertexArrayObject);

                // Create vertex buffer
                _vertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

                int locationAttributeLocation = ShaderManager.Hitbox.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(locationAttributeLocation);
                GL.VertexAttribPointer(locationAttributeLocation, 3, VertexAttribPointerType.Float, false, 0, 0);
            }
        }

        public override void Draw()
        {
            ShaderManager.Hitbox.SetColor("color", Color);
            ShaderManager.Hitbox.SetMatrix4("model", Transform.Model);

            GL.BindVertexArray(_vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.DrawArrays(PrimitiveType.Lines, 0, _vertices.Length);
        }

        public override bool CollidesWith(IHitbox hitbox)
        {
            return (hitbox is IObject3D object3D)
                && (Math.Abs(RelativeLocation.X - object3D.RelativeLocation.X) <= Math.Avg(RelativeScale.X, object3D.RelativeScale.X))
                && (Math.Abs(RelativeLocation.Y - object3D.RelativeLocation.Y) <= Math.Avg(RelativeScale.Y, object3D.RelativeScale.Y))
                && (Math.Abs(RelativeLocation.Z - object3D.RelativeLocation.Z) <= Math.Avg(RelativeScale.Z, object3D.RelativeScale.Z));
        }
    }
}