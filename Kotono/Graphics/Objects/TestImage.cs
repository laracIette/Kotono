using Kotono.Utils.Coordinates;

namespace Kotono.Graphics.Objects
{
    internal class TestImage()
        : Image(new ImageSettings { Texture = Utils.Path.ASSETS + "Characters/a.png", Rect = new Rect(150.0f, 250.0f, 50.0f, 60.0f) })
    {

        protected virtual void OnUpKeyDown()
        {
            SetTransformation(new Rect(y: -50.0f), 1.0f);
        }
        protected virtual void OnDownKeyDown()
        {
            SetTransformation(new Rect(y: 50.0f), 1.0f);
        }
        protected virtual void OnLeftKeyDown()
        {
            SetTransformation(new Rect(x: -50.0f), 1.0f);
        }
        protected virtual void OnRightKeyDown()
        {
            SetTransformation(new Rect(x: 50.0f), 1.0f);
        }
    }
}
