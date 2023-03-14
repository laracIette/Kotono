using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

using Kotono.Utils;

namespace Kotono.Graphics
{
    public class Camera
    {
        private Vector3 _front = -Vector3.UnitZ;

        private Vector3 _up = Vector3.UnitY;

        private Vector3 _right = Vector3.UnitX;

        private float _pitch;

        private float _yaw = -MathHelper.PiOver2;

        private float _fov = MathHelper.PiOver2;

        private float _sensitivity;

        private float _speed;

        public Camera()
        {
            _speed = 1.5f;
            _sensitivity = 0.2f;
        }

        public Vector3 Position { get; set; }

        public float AspectRatio { private get; set; }

        public Vector3 Front => _front;

        public Vector3 Up => _up;

        public Vector3 Right => _right;

        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(_pitch);

            set
            {
                value = MathHelper.Clamp(value, -89.0f, 89.0f);
                _pitch = MathHelper.DegreesToRadians(value);
                UpdateVectors();
            }
        }

        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(_yaw);

            set
            {
                _yaw = MathHelper.DegreesToRadians(value);
                UpdateVectors();
            }
        }

        public float Fov
        {
            get => MathHelper.RadiansToDegrees(_fov);

            set
            {
                value = MathHelper.Clamp(value, 1.0f, 90.0f);
                _fov = MathHelper.DegreesToRadians(value);
            }
        }
        public Matrix4 ViewMatrix => Matrix4.LookAt(Position, Position + _front, _up);

        public Matrix4 ProjectionMatrix => Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.01f, 100f);

        public void Move()
        {
            if (InputManager.KeyboardState.IsKeyDown(Keys.LeftShift))
            {
                _speed = 3.0f;
            }

            if (InputManager.KeyboardState.IsKeyDown(Keys.W))
            {
                Position += Front * _speed * Time.Delta; // Forward
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.S))
            {
                Position -= Front * _speed * Time.Delta; // Backwards
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.A))
            {
                Position -= Right * _speed * Time.Delta; // Left
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.D))
            {
                Position += Right * _speed * Time.Delta; // Right
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.Space))
            {
                Position += Up * _speed * Time.Delta; // Up
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.LeftControl))
            {
                Position -= Up * _speed * Time.Delta; // Down
            }

            Yaw += InputManager.MouseState.Delta.X * _sensitivity;
            Pitch -= InputManager.MouseState.Delta.Y * _sensitivity;
        }

        private void UpdateVectors()
        {
            _front.X = MathF.Cos(_pitch) * MathF.Cos(_yaw);
            _front.Y = MathF.Sin(_pitch);
            _front.Z = MathF.Cos(_pitch) * MathF.Sin(_yaw);

            _front = Vector3.Normalize(_front);
            _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.Cross(_right, _front));
        }

    }
}