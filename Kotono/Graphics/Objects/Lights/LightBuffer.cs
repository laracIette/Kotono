using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace Kotono.Graphics.Objects.Lights
{
    [StructLayout(LayoutKind.Sequential)]
    struct LightBuffer
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public PointLight[] Lights;
        public int NumLights;
    };
}
