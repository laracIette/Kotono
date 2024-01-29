using Kotono.Graphics.Objects.Lights;
using Kotono.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects.Managers
{
    internal static class ObjectManager
    {
        private static readonly Renderer _renderer = new();

        private static readonly List<Object> _objects = [];

        internal static PointLight[] PointLights => _objects.OfType<PointLight>().ToArray();

        internal static SpotLight[] SpotLights => _objects.OfType<SpotLight>().ToArray();

        internal static void SetSize(Point value)
        {
            _renderer.SetSize(value);
        }

        internal static void Create(Object obj)
        {
            switch (obj)
            {
                case PointLight:
                    if (PointLights.Length >= PointLight.MAX_COUNT)
                    {
                        KT.Log($"The number of PointLight is already at its max value: {PointLight.MAX_COUNT}.");
                        return;
                    }
                    break;

                case SpotLight:
                    if (SpotLights.Length >= SpotLight.MAX_COUNT)
                    {
                        KT.Log($"The number of SpotLight is already at its max value: {SpotLight.MAX_COUNT}.");
                        return;
                    }
                    break;

                default:
                    break;
            }

            if (!_objects.Contains(obj))
            {
                _objects.Add(obj);
            }
        }

        internal static void Delete(Object obj)
        {
            if (!_objects.Remove(obj))
            {
                KT.Log($"error: couldn't remove \"{obj}\" from _objects.");
            }
        }

        internal static void Update()
        {
            // List can change during Object.Update() calls
            for (int i = 0; i < _objects.Count; i++)
            {
                _objects[i].Update();
            }
        }

        internal static void Draw()
        {
            foreach (var obj in _objects.OfType<Drawable>())
            {
                _renderer.AddToRenderQueue(obj);
            }

            _renderer.Render();
        }

        internal static void Save()
        {
            foreach (var obj in _objects.OfType<ISaveable>())
            {
                obj.Save();
            }
        }

        internal static void Dispose()
        {
            foreach (var obj in _objects)
            {
                obj.Dispose();
            }

            _objects.Clear();

            _renderer.Dispose();
        }
    }
}