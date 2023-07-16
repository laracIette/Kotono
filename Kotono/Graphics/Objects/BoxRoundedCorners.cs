using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    public class BoxRoundedCorners
    {
        private static readonly float[] _vertices =
        {           
            // locations   // texCoords
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

        public Rect Dest { get; set; }

        public Color Color { get; set; }

        public float FallOff { get; set; } = 20;

        public bool IsDraw { get; private set; } = true;
        
        private Matrix4 Model =>
            Matrix4.CreateScale((Dest + new Rect(w: FallOff * 2)).WorldSpace.W, (Dest + new Rect(h: FallOff * 2)).WorldSpace.H, 1.0f)
            * Matrix4.CreateTranslation(Dest.WorldSpace.X, Dest.WorldSpace.Y, 0.0f);

        public BoxRoundedCorners(Rect dest, Color color) 
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

            Dest = dest;
            Color = color;

            KT.CreateBoxRoundedCorners(this);
        }

        public void Init()
        {

        }

        public void Update()
        {

        }

        public void UpdateShaders()
        {

        }

        public void Draw()
        {
            KT.SetShaderMatrix4(ShaderType.BoxRoundedCorners, "model", Model);
            KT.SetShaderColor(ShaderType.BoxRoundedCorners, "color", Color);
            KT.SetShaderRect(ShaderType.BoxRoundedCorners, "dest", Dest);
            KT.SetShaderFloat(ShaderType.BoxRoundedCorners, "fallOff", FallOff);

            GL.BindVertexArray(_vertexArrayObject);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

            GL.Enable(EnableCap.DepthTest);
        }
    }
}
