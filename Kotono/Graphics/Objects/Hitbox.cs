using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    public class Hitbox
    {
        private Vector3 _position;

        private Vector3 _angle;

        private Vector3 _color;

        private readonly int _vertexArrayObject;

        private readonly int _vertexBufferObject;

        private readonly float[] vertices = 
        { 
            -1.0f, 0.0f, 0.0f, 
             1.0f, 0.0f, 0.0f  
        }; 

        public Hitbox(Vector3 position, Vector3 angle, Vector3 color)
        {
            _position = position;
            _angle = angle;
            _color = color;

            // Create vertex array
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            // create vertex buffer
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            int positionAttributeLocation = ShaderManager.Hitbox.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(positionAttributeLocation);
            GL.VertexAttribPointer(positionAttributeLocation, 3, VertexAttribPointerType.Float, false, 0, 0);
        }

        public void Draw()
        {
            ShaderManager.Hitbox.SetVector3("color", _color);

            ShaderManager.Hitbox.SetMatrix4("model", Matrix4.Identity);
            ShaderManager.Hitbox.SetMatrix4("view", CameraManager.Main.ViewMatrix);
            ShaderManager.Hitbox.SetMatrix4("projection", CameraManager.Main.ProjectionMatrix);

            GL.BindVertexArray(_vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.DrawArrays(PrimitiveType.Lines, 0, 2);
        }
    }
}
