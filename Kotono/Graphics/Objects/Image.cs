using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

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

        private readonly int _texture;

        private Rect _dest;

        private Rect _transformation;

        private double _startTime = 0f;

        private double _endTime = 0f;

        public Rect Dest => _dest;

        private Matrix4 Model =>
            Matrix4.Identity
            * Matrix4.CreateScale(Dest.Normalized.W / 2.0f, Dest.Normalized.H / 2.0f, 1.0f)
            * Matrix4.CreateTranslation(Dest.Normalized.X, Dest.Normalized.Y, 0.0f);

        public bool IsDraw { get; set; } = true;

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

            _dest = dest;

            _transformation = new Rect();

            _texture = TextureManager.LoadTexture(path);
        }

        public void Update()
        {
            if (Time.NowS < _endTime)
            {
                _dest += _transformation * Time.DeltaS;
            }

            // check if Image is out of screen bounds
            Show();
            if (((_dest.X + _dest.W) < 0) || (_dest.X > KT.Width) || ((_dest.Y + _dest.H) < 0) || (_dest.Y > KT.Height))
            {
                Hide();
            }

        }

        public void UpdateShaders()
        {

        }

        public void Draw()
        {
            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            TextureManager.UseTexture(_texture, TextureUnit.Texture0);
            
            KT.SetShaderMatrix4(ShaderType.Image, "model", Model);

            GL.BindVertexArray(_vertexArrayObject);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

            GL.Enable(EnableCap.DepthTest);
        }

        public void Transform(Rect transformation, double time)
        {
            _transformation = transformation / (float)time;

            _startTime = Time.NowS;
            _endTime = _startTime + time;
        }

        public void TransformTo(Rect dest, double time)
        {
            Transform(dest - _dest, time);
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
