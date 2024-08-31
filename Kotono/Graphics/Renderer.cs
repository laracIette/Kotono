using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal sealed class Renderer : IRenderer, IDisposable
    {
        private readonly Framebuffer _framebuffer = new();

        private readonly List<IFrontMesh> _frontMeshRenderQueue = [];

        private readonly List<IObject2D> _object2DRenderQueue = [];

        private readonly List<IObject3D> _object3DRenderQueue = [];

        internal void SetSize(Point size) => _framebuffer.Size = size;

        #region RenderQueue

        public void AddToRenderQueue(IDrawable drawable) // TODO: use IEnumerator and yield return,
                                                         // for each Drawable, create branch
        {
            if (!drawable.IsDraw)
            {
                return;
            }

            switch (drawable)
            {
                case IFrontMesh frontMesh:
                    AddToFrontMeshRenderQueue(frontMesh);
                    break;

                case IObject2D object2D:
                    AddToObject2DRenderQueue(object2D);
                    break;

                case IObject3D object3D:
                    AddToObject3DRenderQueue(object3D);
                    break;

                default:
                    break;
            }
        }

        private void AddToObject2DRenderQueue(IObject2D object2D)
        {
            var position = Rect.GetPositionFromAnchor(object2D.Viewport.RelativePosition, object2D.Viewport.RelativeSize, Anchor.TopLeft);

            if (!Rect.Overlaps(object2D.Rect, new RectBase(position, object2D.Viewport.RelativeSize)))
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

        private void AddToFrontMeshRenderQueue(IFrontMesh frontMesh)
        {
            _frontMeshRenderQueue.Add(frontMesh);
        }

        private void ClearRenderQueues()
        {
            _object2DRenderQueue.Clear();
            _object3DRenderQueue.Clear();
            _frontMeshRenderQueue.Clear();
        }

        #endregion RenderQueue

        #region Render

        public void Render()
        {
            _framebuffer.BeginDraw();

            DrawObject3DRenderQueue();
            DrawFrontMeshRenderQueue();
            DrawObject2DRenderQueue();

            _framebuffer.DrawBufferTextures();

            ClearRenderQueues();

        }

        private void DrawObject2DRenderQueue()
        {
            GL.Enable(EnableCap.Blend);

            _object2DRenderQueue.ForEach(DrawObject2D);

            GL.Disable(EnableCap.Blend);
        }

        private void DrawObject3DRenderQueue()
        {
            GL.Enable(EnableCap.DepthTest);

            _object3DRenderQueue.ForEach(DrawObject3D);

            GL.Disable(EnableCap.DepthTest);
        }

        private void DrawFrontMeshRenderQueue()
        {
            //GL.Enable(EnableCap.DepthTest);

            //GL.Clear(ClearBufferMask.DepthBufferBit);

            _frontMeshRenderQueue.ForEach(DrawFrontMesh);

            //GL.Disable(EnableCap.DepthTest);
        }

        private static void DrawDrawable(IDrawable drawable)
        {
            drawable.Viewport.Use();
            drawable.UpdateShader();
            drawable.Draw();
        }

        private static void DrawObject2D(IObject2D object2D) => DrawDrawable(object2D);

        private static void DrawObject3D(IObject3D object3D) => DrawDrawable(object3D);

        private static void DrawFrontMesh(IFrontMesh frontMesh) => DrawDrawable(frontMesh);

        #endregion Render

        public void Dispose()
        {
            _framebuffer.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
