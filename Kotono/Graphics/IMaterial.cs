using Kotono.Graphics.Textures;

namespace Kotono.Graphics
{
    internal interface IMaterial
    {
        /// <summary>
        /// Use all the <see cref="ImageTexture"/>s of the <see cref="IMaterial"/>.
        /// </summary>
        public void Use();
    }
}
