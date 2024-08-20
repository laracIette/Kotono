using System;

namespace Kotono
{
    /// <summary>
    /// An object that can be updated.
    /// </summary>
    internal interface IObject : IDisposable
    {
        /// <summary>
        /// Wether the <see cref="IObject"/> should be updated.
        /// </summary>
        public bool IsUpdate { get; set; }

        /// <summary>
        /// Wether the <see cref="IObject"/> should be deleted, if true, the <see cref="IObject"/> will be deleted before the next update.
        /// </summary>
        public bool IsDelete { get; }

        /// <summary>
        /// The time at which the <see cref="IObject"/> was instantiated.
        /// </summary>
        public float CreationTime { get; }

        /// <summary>
        /// The elapsed time since the <see cref="IObject"/> was instantiated.
        /// </summary>
        public float TimeSinceCreation { get; }

        /// <summary>
        /// Update the <see cref="IObject"/>.
        /// </summary>
        public void Update();
    }
}
