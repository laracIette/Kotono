using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    public class RoundedBox
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

        public float FallOff { get; set; } 

        public float CornerSize { get; set; } 

        public bool IsDraw { get; private set; } = true;
        
        private Matrix4 Model =>
            Matrix4.CreateScale((Dest + new Rect(w: FallOff * 2)).WorldSpace.W, (Dest + new Rect(h: FallOff * 2)).WorldSpace.H, 1.0f)
            * Matrix4.CreateTranslation(Dest.WorldSpace.X, Dest.WorldSpace.Y, 0.0f);

        public RoundedBox(Rect dest, Color color, float fallOff, float cornerSize) 
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
            FallOff = fallOff;
            CornerSize = cornerSize;

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
            KT.SetShaderMatrix4(ShaderType.RoundedBox, "model", Model);
            KT.SetShaderColor(ShaderType.RoundedBox, "color", Color);
            KT.SetShaderRect(ShaderType.RoundedBox, "dest", new Rect(Dest.X, KT.Size.Y - Dest.Y, Dest.W, Dest.H));
            KT.SetShaderFloat(ShaderType.RoundedBox, "fallOff", FallOff);
            KT.SetShaderFloat(ShaderType.RoundedBox, "cornerSize", CornerSize);

            GL.BindVertexArray(_vertexArrayObject);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }

        public void Show()
        {
            IsDraw = true;
        }

        public void Hide()
        {
            IsDraw = false;
        }
    }
}
