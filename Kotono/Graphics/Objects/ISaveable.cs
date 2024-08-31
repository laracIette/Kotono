namespace Kotono.Graphics.Objects
{
    internal interface ISaveable : IObject
    {
        /// <summary>
        /// The path where the <see cref="ISaveable"/> should be saved.
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// Save the <see cref="ISaveable"/>.
        /// </summary>
        public void Save();
    }
}
