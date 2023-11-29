using Kotono.Graphics.Objects.Managers;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace Kotono.Graphics.Objects.Shapes
{
    public class Triangle : IShape3D
    {
        public Vector[] Vertices { get; }

        private Transform _transform;

        public Transform Transform
        {
            get => _transform;
            set => _transform = value;
        }

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

        public Color Color;

        public Matrix4 Model => Transform.Model;

        public bool IsDraw { get; private set; } = false;

        private int _vertexArrayObject;

        private int _vertexBufferObject;


        private bool _isInitBuffers = false;

        public Triangle()
        {
            Vertices = new Vector[]
            {
                Vector.Zero,
                Vector.Zero,
                Vector.Zero
            };
            _transform = new Transform();
            Color = Color.White;

            ObjectManager.Create(this);
        }

        public Triangle(Vector[] vertices, Transform transform, Color color)
        {
            Vertices = vertices;
            _transform = transform;
            Color = color;

            ObjectManager.Create(this);
        }

        public void Init()
        {

        }

        public void Update()
        {
            if (!_isInitBuffers && IsDraw)
            {
                _isInitBuffers = true;
                InitBuffers();
            }
        }

        public void UpdateShaders()
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

            ShaderManager.Hitbox.SetColor("color", Color);
            ShaderManager.Hitbox.SetMatrix4("model", model);

            GL.BindVertexArray(_vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.DrawArrays(PrimitiveType.LineLoop, 0, Vertices.Length);
        }

        private void InitBuffers()
        {
            // Create vertex array
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            // create vertex buffer
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * Vector.SizeInBytes, Vertices, BufferUsageHint.StaticDraw);

            int locationAttributeLocation = ShaderManager.Hitbox.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(locationAttributeLocation);
            GL.VertexAttribPointer(locationAttributeLocation, 3, VertexAttribPointerType.Float, false, Vector.SizeInBytes, 0);
        }

        public void Show()
        {
            IsDraw = true;
        }

        public void Hide()
        {
            IsDraw = false;
        }

        public Vector this[int index]
        {
            get => index switch
            {
                0 => Vertices[0],
                1 => Vertices[1],
                2 => Vertices[2],
                _ => throw new IndexOutOfRangeException("You tried to access this Triangle at index: " + index)
            };
            set
            {
                switch (index)
                {
                    case 0:
                        Vertices[0] = value;
                        break;
                    case 1:
                        Vertices[1] = value;
                        break;
                    case 2:
                        Vertices[2] = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("You tried to set this Triangle at index: " + index);
                }
            }
        }

        public void Save()
        {

        }

        public void Delete()
        {
            ObjectManager.Delete(this);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
