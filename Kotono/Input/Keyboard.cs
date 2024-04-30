using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Generic;
using System;
using Kotono.Utils;
using System.Runtime.CompilerServices;

namespace Kotono.Input
{
    internal static class Keyboard
    {
        private static KeyboardState? _keyboardState;

        internal static KeyboardState KeyboardState
        {
            get => _keyboardState ?? throw new Exception($"error: _keyboardState must not be null");
            set => _keyboardState = value;
        }

        private static readonly Dictionary<Keys, EventHandler<TimedEventArgs>?> _keysPressed = [];

        internal static void Update()
        {
            foreach (var key in _keysPressed.Keys)
            {
                if (IsKeyPressed(key))
                {
                    _keysPressed[key]?.Invoke(null, new TimedEventArgs());
                }
            }
        }

        internal static void SubscribeKeyPressed(EventHandler<TimedEventArgs> func, Keys key)
        {
            if (!_keysPressed.ContainsKey(key))
            {
                _keysPressed[key] = null;
            }

            _keysPressed[key] += func;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsKeyDown(Keys key) => KeyboardState.IsKeyDown(key);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsKeyPressed(Keys key) => KeyboardState.IsKeyPressed(key);
    }
}
