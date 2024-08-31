namespace Kotono.Graphics.Objects
{
    internal class MaterialTexture(string path)
        : ImageTexture(path)
    {
        internal float Strength { get; set; } = 1.0f;
    }
}
