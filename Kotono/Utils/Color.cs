using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;
using OpenTK.Mathematics;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Kotono.Utils
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Color : IEquatable<Color>
    {
        /// <summary> 
        /// The red component of the Color. 
        /// </summary>
        [JsonInclude]
        public float R = 0.0f;

        /// <summary> 
        /// The green component of the Color. 
        /// </summary>
        [JsonInclude]
        public float G = 0.0f;

        /// <summary> 
        /// The blue component of the Color. 
        /// </summary>
        [JsonInclude]
        public float B = 0.0f;

        /// <summary> 
        /// The alpha component of the Color. 
        /// </summary>
        [JsonInclude]
        public float A = 1.0f;

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #000000FF.
        /// </summary>
        public static Color Black => FromHex("#000000FF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #1E272CFF.
        /// </summary>
        public static Color BlackPearl => FromHex("#1E272CFF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #0000FFFF.
        /// </summary>
        public static Color Blue => FromHex("#0000FFFF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #00FFFFFF.
        /// </summary>
        public static Color Cyan => FromHex("#00FFFFFF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #00008BFF.
        /// </summary>
        public static Color DarkBlue => FromHex("#00008BFF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #A9A9A9FF.
        /// </summary>
        public static Color DarkGray => FromHex("#A9A9A9FF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #2F4F4FFF.
        /// </summary>
        public static Color DarkSlateGray => FromHex("#2F4F4FFF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #696969FF.
        /// </summary>
        public static Color DimGray => FromHex("#696969FF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #808080FF.
        /// </summary>
        public static Color Gray => FromHex("#808080FF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #00FF00FF.
        /// </summary>
        public static Color Green => FromHex("#00FF00FF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #ADD8E6FF.
        /// </summary>
        public static Color LightBlue => FromHex("#ADD8E6FF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #D3D3D3FF.
        /// </summary>
        public static Color LightGray => FromHex("#D3D3D3FF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #B0C4DEFF.
        /// </summary>
        public static Color LightSteelBlue => FromHex("#B0C4DEFF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #FF00FFFF.
        /// </summary>
        public static Color Magenta => FromHex("#FF00FFFF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #191970FF.
        /// </summary>
        public static Color MidnightBlue => FromHex("#191970FF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #000080FF.
        /// </summary>
        public static Color Navy => FromHex("#000080FF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #FFA500FF.
        /// </summary>
        public static Color Orange => FromHex("#FFA500FF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #FF0000FF.
        /// </summary>
        public static Color Red => FromHex("#FF0000FF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #00808000.
        /// </summary>
        public static Color Teal => FromHex("#00808000");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #FFFFFF00.
        /// </summary>
        public static Color Transparent => FromHex("#FFFFFF00");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #FFFFFFFF.
        /// </summary>
        public static Color White => FromHex("#FFFFFFFF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #FFFF00FF.
        /// </summary>
        public static Color Yellow => FromHex("#FFFF00FF");

        /// <summary>
        /// A <see cref="Color"/> whose hexadecimal code is #1A1A33FF.
        /// </summary>
        public static Color _1A1A33FF => FromHex("#1A1A33FF");

        /// <summary>
        /// The hexadecimal code of the <see cref="Color"/>, with its values clamped in range [0, 1].
        /// </summary>
        public readonly string Hex => "#" +
            $"{(byte)(Math.Clamp(R) * 255.0f):X2}" +
            $"{(byte)(Math.Clamp(G) * 255.0f):X2}" +
            $"{(byte)(Math.Clamp(B) * 255.0f):X2}" +
            $"{(byte)(Math.Clamp(A) * 255.0f):X2}";

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
        /// Convert a hex string to a Color accepts 1, 3, 4, 6, 8 letters format. 
        /// </summary>
        public static Color FromHex(string hex)
        {
            hex = hex.Split('#').FirstOrDefault(s => !string.IsNullOrEmpty(s), string.Empty);

            return hex.Length switch
            {
                1 => new Color(HexToF(hex[0])),
                3 => new Color(HexToF(hex[0]), HexToF(hex[1]), HexToF(hex[2])),
                4 => new Color(HexToF(hex[0]), HexToF(hex[1]), HexToF(hex[2]), HexToF(hex[3])),
                6 => new Color(HexToF(hex[0..2]), HexToF(hex[2..4]), HexToF(hex[4..6])),
                8 => new Color(HexToF(hex[0..2]), HexToF(hex[2..4]), HexToF(hex[4..6]), HexToF(hex[6..8])),
                _ => throw new KotonoException($"string \"{hex}\" Length \"{hex.Length}\" isn't handled")
            };
        }

        /// <summary> 
        /// Inputs a 2 character string and returns it converted from hexadecimal string to float. 
        /// </summary>
        private static float HexToF(string hex)
        {
            if (hex.Length != 2)
            {
                throw new KotonoException($"string \"{hex}\" Length \"{hex.Length}\" must be of \"2\"");
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

        /// <summary>
        /// Loops through colors given a frequency.
        /// </summary>
        /// <param name="frequency"> The frequency at which the color loops through RGB values. </param>
        /// <returns></returns>
        public static Color Rainbow(float frequency)
        {
            return new Color(
                (Math.Sin(frequency * Time.Now * 1000.0f + 0.0f) * 0.5f) + 0.5f,
                (Math.Sin(frequency * Time.Now * 1000.0f + 2.0f) * 0.5f) + 0.5f,
                (Math.Sin(frequency * Time.Now * 1000.0f + 4.0f) * 0.5f) + 0.5f
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
