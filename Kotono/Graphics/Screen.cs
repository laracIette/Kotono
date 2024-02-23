using Kotono.Utils.Coordinates;
using System;
using System.Runtime.InteropServices;

namespace Kotono.Graphics
{
    public static partial class Screen
    {
        public static Point Size { get; private set; }

        // Import the necessary Windows API functions
        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool EnumDisplayMonitors(nint hdc, nint lprcClip, MonitorEnumProc lpfnEnum, nint dwData);

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool GetMonitorInfo(nint hMonitor, ref MonitorInfo lpmi);

        [LibraryImport("user32.dll")]
        private static partial IntPtr MonitorFromPoint(ScreenPoint pt, MonitorOptions dwFlags);

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct ScreenPoint(int x, int y)
        {
            public int X = x;
            public int Y = y;
        }

        // Define the required structures
        [StructLayout(LayoutKind.Sequential)]
        public struct MonitorInfo
        {
            public uint Size;
            public ScreenRect Monitor;
            public ScreenRect WorkArea;
            public uint Flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ScreenRect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        // EnumDisplayMonitors callback delegate
        private delegate bool MonitorEnumProc(nint hMonitor, nint hdcMonitor, ref ScreenRect lprcMonitor, nint dwData);

        // MonitorFromPoint flags
        public enum MonitorOptions : uint
        {
            MONITOR_DEFAULTTONULL = 0x00000000,
            MONITOR_DEFAULTTOPRIMARY = 0x00000001,
            MONITOR_DEFAULTTONEAREST = 0x00000002
        }

        static Screen()
        {
            EnumDisplayMonitors(nint.Zero, nint.Zero, MonitorEnumCallback, nint.Zero);
        }

        // Callback function for EnumDisplayMonitors
        private static bool MonitorEnumCallback(nint hMonitor, nint hdcMonitor, ref ScreenRect lprcMonitor, nint dwData)
        {
            var monitorInfo = new MonitorInfo();
            monitorInfo.Size = (uint)Marshal.SizeOf(monitorInfo);
            GetMonitorInfo(hMonitor, ref monitorInfo);

            Size = new Point
            {
                X = monitorInfo.Monitor.Right - monitorInfo.Monitor.Left,
                Y = monitorInfo.Monitor.Bottom - monitorInfo.Monitor.Top
            };

            return true;
        }
    }
}