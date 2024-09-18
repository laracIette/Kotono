using Kotono.Graphics.Objects.Lights;
using Kotono.Input;
using Kotono.Physics;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
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

        private static readonly HashSet<IObject> _objects = [];

        private static readonly Dictionary<Type, MethodInfo[]> _typeInputMethods = [];

        internal static IEnumerable<T> GetObjectsOfType<T>() where T : IObject => _objects.OfType<T>();

        internal static IEnumerable<T> GetObjectsOfType<T>(Func<T, bool> predicate) where T : IObject => _objects.OfType<T>().Where(predicate);

        internal static void SetRendererSize(Point value) => _renderer.SetSize(value);

        internal static void Create(IObject obj)
        {
            switch (obj)
            {
                case PointLight:
                    if (GetObjectsOfType<PointLight>().Count() >= PointLight.MAX_COUNT)
                    {
                        Logger.LogError($"The number of PointLight is already at its max value: {PointLight.MAX_COUNT}");
                        return;
                    }
                    break;

                case SpotLight:
                    if (GetObjectsOfType<SpotLight>().Count() >= SpotLight.MAX_COUNT)
                    {
                        Logger.LogError($"The number of SpotLight is already at its max value: {SpotLight.MAX_COUNT}");
                        return;
                    }
                    break;

                default:
                    break;
            }

            if (_objects.Add(obj))
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

        /// <summary>
        /// Unsubscribes the <see cref="IObject"/> and removes it from <see cref="_objects"/>.
        /// </summary>
        private static void Delete(IObject obj)
        {
            Unsubscribe(obj);

            if (!_objects.Remove(obj))
            {
                Logger.LogError($"couldn't remove {obj.GetType().Name} '{obj}' from _objects");
            }
        }

        /// <summary>
        /// Unsubscribes the <see cref="IObject"/> from <see cref="Mouse"/> and <see cref="Keyboard"/> input events.
        /// </summary>
        private static void Unsubscribe(IObject obj)
        {
            if (_typeInputMethods[obj.GetType()].Length > 0)
            {
                Keyboard.Unsubscribe(obj);
                Mouse.Unsubscribe(obj);
            }
        }

        /// <summary>
        /// Updates all the <see cref="IObject"/>s of the <see cref="ObjectManager"/>
        /// and deletes those disposed.
        /// </summary>
        internal static void Update()
        {
            Span<IObject> objects = [.. GetObjectsOfType<IObject>(o => o.IsUpdate)];

            foreach (var obj in objects)
            {
                obj.Update();
            }

            UpdateFizix();
            UpdateDeleteObjects();
        }

        private static void UpdateFizix()
        {
            foreach (var obj in GetObjectsOfType<IFizixObject>(o => o.IsUpdateFizix))
            {
                obj.UpdateFizix();
            }
        }

        private static void UpdateDeleteObjects()
        {
            if (Keyboard.IsKeyPressed(Keys.Delete))
            {
                OnDeleteKeyPressed();
            }

            Span<IObject> objects = [.. GetObjectsOfType<IObject>(o => o.IsDelete)];

            foreach (var obj in objects)
            {
                Delete(obj);
            }
        }

        internal static void Draw()
        {
            foreach (var obj in GetObjectsOfType<IDrawable>(d => d.IsDraw))
            {
                _renderer.AddToRenderQueue(obj);
            }

            _renderer.Render();
        }

        internal static void Save()
        {
            foreach (var obj in GetObjectsOfType<ISaveable>())
            {
                obj.Save();
            }
        }

        private static void OnDeleteKeyPressed()
        {
            DeleteSelectedList(ISelectable2D.Selected);
            DeleteSelectedList(ISelectable3D.Selected);

            static void DeleteSelectedList<T>(List<T> selected) where T : ISelectable
            {
                for (int i = selected.Count - 1; i >= 0; i--)
                {
                    selected[i].Dispose();
                    selected.RemoveAt(i);
                }
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