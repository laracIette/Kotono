using Kotono.Utils;
using Kotono.Utils.Coordinates;
using StbImageSharp;
using System.Collections.Generic;
using IO = System.IO;

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

        internal static ImageData GetFrom(string path)
        {
            if (!_datas.TryGetValue(path, out ImageData? value))
            {
                StbImage.stbi_set_flip_vertically_on_load(1);

                using IO.Stream stream = IO.File.OpenRead(path);

                var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

                var colors = new Color[image.Width, image.Height];

                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        int index = y * image.Width * 4 + x * 4;

                        colors[x, y] = new Color(
                            image.Data[index + 0] / 255.0f,
                            image.Data[index + 1] / 255.0f,
                            image.Data[index + 2] / 255.0f,
                            image.Data[index + 3] / 255.0f
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
