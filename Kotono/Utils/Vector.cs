﻿using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;

namespace Kotono.Utils
{
    public struct Vector
    {
        public float X;

        public float Y;

        public float Z;

        public readonly float Length => MathF.Sqrt(X * X + Y * Y + Z * Z);

        public readonly Vector Normalized => this / Length;

        public static Vector Zero => new Vector(0, 0, 0);

        public static Vector Unit => new Vector(1, 1, 1);

        public static Vector UnitX => new Vector(1, 0, 0);

        public static Vector UnitXY => new Vector(1, 1, 0);

        public static Vector UnitXZ => new Vector(1, 0, 1);

        public static Vector UnitY => new Vector(0, 1, 0);

        public static Vector UnitYZ => new Vector(0, 1, 1);

        public static Vector UnitZ => new Vector(0, 0, 1);


        public const int SizeInBytes = sizeof(float) * 3;

        public readonly float this[int index] =>
            index switch
            {
                0 => X,
                1 => Y,
                2 => Z,
                _ => throw new IndexOutOfRangeException("You tried to access this Vector at index: " + index)
            };
            
        /// <summary>
        /// Initialize a Vector with X = 0, Y = 0, Z = 0
        /// </summary>
        public Vector()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }

        /// <summary>
        /// Initialize a Vector with X = v.X, Y = v.Y, Z = v.Z
        /// </summary>
        public Vector(Vector v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }

        /// <summary>
        /// Initialize a Vector with X = f, Y = f, Z = f
        /// </summary>
        public Vector(float f)
        {
            X = f;
            Y = f;
            Z = f;
        }

        /// <summary>
        /// Initialize a Vector with X = (float)d, Y = (float)d, Z = (float)d
        /// </summary>
        public Vector(double d)
        {
            X = (float)d;
            Y = (float)d;
            Z = (float)d;
        }

        /// <summary>
        /// Initialize a Vector with X = x, Y = y, Z = z
        /// </summary>
        public Vector(float x = 0, float y = 0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Initialize a Vector with X = (float)x, Y = (float)y, Z = (float)z
        /// </summary>
        public Vector(double x = 0, double y = 0, double z = 0)
        {
            X = (float)x;
            Y = (float)y;
            Z = (float)z;
        }

        public void RotateAroundPoint(Vector point, Vector rotation)
        {
            bool print = InputManager.KeyboardState!.IsKeyDown(Keys.I);
            
            if (print) KT.Print("");
            //var rotation = Quaternion.FromAxisAngle((Vector3)axis, MathHelper.DegreesToRadians(angle));
            //if (print) KT.Print(rotation);

            //this = (Vector)Vector3.Transform((Vector3)this, rotation);
            //if (print) KT.Print(this);

            if (print) KT.Print(this);

            //var direction = (Vector3)(this - point);
            //direction = Quaternion.FromEulerAngles((Vector3)rotation) * direction;
            //this = (Vector)direction + point;

            this = (Vector)(Quaternion.FromEulerAngles((Vector3)rotation) * (Vector3)(this - point)) + point;

            if (print) KT.Print(this);
        }

        public static Vector Cross(Vector left, Vector right)
        {
            return new Vector
            {
                X = left.Y * right.Z - left.Z * right.Y,
                Y = left.Z * right.X - left.X * right.Z,
                Z = left.X * right.Y - left.Y * right.X
            };
        }

        public static Vector operator +(Vector left, Vector right)
        {
            left.X += right.X;
            left.Y += right.Y;
            left.Z += right.Z;
            return left;
        }

        public static Vector operator -(Vector left, Vector right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            left.Z -= right.Z;
            return left;
        }

        public static Vector operator -(Vector v)
        {
            v.X = -v.X;
            v.Y = -v.Y;
            v.Z = -v.Z;
            return v;
        }

        public static Vector operator *(Vector left, Vector right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            left.Z *= right.Z;
            return left;
        }

        public static Vector operator *(Vector v, float f)
        {
            v.X *= f;
            v.Y *= f;
            v.Z *= f;
            return v;
        }

        public static Vector operator /(Vector left, Vector right)
        {
            left.X /= right.X;
            left.Y /= right.Y;
            left.Z /= right.Z;
            return left;
        }

        public static Vector operator /(Vector v, float f)
        {
            v.X /= f;
            v.Y /= f;
            v.Z /= f;
            return v;
        }

        public static bool operator ==(Vector left, Vector right)
        {
            return (left.X == right.X) && (left.Y == right.Y) && (left.Z == right.Z);
        }

        public static bool operator !=(Vector left, Vector right)
        {
            return !(left == right);
        }

        public static explicit operator Vector3(Vector v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        public static explicit operator Vector(Vector3 v)
        {
            return new Vector(v.X, v.Y, v.Z);
        }

        public override readonly string ToString()
        {
            return $"X: {X}, Y: {Y}, Z: {Z}"; ;
        }
    }
}