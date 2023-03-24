using Kotono.Utils;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace Kotono.Graphics
{
    public class Camera
    {
        private float _pitch = 0.0f;

        private float _yaw = -MathHelper.PiOver2;

        private float _fov = MathHelper.PiOver2;

        public Vector3 Front { get; private set; } = -Vector3.UnitZ;

        public Vector3 Up { get; private set; } = Vector3.UnitY;

        public Vector3 Right { get; private set; } = Vector3.UnitX;

        public Vector3 Position { get; private set; } = Vector3.Zero;

        public float AspectRatio { private get; set; }

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
        public Matrix4 ViewMatrix => Matrix4.LookAt(Position, Position + Front, Up);

        public Matrix4 ProjectionMatrix => Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.01f, 100f);

        public Camera() { }

        public void Update()
        {
            Move();
        }

        private void Move()
        {
            float speed = 1.5f;
            float sensitivity = 0.2f;

            if (InputManager.KeyboardState!.IsKeyDown(Keys.LeftShift))
            {
                speed *= 2.0f;
            }

            if (InputManager.KeyboardState.IsKeyDown(Keys.W))
            {
                Position += Front * speed * Time.Delta; // Forward
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.S))
            {
                Position -= Front * speed * Time.Delta; // Backwards
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.A))
            {
                Position -= Right * speed * Time.Delta; // Left
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.D))
            {
                Position += Right * speed * Time.Delta; // Right
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.Space))
            {
                Position += Up * speed * Time.Delta; // Up
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.LeftControl))
            {
                Position -= Up * speed * Time.Delta; // Down
            }

            Yaw += InputManager.MouseState!.Delta.X * sensitivity;
            Pitch -= InputManager.MouseState.Delta.Y * sensitivity;
            Fov -= InputManager.MouseState.ScrollDelta.Y;
        }

        private void UpdateVectors()
        {
            Front = new Vector3 
            {
                X = MathF.Cos(_pitch) * MathF.Cos(_yaw),
                Y = MathF.Sin(_pitch),
                Z = MathF.Cos(_pitch) * MathF.Sin(_yaw) 
            };

            Front = Vector3.Normalize(Front);
            Right = Vector3.Normalize(Vector3.Cross(Front, Vector3.UnitY));
            Up = Vector3.Normalize(Vector3.Cross(Right, Front));
        }

    }
}