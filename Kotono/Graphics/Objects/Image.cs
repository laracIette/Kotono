using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects
{
    public class Image
    {
        private static readonly float[] _vertices =
        {           
            // positions   // texCoords
            -1.0f,  1.0f,  0.0f, 1.0f, 
            -1.0f, -1.0f,  0.0f, 0.0f, 
             1.0f, -1.0f,  1.0f, 0.0f, 

            -1.0f,  1.0f,  0.0f, 1.0f, 
             1.0f, -1.0f,  1.0f, 0.0f, 
             1.0f,  1.0f,  1.0f, 1.0f  
        }; 

        private static int _vertexArrayObject; 

        private static int _vertexBufferObject;

        private static bool _isFirst = true;

        public Image(string path, Rect dest) 
        {
            if (_isFirst)
            {
                _isFirst = false;

                // create vertex array
                _vertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(_vertexArrayObject);

                // vertex buffer
                _vertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * _vertices.Length, _vertices, BufferUsageHint.StaticDraw);

                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
            }
        }
    }
}
