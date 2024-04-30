namespace Kotono
{
    internal interface ICloneable<T> where T : Object
    {
        /// <summary>
        /// Get a clone of the <see cref="ICloneable{T}"/>.
        /// </summary>
        public T Clone();
    }
}
