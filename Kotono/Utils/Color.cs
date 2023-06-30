using OpenTK.Mathematics;
using System;
using System.Globalization;
using System.Linq;

namespace Kotono.Utils
{
    public struct Color
    {
        private float _r;

        private float _g;

        private float _b;

        private float _a;
        
        public float R
        { 
            readonly get => _r; 
            set => _r = Math.Clamp(value, 0, 1);
        }

        public float G
        { 
            readonly get => _g; 
            set => _g = Math.Clamp(value, 0, 1);
        }

        public float B
        { 
            readonly get => _b; 
            set => _b = Math.Clamp(value, 0, 1);
        }

        public float A
        { 
            readonly get => _a; 
            set => _a = Math.Clamp(value, 0, 1);
        }

        public static Color Transparent => new Color(0, 0, 0, 0);

        public static Color Black => new Color(0, 0, 0, 1);
        
        public static Color White => new Color(1, 1, 1, 1);

        public static Color Red => new Color(1, 0, 0, 1);

        public static Color Yellow => new Color(1, 1, 0, 1);

        public static Color Magenta => new Color(1, 0, 1, 1);

        public static Color Green => new Color(0, 1, 0, 1);

        public static Color Cyan => new Color(0, 1, 1, 1);

        public static Color Blue => new Color(0, 0, 1, 1);

        public const int SizeInBytes = sizeof(float) * 4;

        public readonly float this[int index] =>
            index switch
            {
                0 => R,
                1 => G,
                2 => B,
                3 => A,
                _ => throw new IndexOutOfRangeException("You tried to access this Color at index: " + index)
            };

        /// <summary>
        /// Initialize a Color with R = 0, G = 0, B = 0, A = 1
        /// </summary>
        public Color()
        {
            R = 0;
            G = 0;
            B = 0;
            A = 1;
        }

        /// <summary>
        /// Initialize a Color with R = c.R, G = c.G, B = c.B, A = c.A
        /// </summary>
        public Color(Color c)
        {
            R = c.R;
            G = c.G;
            B = c.B;
            A = c.A;
        }

        /// <summary>
        /// Initialize a Color with R = f, G = f, B = f, A = a
        /// </summary>
        public Color(float f, float a = 1)
        {
            R = f;
            G = f;
            B = f;
            A = a;
        }

        /// <summary>
        /// Initialize a Color with R = (float)d, G = (float)d, B = (float)d, A = (float)a
        /// </summary>
        public Color(double d, double a = 1)
        {
            R = (float)d;
            G = (float)d;
            B = (float)d;
            A = (float)a;
        }

        /// <summary>
        /// Initialize a Color with R = r, G = g, B = b, A = a
        /// </summary>
        public Color(float r, float g, float b, float a = 1)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Initialize a Color with R = (float)r, G = (float)g, B = (float)b, A = (float)a
        /// </summary>
        public Color(double r, double g, double b, double a = 1)
        {
            R = (float)r;
            G = (float)g;
            B = (float)b;
            A = (float)a;
        }

        /// <summary> Convert a hex string to a Color accepts 1, 3, 4, 6, 8 letters format </summary>
        public static Color FromHex(string hex)
        {
            hex = hex.Split('#').Where(s => s != "").FirstOrDefault("");

            return hex.Length switch
            {
                1 => new Color(HexToF(hex[0])),
                3 => new Color(HexToF(hex[0]), HexToF(hex[1]), HexToF(hex[2])),
                4 => new Color(HexToF(hex[0]), HexToF(hex[1]), HexToF(hex[2]), HexToF(hex[3])),
                6 => new Color(HexToF(hex[0..2]), HexToF(hex[2..4]), HexToF(hex[4..6])),
                8 => new Color(HexToF(hex[0..2]), HexToF(hex[2..4]), HexToF(hex[4..6]), HexToF(hex[6..8])),
                _ => throw new Exception($"error: string \"{hex}\" Length \"{hex.Length}\" isn't handled")
            };
        }

        /// <summary> Inputs a 2 character string and returns it converted from hexadecimal string to float </summary>
        private static float HexToF(string hex)
        {
            if (hex.Length != 2)
            {
                throw new Exception($"error: string \"{hex}\" Length \"{hex.Length}\" must be of \"2\"");
            }
            else
            {
                return byte.Parse(hex, NumberStyles.HexNumber) / 255f;
            }
        }

        /// <summary> Inputs a char and returns it converted from hexadecimal char to float </summary>
        private static float HexToF(char hex)
        {
            return HexToF(hex.ToString() + hex.ToString());
        }     

        public static Color operator +(Color left, Color right)
        {
            left.R += right.R;
            left.G += right.G;
            left.B += right.B;
            return left;
        }

        public static Color operator -(Color left, Color right)
        {
            left.R -= right.R;
            left.G -= right.G;
            left.B -= right.B;
            return left;
        }

        public static Color operator -(Color c, float f)
        {
            c.R -= f;
            c.G -= f;
            c.B -= f;
            return c;
        }

        public static Color operator -(Color c)
        {
            c.R = -c.R;
            c.G = -c.G;
            c.B = -c.B;
            return c;
        }

        public static Color operator *(Color left, Color right)
        {
            left.R *= right.R;
            left.G *= right.G;
            left.B *= right.B;
            return left;
        }

        public static Color operator *(Color c, float f)
        {
            c.R *= f;
            c.G *= f;
            c.B *= f;
            return c;
        }

        public static Color operator /(Color left, Color right)
        {
            left.R /= right.R;
            left.G /= right.G;
            left.B /= right.B;
            return left;
        }

        public static Color operator /(Color c, float f)
        {
            c.R /= f;
            c.G /= f;
            c.B /= f;
            return c;
        }

        public static bool operator ==(Color left, Color right)
        {
            return (left.R == right.R) && (left.G == right.G) && (left.B == right.B) && (left.A == right.A);
        }

        public static bool operator !=(Color left, Color right)
        {
            return !(left == right);
        }

        public static explicit operator Vector(Color c)
        {
            return new Vector(c.R, c.G, c.B);
        }

        public static explicit operator Color(Vector v)
        {
            return new Color(v.X, v.Y, v.Z);
        }

        public static explicit operator Vector3(Color c)
        {
            return new Vector3(c.R, c.G, c.B);
        }

        public static explicit operator Color(Vector3 v)
        {
            return new Color(v.X, v.Y, v.Z);
        }

        public static explicit operator Vector4(Color c)
        {
            return new Vector4(c.R, c.G, c.B, c.A);
        }

        public static explicit operator Color(Vector4 v)
        {
            return new Color(v.X, v.Y, v.Z, v.W);
        }

        public override readonly string ToString()
        {
            return $"R: {R}, G: {G}, B: {B}, A: {A}";
        }
    }
}
