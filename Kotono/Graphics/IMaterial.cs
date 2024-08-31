namespace Kotono.Graphics
{
    internal interface IMaterial
    {
        /// <summary>
        /// Use all the <see cref="ImageTexture"/> of the <see cref="IMaterial"/>.
        /// </summary>
        public void Use();
    }
}
