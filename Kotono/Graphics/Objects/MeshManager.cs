﻿using Kotono.Graphics.Objects.Meshes;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;

namespace Kotono.Graphics.Objects
{
    internal class MeshManager
    {
        private readonly List<IMesh> _meshes = new();

        /// <summary>
        /// Key: Direct Index,
        /// Value: Real Index.
        /// </summary>
        private readonly Dictionary<int, int> _indexOffset = new();

        private int _meshIndex = 0;

        public MeshManager() { }

        public int Create(IMesh mesh)
        {
            _indexOffset[_meshIndex] = _meshes.Count;

            _meshes.Add(mesh);

            return _meshIndex++;
        }

        public void Delete(int index)
        {
            if (_meshes.Count <= 0)
            {
                throw new Exception($"The number of Mesh is already at 0.");
            }

            _meshes[_indexOffset[index]].Dispose();
            _meshes.RemoveAt(_indexOffset[index]);
            _indexOffset.Remove(index);

            foreach (var i in _indexOffset.Keys)
            {
                if (i > index)
                {
                    _indexOffset[i]--;
                }
            }
        }

        public Vector3 GetPosition(int index)
            => _meshes[_indexOffset[index]].Position;

        public void SetColor(int index, Vector3 color)
        {
            _meshes[_indexOffset[index]].Color = color;
        }

        public void Update()
        {
            foreach (var mesh in _meshes)
            {
                mesh.Update();
            }
        }

        public void Draw()
        {
            foreach (var mesh in _meshes)
            {
                mesh.Draw();
            }
        }
    }
}
