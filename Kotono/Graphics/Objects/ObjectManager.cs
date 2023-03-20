using Kotono.Graphics.Objects.Hitboxes;
using Kotono.Graphics.Objects.Lights;
using Kotono.Graphics.Objects.Meshes;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects
{
    public class ObjectManager
    {
        private static readonly Lazy<Dictionary<string, Tuple<int, int, int>>> _paths = new(() => new());

        public static Dictionary<string, Tuple<int, int, int>> Paths => _paths.Value;

        private readonly MeshManager _meshManager = new();

        private readonly HitboxManager _hitboxManager = new();

        private readonly PointLightManager _pointLightManager = new();

        public ObjectManager() 
        {
        }

        public int CreateMesh(IMesh mesh)
            => _meshManager.Add(mesh);
        
        public int CreateHitbox(IHitbox hitbox)
            => _hitboxManager.Add(hitbox);

        public int CreatePointLight(PointLight pointLight)
            => _pointLightManager.Add(pointLight);
        

        public IMesh GetMesh(int index)
            => _meshManager.Get(index);

        public IHitbox GetHitbox(int index)
            => _hitboxManager.Get(index);

        public PointLight GetPointLight(int index)
            => _pointLightManager.Get(index);

        public void RemoveMesh(int index)
        {
            _meshManager.Remove(index);
        }

        public void RemoveHitbox(int index)
        { 
            _hitboxManager.Remove(index);
        }

        public void RemovePointLight(int index)
        {
            _pointLightManager.Remove(index);
        }

        public void SetHitBoxPosition(int index, Vector3 position)
        {
            _hitboxManager.SetPosition(index, position);
        }

        public void SetHitBoxAngle(int index, Vector3 angle)
        {
            _hitboxManager.SetAngle(index, angle);
        }

        public void SetHitBoxScale(int index, Vector3 scale)
        {
            _hitboxManager.SetScale(index, scale);
        }

        public void SetHitBoxColor(int index, Vector3 color)
        {
            _hitboxManager.SetColor(index, color);
        }

        public int GetPointLightsCount()
            => _pointLightManager.GetCount();

        public int GetFirstPointLightIndex()
            => _pointLightManager.GetFirstIndex();

        public void Update()
        {
            _meshManager.Update();
            _hitboxManager.Update();
            _pointLightManager.Update();
        }

        public void UpdateShaders()
        {
            _pointLightManager.UpdateShaders();
        }

        public void Draw()
        {
            _meshManager.Draw();
            _hitboxManager.Draw();
            _pointLightManager.Draw();
        }

        internal class MeshManager
        {
            private readonly List<IMesh> _meshes = new();

            private readonly Dictionary<int, int> _indexOffset = new();

            private int _meshesCount = 0;

            public MeshManager()
            {
            }

            public int Add(IMesh mesh)
            {
                _indexOffset[_meshesCount] = _meshes.Count;

                _meshes.Add(mesh);

                return _meshesCount++;
            }

            public void Remove(int index)
            {
                _meshes.RemoveAt(_indexOffset[index]);

                foreach (var i in _indexOffset.Keys)
                {
                    if (i > index)
                    {
                        _indexOffset[i]--;
                    }
                }
            }

            public IMesh Get(int index)
            {
                return _meshes[_indexOffset[index]];
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

        internal class HitboxManager
        {
            private readonly List<IHitbox> _hitboxes = new();

            private readonly Dictionary<int, int> _indexOffset = new();

            private int _hitboxesCount = 0;

            public HitboxManager()
            {
            }

            public int Add(IHitbox mesh)
            {
                _indexOffset[_hitboxesCount] = _hitboxes.Count;

                _hitboxes.Add(mesh);

                return _hitboxesCount++;
            }

            public void Remove(int index)
            {
                _hitboxes.RemoveAt(_indexOffset[index]);

                foreach (var i in _indexOffset.Keys)
                {
                    if (i > index)
                    {
                        _indexOffset[i]--;
                    }
                }
            }

            public IHitbox Get(int index)
            {
                return _hitboxes[_indexOffset[index]];
            }

            public void SetPosition(int index, Vector3 position)
            {
                _hitboxes[_indexOffset[index]].Position = position;
            }

            public void SetAngle(int index, Vector3 angle)
            {
                _hitboxes[_indexOffset[index]].Angle = angle;
            }

            public void SetScale(int index, Vector3 scale)
            {
                _hitboxes[_indexOffset[index]].Scale = scale;
            }

            public void SetColor(int index, Vector3 color)
            {
                _hitboxes[_indexOffset[index]].Color = color;
            }

            public void Update()
            {
                foreach (var hitbox in _hitboxes)
                {
                    hitbox.Update();
                }
            }

            public void Draw()
            {
                foreach (var hitbox in _hitboxes)
                {
                    hitbox.Draw();
                }
            }
        }

        internal class PointLightManager
        {
            private readonly List<PointLight> _pointLights = new();

            /// <summary>
            /// Key: Direct Index,
            /// Value: Real Index.
            /// </summary>
            private readonly Dictionary<int, int> _indexOffset = new();

            private int _pointLightsCount = 0;

            public PointLightManager()
            {
            }

            public int Add(PointLight mesh)
            {
                _indexOffset[_pointLightsCount] = _pointLights.Count;

                _pointLights.Add(mesh);

                return _pointLightsCount++;
            }

            public void Remove(int index)
            {
                _pointLights.RemoveAt(_indexOffset[index]);
                _indexOffset.Remove(index);

                foreach (var i in _indexOffset.Keys)
                {
                    if (i > index)
                    {
                        _indexOffset[i]--;
                        _pointLights[_indexOffset[i]].UpdateIndex();
                    }
                }
            }

            public PointLight Get(int index)
            {
                return _pointLights[_indexOffset[index]];
            }

            public int GetCount()
                => _pointLights.Count;

            public int GetFirstIndex()
                => _indexOffset.First().Key;

            public void Update()
            {
                foreach (var pointLight in _pointLights)
                {
                    pointLight.Update();
                }
            }

            public void UpdateShaders()
            {
                foreach (var pointLight in _pointLights)
                {
                    pointLight.UpdateShaders();
                }
            }

            public void Draw()
            {
                foreach (var pointLight in _pointLights)
                {
                    pointLight.Draw();
                }
            }
        }
    }
}