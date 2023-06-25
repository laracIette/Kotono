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

        public Vector Front { get; private set; } = -Vector.UnitZ;

        public Vector Up { get; private set; } = Vector.UnitY;

        public Vector Right { get; private set; } = Vector.UnitX;

        public Vector Location { get; private set; } = Vector.Zero;

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
        public Matrix4 ViewMatrix => Matrix4.LookAt((Vector3)Location, (Vector3)(Location + Front), (Vector3)Up);

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
                Location += Front * speed * Time.DeltaS; // Forward
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.S))
            {
                Location -= Front * speed * Time.DeltaS; // Backwards
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.A))
            {
                Location -= Right * speed * Time.DeltaS; // Left
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.D))
            {
                Location += Right * speed * Time.DeltaS; // Right
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.Space))
            {
                Location += Up * speed * Time.DeltaS; // Up
            }
            if (InputManager.KeyboardState.IsKeyDown(Keys.LeftControl))
            {
                Location -= Up * speed * Time.DeltaS; // Down
            }

            Yaw += InputManager.MouseState!.Delta.X * sensitivity;
            Pitch -= InputManager.MouseState.Delta.Y * sensitivity;
            Fov -= InputManager.MouseState.ScrollDelta.Y;
        }

        private void UpdateVectors()
        {
            Front = new Vector 
            {
                X = MathF.Cos(_pitch) * MathF.Cos(_yaw),
                Y = MathF.Sin(_pitch),
                Z = MathF.Cos(_pitch) * MathF.Sin(_yaw) 
            };

            Front = Front.Normalized;
            Right = Vector.Cross(Front, Vector.UnitY).Normalized;
            Up = Vector.Cross(Right, Front).Normalized;
        }

    }
}