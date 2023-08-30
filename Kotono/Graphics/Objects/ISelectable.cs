namespace Kotono.Graphics.Objects
{
    public interface ISelectable
    {
        public bool IsSelected { get; }

        public bool IsActive { get; }
    }
}
