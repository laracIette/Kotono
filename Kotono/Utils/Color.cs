using Kotono.File;
using OpenTK.Mathematics;
using System;
using System.Globalization;
using System.Linq;

namespace Kotono.Utils
{
    public struct Color
    {
        public ColorComponent R;

        public ColorComponent G;

        public ColorComponent B;

        public ColorComponent A;

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

        /// <summary> Initialize a Color with R = 0, G = 0, B = 0, A = 1 </summary>
        public Color()
        {
            R = 0;
            G = 0;
            B = 0;
            A = 1;
        }

        /// <summary> Initialize a Color with R = c.R, G = c.G, B = c.B, A = c.A </summary>
        public Color(Color c)
        {
            R = c.R;
            G = c.G;
            B = c.B;
            A = c.A;
        }

        /// <summary> Initialize a Color with R = f, G = f, B = f, A = a </summary>
        public Color(float f = 0, float a = 1)
        {
            R = f;
            G = f;
            B = f;
            A = a;
        }

        /// <summary> Initialize a Color with R = (float)d, G = (float)d, B = (float)d, A = (float)a </summary>
        public Color(double d = 0, double a = 1)
        {
            R = (float)d;
            G = (float)d;
            B = (float)d;
            A = (float)a;
        }

        /// <summary> Initialize a Color with R = r, G = g, B = b, A = a </summary>
        public Color(float r, float g, float b, float a = 1)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary> Initialize a Color with R = (float)r, G = (float)g, B = (float)b, A = (float)a </summary>
        public Color(double r, double g, double b, double a = 1)
        {
            R = (float)r;
            G = (float)g;
            B = (float)b;
            A = (float)a;
        }

        /// <summary> Initialize a Color with R = c.R, G = c.G, B = c.B, A = a </summary>
        public Color(Color c, float a)
        {
            R = c.R;
            G = c.G;
            B = c.B;
            A = a;
        }

        /// <summary> Initialize a Color with R = c.R, G = c.G, B = c.B, A = (float)a </summary>
        public Color(Color c, double a)
        {
            R = c.R;
            G = c.G;
            B = c.B;
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

        public static string ToHex(Color c)
        {
            string result = "#";

            result += ((int)(c.R * 255)).ToString("X");
            result += ((int)(c.G * 255)).ToString("X");
            result += ((int)(c.B * 255)).ToString("X");
            result += ((int)(c.A * 255)).ToString("X");
                       
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

        /// <summary> Adds right to left, considering alpha </summary>
        public static Color Add(Color left, Color right)
        {
            left.R += right.R;
            left.G += right.G;
            left.B += right.B;
            left.A += right.A;
            return left;
        }

        /// <summary> Substracts right to left, considering alpha </summary>
        public static Color Substract(Color left, Color right)
        {
            left.R -= right.R;
            left.G -= right.G;
            left.B -= right.B;
            left.A -= right.A;
            return left;
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
