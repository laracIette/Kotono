using System;

namespace Kotono.Graphics
{
    public sealed class CameraManager
    {
        private static readonly Lazy<Camera> _main = new(() => new());

        public static Camera Main => _main.Value;

        private CameraManager() { }
    }
}
