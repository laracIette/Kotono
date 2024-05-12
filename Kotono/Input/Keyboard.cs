using Kotono.Utils.Exceptions;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Kotono.Input
{
    internal static class Keyboard
    {
        private record class Method(Action Action, IObject Instance, MethodInfo MethodInfo);

        private static KeyboardState? _keyboardState;

        internal static KeyboardState KeyboardState
        {
            get => _keyboardState ?? throw new Exception($"error: _keyboardState must not be null");
            set => _keyboardState = value;
        }

        private static readonly Dictionary<Keys, List<Method>> _keyActions = [];

        static Keyboard()
        {
            foreach (var key in Enum.GetValues<Keys>()[..^2]) // remove Keys.Unknown and Keys.LastKey
            {
                _keyActions[key] = [];
            }
        }

        internal static void Update()
        {
            foreach (var key in _keyActions.Keys)
            {
                bool isKeyPressed = IsKeyPressed(key);
                bool isKeyDown = IsKeyDown(key);
                bool isKeyReleased = IsKeyReleased(key);

                foreach (var method in _keyActions[key])
                {
                    if ((isKeyPressed && method.Action == Action.Pressed)
                     || (isKeyDown && method.Action == Action.Down)
                     || (isKeyReleased && method.Action == Action.Released))
                    {
                        method.MethodInfo.Invoke(method.Instance, null);
                    }
                }
            }
        }

        /// <summary>
        /// Subscribe a method to a keyboard key <see cref="Action"/>.
        /// </summary>
        /// <param name="instance"> The object the method belongs to. </param>
        /// <param name="methodInfo"> The method to subscribe. </param>
        internal static void Subscribe(IObject instance, MethodInfo methodInfo)
        {
            Action action;
            int nameEnd;

            if (methodInfo.Name.EndsWith("Pressed"))
            {
                action = Action.Pressed;
                nameEnd = 10;
            }
            else if (methodInfo.Name.EndsWith("Down"))
            {
                action = Action.Down;
                nameEnd = 7;
            }
            else if (methodInfo.Name.EndsWith("Released"))
            {
                action = Action.Released;
                nameEnd = 11;
            }
            else
            {
                throw new KotonoException($"couldn't parse method \"{methodInfo.Name}\" to Action");
            }

            if (Enum.TryParse(methodInfo.Name[2..^nameEnd], out Keys key))
            {
                _keyActions[key].Add(new Method(action, instance, methodInfo));
            }
            else
            {
                Logger.Log($"error: couldn't parse \"{methodInfo.Name[2..^10]}\" to Keys in Keyboard.Subscribe(IObject, MethodInfo).");
            }
        }

        /// <summary>
        /// Unsubscribe a method from a keyboard key <see cref="Action"/>.
        /// </summary>
        /// <param name="instance"> The object the method belongs to. </param>
        /// <param name="methodInfo"> The method to unsubscribe. </param>
        [Obsolete("Use Keyboard.Unsubscribe(IObject) instead as you never need to unsubscribe a single method.")]
        internal static void Unsubscribe(IObject instance, MethodInfo methodInfo)
        {
            foreach (var methods in _keyActions.Values)
            {
                methods.RemoveAll(m => m.Instance == instance && m.MethodInfo == methodInfo);
            }
        }

        /// <summary>
        /// Unsubscribe all the methods of an object from keyboard key <see cref="Action"/>s.
        /// </summary>
        /// <param name="instance"> The object which to unsubscribe the methods. </param>
        internal static void Unsubscribe(IObject instance)
        {
            foreach (var methods in _keyActions.Values)
            {
                methods.RemoveAll(m => m.Instance == instance);
            }
        }

        /// <inheritdoc cref="KeyboardState.IsKeyDown(Keys)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsKeyDown(Keys key) => KeyboardState.IsKeyDown(key);

        /// <inheritdoc cref="KeyboardState.IsKeyPressed(Keys)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsKeyPressed(Keys key) => KeyboardState.IsKeyPressed(key);

        /// <inheritdoc cref="KeyboardState.IsKeyReleased(Keys)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsKeyReleased(Keys key) => KeyboardState.IsKeyReleased(key);
    }
}
