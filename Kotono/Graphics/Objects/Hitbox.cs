using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    public class Hitbox
    {
        private readonly float[] _vertices =
        {
            -0.5f, -0.5f, -0.5f,
             0.5f, -0.5f, -0.5f,

             0.5f, -0.5f, -0.5f,
             0.5f,  0.5f, -0.5f,

             0.5f,  0.5f, -0.5f,
            -0.5f,  0.5f, -0.5f,

            -0.5f,  0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,


            -0.5f, -0.5f,  0.5f,
             0.5f, -0.5f,  0.5f,

             0.5f, -0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,

             0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,

            -0.5f,  0.5f,  0.5f,
            -0.5f, -0.5f,  0.5f,


            -0.5f, -0.5f,  0.5f,
            -0.5f, -0.5f, -0.5f,

             0.5f, -0.5f,  0.5f,
             0.5f, -0.5f, -0.5f,

             0.5f,  0.5f,  0.5f,
             0.5f,  0.5f, -0.5f,

            -0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f, -0.5f
        };

        private Vector3 _position;

        private Vector3 _angle;

        private Vector3 _scale;

        private Vector3 _color;

        private readonly int _vertexArrayObject;

        private readonly int _vertexBufferObject;

        public Hitbox(Vector3 position, Vector3 angle, Vector3 scale, Vector3 color)
        {
            _position = position;
            _angle = angle;
            _scale = scale;
            _color = color;

            // Create vertex array
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            // create vertex buffer
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            int positionAttributeLocation = ShaderManager.Hitbox.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(positionAttributeLocation);
            GL.VertexAttribPointer(positionAttributeLocation, 3, VertexAttribPointerType.Float, false, 0, 0);
        }

        public void Draw()
        {
            ShaderManager.Hitbox.SetVector3("color", _color);

            ShaderManager.Hitbox.SetMatrix4("model", Model);
            ShaderManager.Hitbox.SetMatrix4("view", CameraManager.Main.ViewMatrix);
            ShaderManager.Hitbox.SetMatrix4("projection", CameraManager.Main.ProjectionMatrix);

            GL.BindVertexArray(_vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.DrawArrays(PrimitiveType.Lines, 0, _vertices.Length);
        }

        public Matrix4 Model =>
            Matrix4.CreateScale(_scale)
            * Matrix4.CreateRotationX(_angle.X)
            * Matrix4.CreateRotationY(_angle.Y)
            * Matrix4.CreateRotationZ(_angle.Z)
            * Matrix4.CreateTranslation(_position);
    }
}
