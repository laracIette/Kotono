using Kotono.Input;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Kotono.Engine
{
    public class Mode
    {
        public UserMode UserMode { get; private set; }

        public Mode()
        {
            UserMode = UserMode.Navigate;
        }

        public void Init()
        {

        }

        public void Update()
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
