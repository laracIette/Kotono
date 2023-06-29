using Kotono.Utils;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using Math = Kotono.Utils.Math;

namespace Kotono.Graphics
{
    public class Camera
    {
        private float _pitch = 0.0f;

        private float _yaw = -MathHelper.PiOver2;

        private float _fov = MathHelper.PiOver2;

        private float _speed = 1.0f;

        public Vector Front { get; private set; } = -Vector.UnitZ;

        public Vector Up { get; private set; } = Vector.UnitY;

        public Vector Right { get; private set; } = Vector.UnitX;

        public Vector Location { get; private set; } = Vector.Zero;

        public float AspectRatio { private get; set; }

        public float Pitch
        {
            get => Math.Deg(_pitch);

            set
            {
                value = Math.Clamp(value, -89.0f, 89.0f);
                _pitch = Math.Rad(value);
                UpdateVectors();
            }
        }

        public float Yaw
        {
            get => Math.Deg(_yaw);

            set
            {
                _yaw = Math.Rad(value);
                UpdateVectors();
            }
        }

        public float Fov
        {
            get => Math.Deg(_fov);

            set
            {
                value = Math.Clamp(value, 1.0f, 90.0f);
                _fov = Math.Rad(value);
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
            if (Input.CursorState == CursorState.Normal)
            {
                return;
            }

            float sensitivity = 0.2f;

            Yaw += Input.MouseState!.Delta.X * sensitivity;
            Pitch -= Input.MouseState.Delta.Y * sensitivity;
            _speed += Input.MouseState.ScrollDelta.Y * _speed / 10;
            _speed = Math.Clamp(_speed, 0.1, 100);

            float fast = Input.KeyboardState!.IsKeyDown(Keys.LeftShift) ? 2.0f : 1.0f;
            float slow = Input.KeyboardState.IsKeyDown(Keys.LeftControl) ? 0.5f : 1.0f;

            if (Input.KeyboardState.IsKeyDown(Keys.W))
            {
                Location += Front * _speed * fast * slow * Time.DeltaS; // Forward
            }
            if (Input.KeyboardState.IsKeyDown(Keys.S))
            {
                Location -= Front * _speed * fast * slow * Time.DeltaS; // Backwards
            }
            if (Input.KeyboardState.IsKeyDown(Keys.A))
            {
                Location -= Right * _speed * fast * slow * Time.DeltaS; // Left
            }
            if (Input.KeyboardState.IsKeyDown(Keys.D))
            {
                Location += Right * _speed * fast * slow * Time.DeltaS; // Right
            }
            if (Input.KeyboardState.IsKeyDown(Keys.E))
            {
                Location += Up * _speed * fast * slow * Time.DeltaS; // Up
            }
            if (Input.KeyboardState.IsKeyDown(Keys.Q))
            {
                Location -= Up * _speed * fast * slow * Time.DeltaS; // Down
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