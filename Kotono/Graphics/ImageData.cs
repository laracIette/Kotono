using Kotono.Utils;
using Kotono.Utils.Coordinates;
using StbImageSharp;
using System.Collections.Generic;
using System.IO;

namespace Kotono.Graphics
{
    internal sealed class ImageData
    {
        private static readonly Dictionary<string, ImageData> _datas = [];

        internal PointI Size { get; }

        internal Color[,] Colors { get; }

        internal byte[] Bytes { get; }

        private ImageData(PointI size, Color[,] colors, byte[] bytes)
        {
            Size = size;
            Colors = colors;
            Bytes = bytes;
        }

        internal static ImageData Parse(string path, bool flipVerticallyOnLoad)
        {
            if (!_datas.TryGetValue(path, out ImageData? value))
            {
                bool isAlpha = path.EndsWith(".png");

                var colorComponents = isAlpha ? ColorComponents.RedGreenBlueAlpha : ColorComponents.RedGreenBlue;

                StbImage.stbi_set_flip_vertically_on_load(flipVerticallyOnLoad ? 1 : 0);

                using Stream stream = File.OpenRead(path);

                var image = ImageResult.FromStream(stream, colorComponents);

                var colors = new Color[image.Width, image.Height];

                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        int size = isAlpha ? 4 : 3;
                        int index = y * image.Width * size + x * size;

                        colors[x, y] = new Color(
                            image.Data[index + 0] / 255.0f,
                            image.Data[index + 1] / 255.0f,
                            image.Data[index + 2] / 255.0f,
                            isAlpha ? image.Data[index + 3] / 255.0f : 1.0f
                        );
                    }
                }

                value = new ImageData((image.Width, image.Height), colors, image.Data);

                _datas[path] = value;
            }

            return value;
        }
    }
}
