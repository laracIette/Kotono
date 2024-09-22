using Kotono.Graphics.Textures;
using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal sealed class TestImage : Image
    {
        internal TestImage()
        {
            Texture = new ImageTexture(Path.FromAssets(@"Characters\a.png"));
            RelativePosition = new Point(150.0f, 250.0f);
            RelativeSize = new Point(50.0f, 60.0f);
        }

        private void OnUpKeyDown()
            => Rect.SetPositionTransformation(new Point(0.0f, -50.0f), 1.0f);
        private void OnDownKeyDown()
            => Rect.SetPositionTransformation(new Point(0.0f, 50.0f), 1.0f);
        private void OnLeftKeyDown()
            => Rect.SetPositionTransformation(new Point(-50.0f, 0.0f), 1.0f);
        private void OnRightKeyDown()
            => Rect.SetPositionTransformation(new Point(50.0f, 0.0f), 1.0f);
    }
}
