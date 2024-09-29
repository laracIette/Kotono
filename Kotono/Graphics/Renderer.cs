using Kotono.Graphics.Objects;
using Kotono.Graphics.Objects.Meshes;
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

        private readonly List<IObject3D> _object3DOpaqueRenderQueue = [];

        private readonly List<IObject3D> _object3DTransparentRenderQueue = [];

        private ICubemap? _cubemap = null;

        private int _renders = 0;

        internal void SetSize(Point size) => _framebuffer.Size = size;

        #region RenderQueue

        public void AddToRenderQueue(IDrawable drawable)
        {
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

                case ICubemap cubemap:
                    _cubemap = cubemap;
                    break;

                default:
                    break;
            }
        }

        private void AddToObject2DRenderQueue(IObject2D object2D)
        {
            // TODO: plz
            //var position = Rect.GetPositionFromAnchor(object2D.Viewport.RelativePosition, object2D.Viewport.RelativeSize, Anchor.TopLeft);

            //if (!Rect.Overlaps(object2D.Rect, new RectBase(position, object2D.Viewport.RelativeSize)))
            //{
            //    return;
            //}

            _object2DRenderQueue.Add(object2D);
        }

        private void AddToObject3DRenderQueue(IObject3D object3D)
        {
            if (object3D.Color.A >= 1.0f)
            {
                _object3DOpaqueRenderQueue.Add(object3D);
            }
            else
            {
                _object3DTransparentRenderQueue.Add(object3D);
            }
        }

        private void AddToFrontMeshRenderQueue(IFrontMesh frontMesh)
            => _frontMeshRenderQueue.Add(frontMesh);

        private void ClearRenderQueues()
        {
            _object2DRenderQueue.Clear();
            _object3DOpaqueRenderQueue.Clear();
            _object3DTransparentRenderQueue.Clear();
            _frontMeshRenderQueue.Clear();
            _cubemap = null;
        }

        #endregion RenderQueue

        #region Render

        public void Render()
        {
            _framebuffer.BeginDraw();

            DrawCubemap();
            DrawObject3DOpaqueRenderQueue();
            DrawObject3DTransparentRenderQueue();
            DrawFrontMeshRenderQueue();
            DrawObject2DRenderQueue();

            _framebuffer.DrawBufferTextures();

            ClearRenderQueues();

            ++_renders;
        }

        private void DrawObject2DRenderQueue()
        {
            if (_object2DRenderQueue.Count == 0)
            {
                return;
            }

            _object2DRenderQueue.Sort((a, b) => a.Layer.CompareTo(b.Layer));

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            _object2DRenderQueue.ForEach(DrawDrawable);

            GL.Disable(EnableCap.Blend);
        }

        private void DrawObject3DOpaqueRenderQueue()
        {
            if (_object3DOpaqueRenderQueue.Count == 0)
            {
                return;
            }

            GL.DepthFunc(DepthFunction.Lequal);
            GL.Enable(EnableCap.DepthTest);

            _object3DOpaqueRenderQueue.ForEach(DrawDrawable);

            GL.DepthFunc(DepthFunction.Less);
            GL.Disable(EnableCap.DepthTest);
        }

        private void DrawObject3DTransparentRenderQueue()
        {
            if (_object3DTransparentRenderQueue.Count == 0)
            {
                return;
            }

            _object3DTransparentRenderQueue.Sort(
                (a, b) => Vector.Distance(b.WorldLocation, Camera.Active.WorldLocation)
                .CompareTo(Vector.Distance(a.WorldLocation, Camera.Active.WorldLocation))
            );

            GL.DepthMask(false);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.DepthTest);

            _object3DTransparentRenderQueue.ForEach(DrawDrawable);

            GL.DepthMask(true);
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.DepthTest);
        }

        private void DrawFrontMeshRenderQueue()
        {
            if (_frontMeshRenderQueue.Count == 0)
            {
                return;
            }

            //GL.Enable(EnableCap.DepthTest);
            //GL.Clear(ClearBufferMask.DepthBufferBit);

            _frontMeshRenderQueue.ForEach(DrawDrawable);

            //GL.Disable(EnableCap.DepthTest);
        }

        private void DrawCubemap()
        {
            if (_cubemap is null)
            {
                return;
            }

            GL.DepthMask(false);
            GL.DepthFunc(DepthFunction.Lequal);
            DrawDrawable(_cubemap);

            GL.DepthMask(true);
            GL.DepthFunc(DepthFunction.Less);
        }

        private static void DrawDrawable(IDrawable drawable)
        {
            drawable.Viewport.Use();
            drawable.UpdateShader();
            drawable.Draw();
        }

        #endregion Render

        public void Dispose() => _framebuffer.Dispose();
    }
}
