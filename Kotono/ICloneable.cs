namespace Kotono
{
    internal interface ICloneable<T> where T : IObject
    {
        /// <summary>
        /// Get a clone of the <see cref="ICloneable{T}"/>.
        /// </summary>
        public T Clone();
    }
}
