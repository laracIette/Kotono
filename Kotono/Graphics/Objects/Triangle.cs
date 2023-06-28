using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Kotono.Graphics.Objects
{
    public class Triangle
    {
        public Vector Vertex1;

        public Vector Vertex2;

        public Vector Vertex3;

        private Transform _transform;

        public Transform Transform => _transform;

        public Vector Location
        {
            get => _transform.Location;
            set => _transform.Location = value;
        }

        public Vector Rotation
        {
            get => _transform.Rotation;
            set => _transform.Rotation = value;
        }

        public Vector Scale
        {
            get => _transform.Scale;
            set => _transform.Scale = value;
        }

        public Vector Color;

        public bool IsDraw = true;

        private int _vertexArrayObject;

        private int _vertexBufferObject;

        private readonly Vector[] _vertices = new Vector[3];

        public Triangle()
        {
            Vertex1 = Vector.Zero;
            Vertex2 = Vector.Zero;
            Vertex3 = Vector.Zero;
            _transform = new Transform();
            Color = Vector.White;
            InitBuffers();
        }

        public Triangle(Vector vertex1, Vector vertex2, Vector vertex3, Transform transform, Vector color)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Vertex3 = vertex3;
            _transform = transform;
            Color = color;
            InitBuffers();
        }

        private void InitBuffers()
        {
            _vertices[0] = Vertex1;
            _vertices[1] = Vertex2;
            _vertices[2] = Vertex3;

            // Create vertex array
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            // create vertex buffer
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * Vector.SizeInBytes, _vertices, BufferUsageHint.StaticDraw);

            int locationAttributeLocation = KT.GetShaderAttribLocation(ShaderType.Hitbox, "aPos");
            GL.EnableVertexAttribArray(locationAttributeLocation);
            GL.VertexAttribPointer(locationAttributeLocation, 3, VertexAttribPointerType.Float, false, Vector.SizeInBytes, 0);
        }

        public void Init()
        {

        }

        public void Update()
        {

        }

        public void Draw()
        {
            var model =
                Matrix4.CreateScale((Vector3)Scale)
                * Matrix4.CreateRotationX(Rotation.X)
                * Matrix4.CreateRotationY(Rotation.Y)
                * Matrix4.CreateRotationZ(Rotation.Z)
                * Matrix4.CreateTranslation((Vector3)Location);

            KT.SetShaderVector(ShaderType.Hitbox, "color", Color);
            KT.SetShaderMatrix4(ShaderType.Hitbox, "model", model);

            GL.BindVertexArray(_vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.DrawArrays(PrimitiveType.LineLoop, 0, _vertices.Length);
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
