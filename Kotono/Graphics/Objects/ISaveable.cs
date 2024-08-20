namespace Kotono.Graphics.Objects
{
    internal interface ISaveable
    {
        /// <summary>
        /// The path to the save file of the ISaveable.
        /// </summary>
        public string FilePath { get; set; }

        public void Save();
    }
}
