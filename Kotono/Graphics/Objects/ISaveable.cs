namespace Kotono.Graphics.Objects
{
    internal interface ISaveable : IObject
    {
        /// <summary>
        /// Save the <see cref="ISaveable"/>.
        /// </summary>
        public void Save();
    }
}
