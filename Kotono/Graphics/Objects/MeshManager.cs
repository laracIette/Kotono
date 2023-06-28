using Kotono.Graphics.Objects.Meshes;
using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects
{
    internal class MeshManager
    {
        private readonly List<Mesh> _meshes = new();

        internal MeshManager() { }

        internal void Create(Mesh mesh)
        {
            _meshes.Add(mesh);
        }

        internal void Delete(Mesh mesh)
        {
            if (_meshes.Count <= 0)
            {
                KT.Print($"The number of Mesh is already at 0.");
            }
            else
            {
                _meshes.Remove(mesh);
            }
        }


        internal void Init()
        {
            foreach (var mesh in _meshes)
            {
                mesh.Init();
            }
        }

        internal void Update()
        {
            foreach (var mesh in _meshes)
            {
                mesh.Update();
            }
        }

        internal void Draw()
        {
            foreach (var mesh in _meshes)
            {
                if (!mesh.IsInFront && mesh.IsDraw)
                {
                    mesh.Draw();
                }
            }
            GL.Clear(ClearBufferMask.DepthBufferBit);
            foreach (var mesh in _meshes)
            {
                if (mesh.IsInFront && mesh.IsDraw)
                {
                    mesh.Draw();
                }
            }
        }
    }
}
