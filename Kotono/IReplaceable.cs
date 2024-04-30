namespace Kotono
{
    internal interface IReplaceable<T> where T : Object
    {
        /// <summary>
        /// Replaces the values of the <see cref="IReplaceable{T}"/> by the values of obj.
        /// </summary>
        public void ReplaceBy(T obj);
    }
}
