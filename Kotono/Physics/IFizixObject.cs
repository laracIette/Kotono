namespace Kotono.Physics
{
    internal interface IFizixObject : IObject
    {
        /// <summary>
        /// Wether the <see cref="IFizixObject"/> should update the physics.
        /// </summary>
        public bool IsUpdateFizix { get; set; }

        /// <summary>
        /// Update the physics of the <see cref="IFizixObject"/>.
        /// </summary>
        public void UpdateFizix();
    }
}
