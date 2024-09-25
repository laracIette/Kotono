using System;
using System.Threading.Tasks;

namespace Kotono.Graphics.Objects
{
    internal interface ISaveable : IObject
    {
        /// <summary>
        /// The id of the <see cref="ISaveable"/>.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// Save the <see cref="ISaveable"/>.
        /// </summary>
        public void Save();

        /// <summary>
        /// Save the <see cref="ISaveable"/> asynchronously.
        /// </summary>
        public Task SaveAsync();
    }
}
