using Kotono.Input;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Engine
{
    public enum UserMode
    {
        Navigate,
        Edit,
        Play
    }

    internal class Mode
    {
        internal UserMode UserMode { get; private set; }

        internal Mode()
        {
            UserMode = UserMode.Navigate;
        }

        internal void Init()
        {

        }

        internal void Update()
        {
            if (Mouse.IsButtonDown(MouseButton.Right))
            {
                UserMode = UserMode.Play;
            }
            else
            {
                UserMode = UserMode.Navigate;
            }
        }
    }
}
