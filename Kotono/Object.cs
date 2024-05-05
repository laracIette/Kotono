using Kotono.Graphics;
using Kotono.Graphics.Objects;
using Kotono.Utils;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Kotono
{
    internal abstract class Object : IObject
    {
#if DEBUG
        private string? __type => GetType().FullName;
#endif


        public virtual bool IsUpdate { get; set; } = true;

        public bool IsDelete { get; private set; } = false;

        internal Object()
        {
            ObjectManager.Create(this);
        }

        public virtual void Update() { }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);

            IsDelete = true;
        }



        /// <inheritdoc cref="Printer.Print(object?, Color)"/>
        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void print(object? obj, Color color) => Printer.Print(obj, color);

        /// <inheritdoc cref="Printer.Print(object?, bool)"/>
        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void print(object? obj, bool isRainbow = false) => Printer.Print(obj, isRainbow);

        /// <inheritdoc cref="Printer.Print()"/>
        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void print() => Printer.Print();

        /// <inheritdoc cref="Logger.Log(object?)"/>
        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void log(object? obj) => Logger.Log(obj);

        /// <inheritdoc cref="Logger.Log()"/>>
        [Conditional("DEBUG")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static void log() => Logger.Log();

        protected static string str(params object[] objects) 
        {
            string result = "";
            foreach (var obj in objects)
            {
                result += obj;
            }
            return result;
        }
    }
}
