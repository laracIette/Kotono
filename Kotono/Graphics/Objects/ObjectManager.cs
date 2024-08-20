using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Input;
using Kotono.Physics;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using Kotono.Utils.Timing;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Kotono.Graphics.Objects
{
    internal static class ObjectManager
    {
        private static readonly Renderer _renderer = new();

        private static readonly List<IObject> _objects = [];

        private static readonly Dictionary<Type, MethodInfo[]> _typeInputMethods = [];

        internal static PointLight[] PointLights => _objects.OfType<PointLight>().Where(p => p.IsOn).ToArray();

        internal static SpotLight[] SpotLights => _objects.OfType<SpotLight>().Where(s => s.IsOn).ToArray();

        internal static IHitbox[] Hitboxes => _objects.OfType<IHitbox>().ToArray();

        internal static int IndexOf(IObject obj) => _objects.IndexOf(obj);

        internal static void SetRendererSize(Point value) => _renderer.SetSize(value);

        internal static void Create(IObject obj)
        {
            switch (obj)
            {
                case PointLight:
                    if (PointLights.Length >= PointLight.MAX_COUNT)
                    {
                        Logger.LogError($"The number of PointLight is already at its max value: {PointLight.MAX_COUNT}");
                        return;
                    }
                    break;

                case SpotLight:
                    if (SpotLights.Length >= SpotLight.MAX_COUNT)
                    {
                        Logger.LogError($"The number of SpotLight is already at its max value: {SpotLight.MAX_COUNT}");
                        return;
                    }
                    break;

                default:
                    break;
            }

            if (_objects.TryAddUnique(obj))
            {
                Subscribe(obj);
            }
        }

        private static void Subscribe(IObject obj)
        {
            var type = obj.GetType();

            // only needs to be checked once per type
            if (!_typeInputMethods.TryGetValue(type, out MethodInfo[]? methods))
            {
                methods = type.GetAllMethods(m => m.Name.StartsWith("On")).ToArray();
                _typeInputMethods[type] = methods;
            }

            foreach (var methodInfo in methods)
            {
                if (methodInfo.Name.Contains("Key"))
                {
                    Keyboard.Subscribe(obj, methodInfo);
                }
                else if (methodInfo.Name.Contains("Button"))
                {
                    Mouse.Subscribe(obj, methodInfo);
                }
            }
        }

        private static void Delete(IObject obj)
        {
            Unsubscribe(obj);

            if (!_objects.Remove(obj))
            {
                Logger.LogError($"couldn't remove {obj.GetType().Name} \"{obj}\" from _objects");
            }
        }

        private static void Unsubscribe(IObject obj)
        {
            if (_typeInputMethods[obj.GetType()].Length > 0)
            {
                Keyboard.Unsubscribe(obj);
                Mouse.Unsubscribe(obj);
            }
        }

        internal static void Update()
        {
            // List can change during for loop
            for (int i = 0; i < _objects.Count; i++)
            {
                if (_objects[i].IsUpdate)
                {
                    _objects[i].Update();
                }
            }

            UpdateFizix();

            if (Keyboard.IsKeyPressed(Keys.Delete))
            {
                OnDeleteKeyPressed();
            }

            // List can change during for loop
            for (int i = _objects.Count - 1; i >= 0; i--)
            {
                if (_objects[i].IsDelete)
                {
                    Delete(_objects[i]);
                }
            }

            //Logger.Log(_objects.OfType<Rect>().Count());
        }

        private static void UpdateFizix()
        {
            foreach (var obj in _objects.OfType<IFizixObject>())
            {
                if (obj.IsUpdateFizix)
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
                ISelectable.Selected[i].Dispose();
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