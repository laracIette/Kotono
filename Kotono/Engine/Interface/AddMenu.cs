using Kotono.Graphics.Objects;
using Kotono.Utils;

namespace Kotono.Engine.Interface
{
    public static class AddMenu
    {
        private static readonly RoundedBox _backgroundBox = new(new Rect(100, 100, 300, 500), Color.White, 0, 5, 30);

        public static void Init()
        {
            _backgroundBox.Show();
        }

        public static void Update()
        {

        }
    }
}
