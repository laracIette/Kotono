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
            if (!Drawables.Contains(obj))
            {
                // index of the first obj of a superior layer
                int index = Drawables.FindIndex(i => i.Layer > obj.Layer);

                // if every obj has an inferior Layer
                if (index == -1)
                {
                    // add obj at the end
                    Drawables.Add(obj);
                }
                else
                {
                    // insert before the first obj of a superior layer 
                    Drawables.Insert(index, obj);
                }
            }
        }

        internal override void Draw()
        {
            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);

            // TODO: use foreach when developement done
            for (int i = 0; i < Drawables.Count; i++)
            {
                if (Drawables[i].IsDraw)
                {
                    // If Drawables[i] is an IElement, use its viewport, else use window viewport
                    ((Drawables[i] as IElement)?.Viewport ?? ComponentManager.Window.Viewport).Use();

                    if (Rect.Overlaps(Drawables[i].Dest, Rect.FromAnchor(ComponentManager.ActiveViewport.Dest, Anchor.TopLeft)))
                    {
                        Drawables[i].Draw();
                    }
                }
            }

            GL.Disable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
        }
    }
}
