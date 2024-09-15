using Kotono.Utils.Exceptions;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Kotono.Input
{
    internal static class Keyboard
    {
        private sealed record class Method(InputAction InputAction, IObject Instance, MethodInfo MethodInfo);

        private static KeyboardState? _keyboardState;

        internal static KeyboardState KeyboardState
        {
            get => _keyboardState ?? throw new Exception($"error: _keyboardState must not be null");
            set => _keyboardState = value;
        }

        private static readonly Dictionary<Keys, List<Method>> _keyActions = [];

        static Keyboard()
        {
            foreach (var key in Enum.GetValues<Keys>().Distinct())
            {
                if (key != Keys.Unknown)
                {
                    _keyActions[key] = [];
                }
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
                    if ((isKeyPressed && method.InputAction == InputAction.Pressed)
                     || (isKeyDown && method.InputAction == InputAction.Down)
                     || (isKeyReleased && method.InputAction == InputAction.Released))
                    {
                        method.MethodInfo.Invoke(method.Instance, null);
                    }
                }
            }
        }

        /// <summary>
        /// Subscribe a method to a keyboard key <see cref="InputAction"/>.
        /// </summary>
        /// <param name="instance"> The object the method belongs to. </param>
        /// <param name="methodInfo"> The method to subscribe. </param>
        internal static void Subscribe(IObject instance, MethodInfo methodInfo)
        {
            InputAction action;
            int nameEnd;

            if (methodInfo.Name.EndsWith("Pressed"))
            {
                action = InputAction.Pressed;
                nameEnd = 10;
            }
            else if (methodInfo.Name.EndsWith("Down"))
            {
                action = InputAction.Down;
                nameEnd = 7;
            }
            else if (methodInfo.Name.EndsWith("Released"))
            {
                action = InputAction.Released;
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
        /// Unsubscribe all the methods of an object from keyboard key <see cref="InputAction"/>s.
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
