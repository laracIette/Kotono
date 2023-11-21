using Kotono.Graphics.Objects.Managers;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using Math = Kotono.Utils.Math;

namespace Kotono.Graphics.Objects.Hitboxes
{
    public class Sphere : IHitbox
    {
        private const int SEGMENTS = 64;

        private static readonly Vector[] _vertices = new Vector[SEGMENTS];

        private static int _vertexArrayObject;

        private static int _vertexBufferObject;

        private static bool _isFirst = true;

        public bool IsDraw { get; private set; } = true;

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

        public float Radius => Scale.X;

        public Vector Velocity { get; set; }

        public Color Color { get; set; } = Color.White;

        public List<Sphere> Collisions { get; set; } = new();

        public Sphere()
        {
            if (_isFirst)
            {
                _isFirst = false;

                for (int i = 0; i < SEGMENTS; i++)
                {
                    float rotation = i / (float)SEGMENTS * MathHelper.TwoPi;
                    _vertices[i] = new Vector
                    {
                        X = 0.5f * Math.Cos(rotation),
                        Y = 0.5f * Math.Sin(rotation),
                        Z = 0f
                    };
                }

                // Create vertex array
                _vertexArrayObject = GL.GenVertexArray();
                GL.BindVertexArray(_vertexArrayObject);

                // create vertex buffer
                _vertexBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
                GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * Vector.SizeInBytes, _vertices, BufferUsageHint.StaticDraw);

                int locationAttributeLocation = ShaderManager.Hitbox.GetAttribLocation("aPos");
                GL.EnableVertexAttribArray(locationAttributeLocation);
                GL.VertexAttribPointer(locationAttributeLocation, 3, VertexAttribPointerType.Float, false, Vector.SizeInBytes, 0);
            }

            _transform = new Transform();

            ObjectManager.Create(this);
        }

        public void Init() { }

        public void Update()
        {
            Location += Velocity * Time.DeltaS;
        }

        public void UpdateShaders()
        {

        }

        public void Draw()
        {
            DrawCircle(new Vector(MathHelper.PiOver2, 0f, 0f));
            DrawCircle(new Vector(0f, MathHelper.PiOver2, 0f));
            DrawCircle(new Vector(0f, 0f, MathHelper.PiOver2));
        }

        private void DrawCircle(Vector rotation)
        {
            var model =
                Matrix4.CreateScale((Vector3)Scale)
                * Matrix4.CreateRotationX(Rotation.X + rotation.X)
                * Matrix4.CreateRotationY(Rotation.Y + rotation.Y)
                * Matrix4.CreateRotationZ(Rotation.Z + rotation.Z)
                * Matrix4.CreateTranslation((Vector3)Location);

            ShaderManager.Hitbox.SetColor("color", Color);
            ShaderManager.Hitbox.SetMatrix4("model", model);

            GL.BindVertexArray(_vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.DrawArrays(PrimitiveType.LineLoop, 0, _vertices.Length);
        }

        public bool Collides(IHitbox h)
        {
            return (Math.Abs(Location.X - h.Location.X) < (Scale.X + h.Scale.X) / 2)
                && (Math.Abs(Location.Y - h.Location.Y) < (Scale.Y + h.Scale.Y) / 2)
                && (Math.Abs(Location.Z - h.Location.Z) < (Scale.Z + h.Scale.Z) / 2);
        }

        public bool Collides(Sphere s)
        {
            return Vector.Distance(this, s) < (Scale.X + s.Scale.X) / 2;
        }

        public bool IsColliding => Collisions.Any(Collides);

        public void Save()
        {

        }

        public void Show()
        {
            IsDraw = true;
        }

        public void Hide()
        {
            IsDraw = false;
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