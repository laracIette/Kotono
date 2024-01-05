using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Math = Kotono.Utils.Math;

namespace Kotono.Graphics.Objects.Hitboxes
{
    public class Sphere : Hitbox
    {
        private const int SEGMENTS = 64;

        private static readonly Vector[] _vertices = new Vector[SEGMENTS];

        private static int _vertexArrayObject;

        private static int _vertexBufferObject;

        private static bool _isFirst = true;

        public float Radius => Scale.X;

        public Vector Velocity { get; set; }

        public Sphere()
            : base()
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
                        Z = 0.0f
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
        }

        public override void Update()
        {
            Location += Velocity * Time.DeltaS;
        }

        public override void Draw()
        {
            DrawCircle(new Vector(MathHelper.PiOver2, 0.0f, 0.0f));
            DrawCircle(new Vector(0.0f, MathHelper.PiOver2, 0.0f));
            DrawCircle(new Vector(0.0f, 0.0f, MathHelper.PiOver2));
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

        public override bool CollidesWith(Hitbox hitbox)
        {
            switch (hitbox)
            {
                case Sphere sphere:
                    return Vector.Distance(this, sphere) < (Radius + sphere.Radius) / 2.0f;
                
                default:
                    return Math.Abs(Location.X - hitbox.Location.X) <= (Scale.X + hitbox.Scale.X) / 2.0f
                        && Math.Abs(Location.Y - hitbox.Location.Y) <= (Scale.Y + hitbox.Scale.Y) / 2.0f
                        && Math.Abs(Location.Z - hitbox.Location.Z) <= (Scale.Z + hitbox.Scale.Z) / 2.0f;
            }
        }
    }
}