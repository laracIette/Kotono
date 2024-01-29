using Kotono.Engine.UserInterface.Elements;
using Kotono.Graphics.Objects;
using Kotono.Utils;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal class Renderer : IDisposable
    {
        private readonly FrameBuffer _frameBuffer = new(KT.Size);

        private readonly List<IObject2D> _object2DRenderQueue = [];
        
        private readonly List<IObject3D> _object3DRenderQueue = [];
        
        private readonly List<IDrawable> _drawableRenderQueue = [];
        
        internal void SetSize(Point value)
        {
            _frameBuffer.Size = value;
        }

        #region RenderQueue

        internal void AddToRenderQueue(IDrawable drawable)
        {
            if (!drawable.IsDraw)
            {
                return;
            }

            switch (drawable)
            {
                case IObject2D object2D:
                    AddToObject2DRenderQueue(object2D);
                    break;

                case IObject3D object3D:
                    AddToObject3DRenderQueue(object3D);
                    break;

                default:
                    AddToDrawableRenderQueue(drawable);
                    break;
            }
        }

        private void AddToObject2DRenderQueue(IObject2D object2D)
        {
            if (!Rect.Overlaps(object2D.Dest, Rect.FromAnchor(((object2D as IElement)?.Viewport ?? ComponentManager.WindowViewport).Dest, Anchor.TopLeft)))
            {
                return;
            }

            // Index of the first object of a superior layer
            int index = _object2DRenderQueue.FindIndex(i => i.Layer > object2D.Layer);

            // If every object has an inferior Layer
            if (index == -1)
            {
                // Add object2D at the end
                _object2DRenderQueue.Add(object2D);
            }
            else
            {
                // Insert before the first object of a superior layer 
                _object2DRenderQueue.Insert(index, object2D);
            }
        }

        private void AddToObject3DRenderQueue(IObject3D object3D)
        {
            _object3DRenderQueue.Add(object3D);
        }

        private void AddToDrawableRenderQueue(IDrawable drawable)
        {
            _drawableRenderQueue.Add(drawable);
        }

        private void ClearRenderQueues()
        {
            _object2DRenderQueue.Clear();
            _object3DRenderQueue.Clear();
            _drawableRenderQueue.Clear();
        }

        #endregion RenderQueue

        #region Render

        internal void Render()
        {
            _frameBuffer.PreDraw();

            DrawObject3DRenderQueue();
            DrawObject2DRenderQueue();

            _frameBuffer.DrawBufferTextures();

            ClearRenderQueues();
        }

        private void DrawObject2DRenderQueue()
        {
            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);

            foreach (var object2D in _object2DRenderQueue)
            {
                DrawObject2D(object2D);
            }

            GL.Disable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
        }

        private static void DrawObject2D(IObject2D object2D) 
        {
            ((object2D as IElement)?.Viewport ?? ComponentManager.WindowViewport).Use();

            object2D.Draw();
        }

        private void DrawObject3DRenderQueue()
        {
            ComponentManager.WindowViewport.Use();

            foreach (var object3D in _object3DRenderQueue)
            {
                object3D.Draw();
            }
        }

        #endregion Render

        public void Dispose()
        {
            _frameBuffer.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
