using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal class TestImage : Image
    {
        internal TestImage()
            : base(Path.FromAssets("Characters/a.png"))
        {
            Position = new Point(150.0f, 250.0f);
            Size = new Point(50.0f, 60.0f);
        }

        protected virtual void OnUpKeyDown()
        {
            Rect.SetTransformation(new RectBase(new Point(0.0f, -50.0f), Point.Unit), 1.0f);
        }
        protected virtual void OnDownKeyDown()
        {
            Rect.SetTransformation(new RectBase(new Point(0.0f, 50.0f), Point.Unit), 1.0f);
        }
        protected virtual void OnLeftKeyDown()
        {
            Rect.SetTransformation(new RectBase(new Point(-50.0f, 0.0f), Point.Unit), 1.0f);
        }
        protected virtual void OnRightKeyDown()
        {
            Rect.SetTransformation(new RectBase(new Point(50.0f, 0.0f), Point.Unit), 1.0f);
        }
    }
}
