using Kotono.Graphics.Objects.Shapes;
using Kotono.Input;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using CursorState = Kotono.Input.CursorState;

namespace Kotono.Graphics.Objects
{
    internal sealed class Camera : Object3D
    {
        internal static Camera Active { get; set; } = new();

        private float _pitch = 0.0f;

        private float _yaw = -Math.PiOver2;

        private float _fov = 90.0f;

        private float _speed = 1.0f;

        private readonly Line _line;

        internal Vector Front { get; private set; } = -Vector.UnitZ;

        internal Vector Up { get; private set; } = Vector.UnitY;

        internal Vector Right { get; private set; } = Vector.UnitX;

        public override Rotator RelativeRotation => new(Pitch, -Yaw - Math.PiOver2, 0.0f);

        internal float AspectRatio => Viewport.WorldSize.Ratio;

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
            set => _fov = Math.Clamp(value, 1.0f, 170.0f);
        }

        internal Matrix4 ViewMatrix => Matrix4.LookAt((Vector3)WorldLocation, (Vector3)(WorldLocation + Front), (Vector3)Up);

        internal Matrix4 ProjectionMatrix => Matrix4.CreatePerspectiveFieldOfView(Math.Rad(Fov), AspectRatio, 0.01f, 1000.0f);

        internal Camera()
        {
            _line = new Line(WorldLocation, Front)
            {
                IsDraw = false,
                Color = Color.Red,
                Parent = this
            };
        }

        public override void Update() => Move();

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
                WorldLocation += speed * Front;
            }
            if (Keyboard.IsKeyDown(Keys.S))
            {
                WorldLocation -= speed * Front; 
            }
            if (Keyboard.IsKeyDown(Keys.A))
            {
                WorldLocation -= speed * Right; 
            }
            if (Keyboard.IsKeyDown(Keys.D))
            {
                WorldLocation += speed * Right; 
            }
            if (Keyboard.IsKeyDown(Keys.E))
            {
                WorldLocation += speed * Up; 
            }
            if (Keyboard.IsKeyDown(Keys.Q))
            {
                WorldLocation -= speed * Up;
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

        private void OnXKeyDown() => Fov -= Time.Delta * 50.0f;

        private void OnCKeyDown() => Fov += Time.Delta * 50.0f;

        public override void Dispose()
        {
            _line.Dispose();

            base.Dispose();
        }
    }
}