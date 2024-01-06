using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Math = Kotono.Utils.Math;

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

        internal Box()
            : base()
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
            var model =
                Matrix4.CreateScale((Vector3)Scale)
                * Matrix4.CreateRotationX(Rotation.X)
                * Matrix4.CreateRotationY(Rotation.Y)
                * Matrix4.CreateRotationZ(Rotation.Z)
                * Matrix4.CreateTranslation((Vector3)Location);

            ShaderManager.Hitbox.SetColor("color", Color);
            ShaderManager.Hitbox.SetMatrix4("model", model);

            GL.BindVertexArray(_vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.DrawArrays(PrimitiveType.Lines, 0, _vertices.Length);
        }

        public override bool CollidesWith(Hitbox hitbox)
        {
            return (hitbox is IObject3D object3D)
                && (Math.Abs(Location.X - object3D.Location.X) <= (Scale.X + object3D.Scale.X) / 2.0f)
                && (Math.Abs(Location.Y - object3D.Location.Y) <= (Scale.Y + object3D.Scale.Y) / 2.0f)
                && (Math.Abs(Location.Z - object3D.Location.Z) <= (Scale.Z + object3D.Scale.Z) / 2.0f);
        }
    }
}