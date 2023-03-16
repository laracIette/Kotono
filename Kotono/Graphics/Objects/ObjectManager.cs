using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using System;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects
{
    public static class ObjectManager
    {
        public static readonly List<IMesh> Meshes = new();
        public static readonly List<PointLight> PointLights = new();
        public static readonly Dictionary<string, Tuple<int, int, int>> Paths = new();
    }
}