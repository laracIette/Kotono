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

        internal HitboxManager() { }

        internal int Create(IHitbox mesh)
        {
            _indexOffset[_hitboxIndex] = _hitboxes.Count;

            _hitboxes.Add(mesh);

            return _hitboxIndex++;
        }

        internal void Delete(int index)
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

        internal void SetPosition(int index, Vector3 position)
        {
            _hitboxes[_indexOffset[index]].Position = position;
        }

        internal void SetAngle(int index, Vector3 angle)
        {
            _hitboxes[_indexOffset[index]].Angle = angle;
        }

        internal void SetScale(int index, Vector3 scale)
        {
            _hitboxes[_indexOffset[index]].Scale = scale;
        }

        internal void SetColor(int index, Vector3 color)
        {
            _hitboxes[_indexOffset[index]].Color = color;
        }

        internal void AddCollision(int index, int hitboxIndex)
            => _hitboxes[_indexOffset[index]].Collisions.Add(hitboxIndex);

        internal void AddCollision(int index, int[] hitboxIndexes)
            => _hitboxes[_indexOffset[index]].Collisions.AddRange(hitboxIndexes);

        internal int[] GetAll()
            => _indexOffset.Keys.ToArray();

        internal bool IsColliding(int index)
            => _hitboxes[_indexOffset[index]].Collisions.Any(h => _hitboxes[_indexOffset[index]].Collides(_hitboxes[_indexOffset[h]]));

        internal void Update()
        {
            foreach (var hitbox in _hitboxes)
            {
                hitbox.Update();
            }
        }

        internal void Draw()
        {
            foreach (var hitbox in _hitboxes)
            {
                hitbox.Draw();
            }
        }
    }
}
