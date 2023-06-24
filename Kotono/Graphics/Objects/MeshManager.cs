using Kotono.Graphics.Objects.Meshes;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects
{
    internal class MeshManager
    {
        private readonly List<IMesh> _meshes = new();

        internal MeshManager() { }

        internal void Create(IMesh mesh)
        {
            _meshes.Add(mesh);
        }

        internal void Delete(IMesh mesh)
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
                mesh.Draw();
            }
        }
    }
}
