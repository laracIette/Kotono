using Kotono.File;
using OpenTK.Mathematics;
using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

namespace Kotono.Utils
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Color : IEquatable<Color>
    {
        private float _r;

        private float _g;

        private float _b;

        private float _a;

        /// <summary> 
        /// The red component of the Color. 
        /// </summary>
        [Parsable]
        public float R
        {
            readonly get => _r;
            set => _r = Math.Clamp(value, 0.0f, 1.0f);
        }

        /// <summary> 
        /// The green component of the Color. 
        /// </summary>
        [Parsable]
        public float G
        {
            readonly get => _g;
            set => _g = Math.Clamp(value, 0.0f, 1.0f);
        }

        /// <summary> 
        /// The blue component of the Color. 
        /// </summary>
        [Parsable]
        public float B
        {
            readonly get => _b;
            set => _b = Math.Clamp(value, 0.0f, 1.0f);
        }

        /// <summary> 
        /// The alpha component of the Color. 
        /// </summary>
        [Parsable]
        public float A
        {
            readonly get => _a;
            set => _a = Math.Clamp(value, 0.0f, 1.0f);
        }

        public static Color Black => FromHex("#000000FF");

        public static Color Blue => FromHex("#0000FFFF");

        public static Color Cyan => FromHex("#00FFFFFF");

        public static Color DarkGray => FromHex("#404040FF");

        public static Color Gray => FromHex("#808080FF");

        public static Color Green => FromHex("#00FF00FF");

        public static Color LightGray => FromHex("#C0C0C0FF");

        public static Color Magenta => FromHex("#FF00FFFF");

        public static Color Red => FromHex("#FF0000FF");

        public static Color Transparent => FromHex("#FFFFFF00");

        public static Color White => FromHex("#FFFFFFFF");

        public static Color Yellow => FromHex("#FFFF00FF");

        public static int SizeInBytes => sizeof(float) * 4;

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
        /// Initialize a Color with R = 0, G = 0, B = 0, A = 1. 
        /// </summary>
        public Color()
        {
            R = 0.0f;
            G = 0.0f;
            B = 0.0f;
            A = 1.0f;
        }

        /// <summary> 
        /// Initialize a Color with R = c.R, G = c.G, B = c.B, A = c.A. 
        /// </summary>
        public Color(Color c)
        {
            R = c.R;
            G = c.G;
            B = c.B;
            A = c.A;
        }

        /// <summary> 
        /// Initialize a Color with R = f, G = f, B = f, A = a. 
        /// </summary>
        public Color(float f = 0.0f, float a = 1.0f)
        {
            R = f;
            G = f;
            B = f;
            A = a;
        }

        /// <summary> 
        /// Initialize a Color with R = r, G = g, B = b, A = a. 
        /// </summary>
        public Color(float r, float g, float b, float a = 1.0f)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary> 
        /// Initialize a Color with R = c.R, G = c.G, B = c.B, A = a. 
        /// </summary>
        public Color(Color c, float a)
        {
            R = c.R;
            G = c.G;
            B = c.B;
            A = a;
        }

        /// <summary> 
        /// Convert a hex string to a Color accepts 1, 3, 4, 6, 8 letters format. 
        /// </summary>
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

        /// <summary> 
        /// Inputs a 2 character string and returns it converted from hexadecimal string to float. 
        /// </summary>
        private static float HexToF(string hex)
        {
            if (hex.Length != 2)
            {
                throw new Exception($"error: string \"{hex}\" Length \"{hex.Length}\" must be of \"2\"");
            }
            else
            {
                return byte.Parse(hex, NumberStyles.HexNumber) / 255.0f;
            }
        }

        /// <summary> 
        /// Inputs a char and returns it converted from hexadecimal char to float.
        /// </summary>
        private static float HexToF(char hex)
        {
            return HexToF(hex.ToString() + hex.ToString());
        }

        public static string ToHex(Color c)
        {
            string result = "#";

            result += ((int)(c.R * 255.0f)).ToString("X");
            result += ((int)(c.G * 255.0f)).ToString("X");
            result += ((int)(c.B * 255.0f)).ToString("X");
            result += ((int)(c.A * 255.0f)).ToString("X");

            return result;
        }

        public static Color FromProperties(Properties p)
        {
            return new Color
            {
                R = float.Parse(p["Color.R"]),
                G = float.Parse(p["Color.G"]),
                B = float.Parse(p["Color.B"]),
                A = float.Parse(p["Color.A"])
            };
        }

        public static Color FromProperties(Properties p, string parent)
        {
            return new Color
            {
                R = float.Parse(p[parent + ".Color.R"]),
                G = float.Parse(p[parent + ".Color.G"]),
                B = float.Parse(p[parent + ".Color.B"]),
                A = float.Parse(p[parent + ".Color.A"])
            };
        }

        /// <summary>
        /// Loops through colors given a frequency.
        /// </summary>
        /// <param name="frequency"> The frequency at which the color loops through RGB values. </param>
        /// <returns></returns>
        public static Color Rainbow(double frequency)
        {
            return new Color(
                ((float)MathD.Sin(frequency * Time.Now + 0.0) * 0.5f) + 0.5f,
                ((float)MathD.Sin(frequency * Time.Now + 2.0) * 0.5f) + 0.5f,
                ((float)MathD.Sin(frequency * Time.Now + 4.0) * 0.5f) + 0.5f
            );
        }

        /// <summary> 
        /// Adds right to left, considering alpha.
        /// </summary>
        public static Color Add(Color left, Color right)
        {
            left.R += right.R;
            left.G += right.G;
            left.B += right.B;
            left.A += right.A;
            return left;
        }

        /// <summary> 
        /// Substracts right to left, considering alpha. 
        /// </summary>
        public static Color Substract(Color left, Color right)
        {
            left.R -= right.R;
            left.G -= right.G;
            left.B -= right.B;
            left.A -= right.A;
            return left;
        }

        public static Color Parse(string[] values)
        {
            return new Color
            {
                R = float.Parse(values[0]),
                G = float.Parse(values[1]),
                B = float.Parse(values[2]),
                A = float.Parse(values[3])
            };
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
            return left.Equals(right);
        }

        public static bool operator !=(Color left, Color right)
        {
            return !(left == right);
        }

        public override readonly bool Equals(object? obj)
        {
            return obj is Color c && Equals(c);
        }

        public readonly bool Equals(Color c)
        {
            return R == c.R
                && G == c.G
                && B == c.B
                && A == c.A;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(R, G, B, A);
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

        public static explicit operator Color4(Color c)
        {
            return new Color4(c.R, c.G, c.B, c.A);
        }

        public static explicit operator Color(Color4 c)
        {
            return new Color(c.R, c.G, c.B, c.A);
        }

        public override readonly string ToString()
        {
            return $"R: {R}, G: {G}, B: {B}, A: {A}";
        }
    }
}
