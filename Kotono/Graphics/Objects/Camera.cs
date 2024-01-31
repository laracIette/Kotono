using Kotono.Graphics.Objects.Managers;
using Kotono.Input;
using Kotono.Utils;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using CursorState = Kotono.Input.CursorState;
using Math = Kotono.Utils.Math;

namespace Kotono.Graphics.Objects
{
    internal class Camera
    {
        private float _pitch = 0.0f;

        private float _yaw = -Math.PI / 2.0f;

        private float _fov = Math.PI / 2.0f;

        private float _speed = 1.0f;

        internal Vector Front { get; private set; } = -Vector.UnitZ;

        internal Vector Up { get; private set; } = Vector.UnitY;

        internal Vector Right { get; private set; } = Vector.UnitX;

        internal Vector Location { get; private set; } = Vector.Zero;

        internal float AspectRatio { get; set; } = 16.0f / 9.0f;

        internal float Pitch
        {
            get => Math.Deg(_pitch);
            set
            {
                value = Math.Clamp(value, -89.0f, 89.0f);
                _pitch = Math.Rad(value);
                UpdateVectors();
            }
        }

        internal float Yaw
        {
            get => Math.Deg(_yaw);
            set
            {
                _yaw = Math.Rad(value);
                UpdateVectors();
            }
        }

        internal float Fov
        {
            get => Math.Deg(_fov);
            set
            {
                value = Math.Clamp(value, 1.0f, 90.0f);
                _fov = Math.Rad(value);
            }
        }
        internal Matrix4 ViewMatrix => Matrix4.LookAt((Vector3)Location, (Vector3)(Location + Front), (Vector3)Up);

        internal Matrix4 ProjectionMatrix => Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.01f, 1000.0f);

        internal Camera()
        {
            CameraManager.Create(this);
        }

        internal void Update()
        {
            Move();
        }

        private void Move()
        {
            if (Mouse.CursorState != CursorState.Centered)
            {
                return;
            }

            float sensitivity = 0.2f;

            Yaw += Mouse.Delta.X * sensitivity;
            Pitch -= Mouse.Delta.Y * sensitivity;
            _speed += Mouse.ScrollDelta.Y * _speed / 10.0f;
            _speed = Math.Clamp(_speed, 0.1f, 100.0f);

            float fast = Keyboard.IsKeyDown(Keys.LeftShift) ? 2.0f : 1.0f;
            float slow = Keyboard.IsKeyDown(Keys.LeftControl) ? 0.5f : 1.0f;

            if (Keyboard.IsKeyDown(Keys.W))
            {
                Location += Front * _speed * fast * slow * Time.Delta; // Forward
            }
            if (Keyboard.IsKeyDown(Keys.S))
            {
                Location -= Front * _speed * fast * slow * Time.Delta; // Backwards
            }
            if (Keyboard.IsKeyDown(Keys.A))
            {
                Location -= Right * _speed * fast * slow * Time.Delta; // Left
            }
            if (Keyboard.IsKeyDown(Keys.D))
            {
                Location += Right * _speed * fast * slow * Time.Delta; // Right
            }
            if (Keyboard.IsKeyDown(Keys.E))
            {
                Location += Up * _speed * fast * slow * Time.Delta; // Up
            }
            if (Keyboard.IsKeyDown(Keys.Q))
            {
                Location -= Up * _speed * fast * slow * Time.Delta; // Down
            }

        }

        private void UpdateVectors()
        {
            Front = new Vector
            {
                X = Math.Cos(_pitch) * Math.Cos(_yaw),
                Y = Math.Sin(_pitch),
                Z = Math.Cos(_pitch) * Math.Sin(_yaw)
            };

            Front = Front.Normalized;
            Right = Vector.Cross(Front, Vector.UnitY).Normalized;
            Up = Vector.Cross(Right, Front).Normalized;
        }

    }
}