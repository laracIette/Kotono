using Kotono.Graphics.Objects.Shapes;
using Kotono.Input;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using CursorState = Kotono.Input.CursorState;
using Math = Kotono.Utils.Math;

namespace Kotono.Graphics.Objects
{
    internal class Camera : Object
    {
        private float _pitch = 0.0f;

        private float _yaw = -Math.PI / 2.0f;

        private float _fov = 90.0f;

        private float _speed = 1.0f;

        private readonly Line _line;

        internal Vector Front { get; private set; } = -Vector.UnitZ;

        internal Vector Up { get; private set; } = Vector.UnitY;

        internal Vector Right { get; private set; } = Vector.UnitX;

        internal Vector Location { get; set; } = Vector.Zero;

        internal Vector Rotation => new(Pitch, -Yaw - Math.PI / 2.0f, 0.0f);

        internal float AspectRatio { get; set; } = 16.0f / 9.0f;

        internal float Pitch
        {
            get => _pitch;
            set
            {
                _pitch = Math.Clamp(value, Math.Rad(-89.0f), Math.Rad(89.0f));
                UpdateVectors();
            }
        }

        internal float Yaw
        {
            get => _yaw;
            set
            {
                _yaw = value;
                UpdateVectors();
            }
        }

        internal float Fov
        {
            get => _fov;
            set => _fov = Math.Clamp(value, 1.0f, 90.0f);
        }

        internal Matrix4 ViewMatrix => Matrix4.LookAt((Vector3)Location, (Vector3)(Location + Front), (Vector3)Up);

        internal Matrix4 ProjectionMatrix => Matrix4.CreatePerspectiveFieldOfView(Math.Rad(Fov), AspectRatio, 0.01f, 1000.0f);

        internal Camera() : base()
        {
            _line = new Line(Location, Front, Transform.Default, Color.Red)
            {
                IsDraw = false
            };
        }

        public override void Update()
        {
            Move();

            _line.Location = Location;
            _line.Rotation = Rotation;
        }

        private void Move()
        {
            if (Mouse.CursorState != CursorState.Centered)
            {
                return;
            }

            float sensitivity = 0.005f;

            Yaw += Mouse.Delta.X * sensitivity;
            Pitch -= Mouse.Delta.Y * sensitivity;

            _speed += Mouse.ScrollDelta.Y * _speed / 10.0f;
            _speed = Math.Clamp(_speed, 0.1f, 100.0f);

            float fast = Keyboard.IsKeyDown(Keys.LeftShift) ? 2.0f : 1.0f;
            float slow = Keyboard.IsKeyDown(Keys.LeftControl) ? 0.5f : 1.0f;

            float speed = _speed * fast * slow * Time.Delta;

            if (Keyboard.IsKeyDown(Keys.W))
            {
                Location += speed * Front; // Forward
            }
            if (Keyboard.IsKeyDown(Keys.S))
            {
                Location -= speed * Front; // Backwards
            }
            if (Keyboard.IsKeyDown(Keys.A))
            {
                Location -= speed * Right; // Left
            }
            if (Keyboard.IsKeyDown(Keys.D))
            {
                Location += speed * Right; // Right
            }
            if (Keyboard.IsKeyDown(Keys.E))
            {
                Location += speed * Up; // Up
            }
            if (Keyboard.IsKeyDown(Keys.Q))
            {
                Location -= speed * Up; // Down
            }

        }

        private void UpdateVectors()
        {
            Front = new Vector(
                Math.Cos(Pitch) * Math.Cos(Yaw),
                Math.Sin(Pitch),
                Math.Cos(Pitch) * Math.Sin(Yaw)
            ).Normalized;
            Right = Vector.Cross(Front, Vector.UnitY).Normalized;
            Up = Vector.Cross(Right, Front).Normalized;
        }

    }
}