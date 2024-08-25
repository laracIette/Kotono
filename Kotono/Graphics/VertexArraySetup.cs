namespace Kotono.Graphics
{
    internal sealed class VertexArraySetup
    {
        internal VertexArrayObject VertexArrayObject { get; } = new();

        internal VertexBufferObject VertexBufferObject { get; } = new();
    }
}
