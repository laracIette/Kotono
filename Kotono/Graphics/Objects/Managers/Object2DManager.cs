using Kotono.Engine.UserInterface.Elements;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects.Managers
{
    internal class Object2DManager : DrawableManager<IObject2D>
    {
        internal Object2DManager()
            : base() { }

        internal override void Create(IObject2D obj)
        {
            if (!_drawables.Contains(obj))
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
        }

        internal override void Draw()
        {
            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);

            for (int i = 0; i < _drawables.Count; i++)
            {
                if (_drawables[i].IsDraw && Rect.Overlaps(_drawables[i].Dest, Rect.FromAnchor(new Rect(Point.Zero, KT.Size), Anchor.TopLeft)))
                {
                    if (_drawables[i] is IElement element)
                    {
                        element.Viewport.Use();
                    }
                    else
                    {
                        ComponentManager.Window.Viewport.Use();
                    }
                    _drawables[i].Draw();
                }
            }

            GL.Disable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
        }
    }
}
