using Kotono.Engine.UserInterface.Elements;
using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Meshes;
using Kotono.Utils;
using Kotono.Utils.Coordinates;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace Kotono.Graphics
{
    internal class Renderer : IRenderer, IDisposable
    {
        private readonly Framebuffer _framebuffer = new();

        private readonly List<IFrontMesh> _frontMeshRenderQueue = [];
        
        private readonly List<IObject2D> _object2DRenderQueue = [];
        
        private readonly List<IObject3D> _object3DRenderQueue = [];

        internal void SetSize(Point value) => _framebuffer.Size = value;

        #region RenderQueue

        public void AddToRenderQueue(IDrawable drawable)
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
            if (!Rect.Overlaps(object2D.Dest, Rect.FromAnchor(((object2D as IElement)?.Viewport ?? WindowComponentManager.WindowViewport).Dest, Anchor.TopLeft)))
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

            foreach (var object2D in _object2DRenderQueue)
            {
                DrawObject2D(object2D);
            }

            GL.Disable(EnableCap.Blend);
        }

        private static void DrawObject2D(IObject2D object2D) 
        {
            ((object2D as IElement)?.Viewport ?? WindowComponentManager.WindowViewport).Use();

            object2D.Draw();
        }

        private void DrawObject3DRenderQueue()
        {
            GL.Enable(EnableCap.DepthTest);

            WindowComponentManager.WindowViewport.Use();

            foreach (var object3D in _object3DRenderQueue)
            {
                object3D.Draw();
            }

            GL.Disable(EnableCap.DepthTest);
        }

        private void DrawFrontMeshRenderQueue()
        {
            //GL.Enable(EnableCap.DepthTest);

            //GL.Clear(ClearBufferMask.DepthBufferBit);

            WindowComponentManager.WindowViewport.Use();

            foreach (var frontMesh in _frontMeshRenderQueue)
            {
                frontMesh.Draw();
            }

            //GL.Disable(EnableCap.DepthTest);
        }

        #endregion Render

        public void Dispose()
        {
            _framebuffer.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
