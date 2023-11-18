using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects.Managers
{
    public class Object2DManager : DrawableManager<IObject2D>
    {
        public Object2DManager() 
            : base() { }

        public override void Create(IObject2D obj)
        {
            // index of the first obj of a superior layer
            int index = _drawables.FindIndex(i => i.Layer > obj.Layer);

            // if every obj has an inferior Layer
            if (index == -1)
            {
                // add obj at the end
                _drawables.Add(obj);
            }
            else
            {
                // insert before the first obj of a superior layer 
                _drawables.Insert(index, obj);
            }
        }

        public override void Draw()
        {
            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);

            foreach (var drawable in _drawables)
            {
                if (drawable.IsDraw && Rect.Overlaps(drawable.Dest, Rect.FromAnchor(new Rect(Point.Zero, KT.Size), Anchor.TopLeft)))
                {
                    drawable.Draw();
                }
            }

            GL.Disable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
        }
    }
}
