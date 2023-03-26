using Kotono.Graphics.Objects.Hitboxes;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kotono.Graphics.Objects
{
    internal class HitboxManager
    {
        private readonly List<IHitbox> _hitboxes = new();

        /// <summary>
        /// Key: Direct Index,
        /// Value: Real Index.
        /// </summary>
        private readonly Dictionary<int, int> _indexOffset = new();

        private int _hitboxIndex = 0;

        public HitboxManager() { }

        public int Create(IHitbox mesh)
        {
            _indexOffset[_hitboxIndex] = _hitboxes.Count;

            _hitboxes.Add(mesh);

            return _hitboxIndex++;
        }

        public void Delete(int index)
        {
            if (_hitboxes.Count <= 0)
            {
                throw new Exception($"The number of Hitbox is already at 0.");
            }

            _hitboxes.RemoveAt(_indexOffset[index]);
            _indexOffset.Remove(index);

            foreach (var i in _indexOffset.Keys)
            {
                if (i > index)
                {
                    _indexOffset[i]--;
                }
            }
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

        public void AddCollision(int index, int hitboxIndex)
            => _hitboxes[_indexOffset[index]].Collisions.Add(hitboxIndex);

        public void AddCollision(int index, int[] hitboxIndexes)
            => _hitboxes[_indexOffset[index]].Collisions.AddRange(hitboxIndexes);

        public int[] GetAll()
            => _indexOffset.Keys.ToArray();

        public bool IsColliding(int index)
            => _hitboxes[_indexOffset[index]].Collisions.Any(h => _hitboxes[_indexOffset[index]].Collides(_hitboxes[_indexOffset[h]]));

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
}
