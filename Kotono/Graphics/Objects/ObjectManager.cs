using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Input;
using Kotono.Physics;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Exceptions;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects
{
    internal static class ObjectManager
    {
        private static readonly Renderer _renderer = new();

        private static readonly List<IObject> _objects = [];

        internal static PointLight[] PointLights => _objects.OfType<PointLight>().ToArray();

        internal static SpotLight[] SpotLights => _objects.OfType<SpotLight>().ToArray();

        internal static IHitbox[] Hitboxes => _objects.OfType<IHitbox>().ToArray();

        internal static Camera ActiveCamera => _objects.OfType<Camera>().FirstOrNull() ?? throw new KotonoException("there is no Object of type Camera in _objects");

        internal static void SetSize(Point value)
        {
            _renderer.SetSize(value);
        }

        internal static void Create(IObject obj)
        {
            switch (obj)
            {
                case PointLight:
                    if (PointLights.Length >= PointLight.MAX_COUNT)
                    {
                        Logger.Log($"The number of PointLight is already at its max value: {PointLight.MAX_COUNT}.");
                        return;
                    }
                    break;

                case SpotLight:
                    if (SpotLights.Length >= SpotLight.MAX_COUNT)
                    {
                        Logger.Log($"The number of SpotLight is already at its max value: {SpotLight.MAX_COUNT}.");
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

        internal static void Delete(IObject obj)
        {
            if (!_objects.Remove(obj))
            {
                Logger.Log($"error: couldn't remove \"{obj}\" from _objects.");
            }
        }

        internal static void Update()
        {
            // List can change during _objects[i].Update() calls
            for (int i = 0; i < _objects.Count; i++)
            {
                _objects[i].Update();
            }

            if (Keyboard.IsKeyPressed(Keys.Delete))
            {
                OnDeleteKeyPressed();
            }

            UpdateFizix();
        }

        private static void UpdateFizix()
        {
            foreach (var obj in _objects.OfType<IFizixObject>())
            {
                if (obj.IsFizix)
                {
                    Fizix.Update(obj);
                }
            }
        }

        internal static void Draw()
        {
            foreach (var obj in _objects.OfType<IDrawable>())
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

        private static void OnDeleteKeyPressed()
        {
            for (int i = ISelectable.Selected.Count - 1; i >= 0; i--)
            {
                ISelectable.Selected[i].Delete();
                ISelectable.Selected.RemoveAt(i);
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