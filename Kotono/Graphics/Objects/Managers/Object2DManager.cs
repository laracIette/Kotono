using Kotono.Engine.UserInterface.Elements;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;

namespace Kotono.Graphics.Objects.Managers
{
    internal class Object2DManager()
        : DrawableManager<Object2D>()
    {
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
                    ((Drawables[i] as IElement)?.Viewport ?? ComponentManager.WindowViewport).Use();

                    if (Rect.Overlaps(Drawables[i].Dest, Rect.FromAnchor(ComponentManager.ActiveViewport.Dest, Anchor.TopLeft)))
                    {
                        Drawables[i].Draw();
                    }
                }
            }

            GL.Disable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
        }

        internal void UpdateLayer(Object2D obj)
        {
            // Remove obj 
            Drawables.Remove(obj);

            // Index of the first object of a superior layer
            int index = Drawables.FindIndex(i => i.Layer > obj.Layer);

            // If every object has an inferior Layer
            if (index == -1)
            {
                // Add obj at the end
                Drawables.Add(obj);
            }
            else
            {
                // Insert before the first object of a superior layer 
                Drawables.Insert(index, obj);
            }
        }
    }
}
