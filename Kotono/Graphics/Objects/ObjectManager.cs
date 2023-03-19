using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using System;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects
{
    public sealed class ObjectManager
    {
        private static readonly Lazy<List<IMesh>> _meshes = new(() => new());

        private static readonly Lazy<List<PointLight>> _pointLights = new(() => new());

        private static readonly Lazy<List<IHitbox>> _hitboxes = new(() => new());

        private static readonly Lazy<Dictionary<string, Tuple<int, int, int>>> _paths = new(() => new());

        public static List<IMesh> Meshes => _meshes.Value;

        public static List<PointLight> PointLights => _pointLights.Value;

        public static List<IHitbox> Hitboxes => _hitboxes.Value;

        public static Dictionary<string, Tuple<int, int, int>> Paths => _paths.Value;

        private ObjectManager() { }
    }
}